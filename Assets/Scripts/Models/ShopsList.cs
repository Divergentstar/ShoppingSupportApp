﻿using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

public class ShopsList : MonoBehaviour {

    public Transform contentPanel;
    public SimpleObjectPool shopObjectPool;
    public Sprite spriteColruyt;
    private WWW www;
    private string databaseUrl = "http://gmanager.herokuapp.com/api/shop/list/";
    private string jsonData = "";
    private string[] jsonDataObjects;
    private ShopData foundShop;
    private ShopNetworkData foundShopNetwork;
    private ImageData foundImage;

    // Use this for initialization
    void Start () {
		
	}

    //Invoke this function where to want to make request.
    void GetShops()
    {
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataShops(www));
    }

    IEnumerator GetDataShops(WWW www)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            //Data is in json format, we need to parse the Json.
            JSONArray jsonArrayShops = JSONArray.Parse(jsonData);

            //parse text to array of strings
            jsonData = jsonData.Replace("[", "");
            jsonData = jsonData.Replace("]", "");
            jsonDataObjects = jsonData.Split('}');

            for (int i = 0; i < jsonDataObjects.Length; i++)
            {
                jsonDataObjects[i] = jsonDataObjects[i] + "}";

                if (i > 0)
                {
                    jsonDataObjects[i] = jsonDataObjects[i].Substring(1);
                }
            }

            if (jsonArrayShops == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                for (int i = 0; i < jsonArrayShops.Length; i++)
                {
                    foundShop = new ShopData();
                    foundShopNetwork = new ShopNetworkData();
                    foundImage = new ImageData();

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectShop = JSONObject.Parse(jsonDataObjects[i]);

                    //sending the request to url
                    databaseUrl = "http://gmanager.herokuapp.com/api/network/get/" + Convert.ToInt32(jsonObjectShop["shop_network_id"].Number);
                    www = new WWW(databaseUrl);

                    //Wait for request to complete
                    yield return www;

                    if (www.error == null)
                    {
                        jsonData = www.text;
                        //Data is in json format, we need to parse the Json.
                        JSONObject jsonObjectShopNetwork = JSONObject.Parse(jsonData);

                        if (jsonObjectShopNetwork == null)
                        {
                            Debug.Log("No data converted");
                        }
                        else
                        {
                            foundImage.Image = spriteColruyt;

                            foundShopNetwork.Id = Convert.ToInt32(jsonObjectShopNetwork["id"].Number);
                            foundShopNetwork.Name = jsonObjectShopNetwork["name"].Str;
                            foundShopNetwork.Image = foundImage;

                            //now we can get the values from json of any attribute.
                            foundShop.Id = Convert.ToInt32(jsonObjectShop["id"].Number);
                            foundShop.Name = jsonObjectShop["description"].Str;
                            foundShop.Location = jsonObjectShop["address"].Str;
                            foundShop.ShopNetwork = foundShopNetwork;

                            GameObject newShopListItem = shopObjectPool.GetObject();
                            newShopListItem.transform.SetParent(contentPanel);

                            ShopListItem shopListItem = newShopListItem.GetComponent<ShopListItem>();
                            shopListItem.ShowShopInfo(foundShop);
                        }
                    }
                    else
                    {
                        Debug.Log(www.error);
                    }
                }
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
