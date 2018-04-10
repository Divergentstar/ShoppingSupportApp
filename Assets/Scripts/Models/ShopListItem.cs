using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class ShopListItem : MonoBehaviour {

    public Text nameShop;
    private string databaseUrl = "http://gmanager.herokuapp.com/api/network/get/1";
    private string jsonData = "";
    ShopData foundShop = new ShopData();

    // Use this for initialization
    void Start () {
        GetData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Invoke this function where to want to make request.
    void GetData()
    {
        //sending the request to url
        WWW www = new WWW(databaseUrl);
        StartCoroutine(GetdataEnumerator(www));
    }

    IEnumerator GetdataEnumerator(WWW www)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            //Data is in json format, we need to parse the Json.
            JSONObject json = JSONObject.Parse(jsonData);

            if (json == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                //now we can get the values from json of any attribute.
                foundShop.Id = Convert.ToInt32(json["id"].Number);
                foundShop.Name = json["name"].Str;

                showShop(foundShop);
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void showShop(ShopData shop)
    {
        nameShop.text = shop.Name;
    }
}
