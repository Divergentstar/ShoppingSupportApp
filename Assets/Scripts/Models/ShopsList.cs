using Assets.Scripts.Data;
using System;
using System.Collections;
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
        GetShops();
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
            Debug.Log(jsonData);

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

                    GetShopNetwork(i);
                }
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    //Invoke this function where to want to make request.
    void GetShopNetwork(int index)
    {
        //Data is in json format, we need to parse the Json.
        JSONObject jsonObjectShop = JSONObject.Parse(jsonDataObjects[index]);
        //construct the database url
        databaseUrl = "http://gmanager.herokuapp.com/api/network/get/" + Convert.ToInt32(jsonObjectShop["shop_network_id"].Number);
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataShopNetwork(www, index));
    }

    IEnumerator GetDataShopNetwork(WWW www, int index)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONObject jsonObjectShopNetwork = JSONObject.Parse(jsonData);
            JSONObject jsonObjectShop = JSONObject.Parse(jsonDataObjects[index]);

            if (jsonObjectShopNetwork == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                foundImage.Image = spriteColruyt;

                //now we can get the values from json of any attribute.
                foundShopNetwork.Id = Convert.ToInt32(jsonObjectShopNetwork["id"].Number);
                foundShopNetwork.Name = jsonObjectShopNetwork["name"].Str;
                foundShopNetwork.Image = foundImage;

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
