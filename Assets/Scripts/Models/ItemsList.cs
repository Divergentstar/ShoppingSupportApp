using Assets.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;
using Boomlagoon.JSON;

public class ItemsList : MonoBehaviour
{

    public Transform contentPanel;
    public SimpleObjectPool itemObjectPool;
    private WWW www;
    private string databaseUrl = "";
    private string jsonData = "";
    private string[] jsonDataObjects;
    private int categoryId = 6;
    private int shopId = 1;
    private int itemId = 11;
    private int imageId = 1;
    private ItemInShopData foundItemInShop;
    private ShopData foundShop;
    private ItemData foundItem;
    private CategoryData foundCategory;
    private ImageData foundImage;

    // Use this for initialization
    void Start()
    {
        GetCategoryId();
        GetShopId();
        GetItemsInShop();
    }

    void GetCategoryId()
    {
        MovingController movingController = GetComponent<MovingController>();
        //categoryId = movingController.GetCategoryId();
        GetCategory();
    }

    void GetShopId()
    {
        MovingController movingController = GetComponent<MovingController>();
        //shopId = movingController.GetShopId();
        GetShop();
    }

    //Invoke this function where to want to make request.
    void GetItemsInShop()
    {
        databaseUrl = "http://gmanager.herokuapp.com/api/good_in_shop/list/" + shopId;

        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataItemsInShop(www));
    }

    IEnumerator GetDataItemsInShop(WWW www)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONArray jsonArrayItemsInShop = JSONArray.Parse(jsonData);

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

            if (jsonArrayItemsInShop == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                for (int i = 0; i < jsonArrayItemsInShop.Length; i++)
                {
                    foundItemInShop = new ItemInShopData();

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectItemInShop = JSONObject.Parse(jsonDataObjects[i]);

                    itemId = Convert.ToInt32(jsonObjectItemInShop["good_id"].Number);
                    GetItem(i);
                }
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    //Invoke this function where to want to make request.
    void GetItem(int index)
    {
        databaseUrl = "http://gmanager.herokuapp.com/api/good/get/" + itemId;
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataItem(www, index));
    }

    IEnumerator GetDataItem(WWW www, int index)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            string jsonDataItem = jsonData;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONObject jsonObjectItem = JSONObject.Parse(jsonData);

            if (jsonObjectItem == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                foundItem = new ItemData();
                foundImage = new ImageData();

                //Data is in json format, we need to parse the Json.
                JSONObject jsonObjectItemInShop = JSONObject.Parse(jsonDataObjects[index]);

                if (Convert.ToInt32(jsonObjectItem["category_id"].Number) == categoryId)
                {
                    imageId = Convert.ToInt32(jsonObjectItem["image"].Number);
                    GetImage(index, jsonDataItem);
                }
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void GetImage(int index, string jsonDataItem)
    {
        databaseUrl = "http://gmanager.herokuapp.com/api/image/get/" + imageId;

        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataImage(www, index, jsonDataItem));
    }

    IEnumerator GetDataImage(WWW www, int index, string jsonDataItem)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONObject jsonObjectItem = JSONObject.Parse(jsonDataItem);
            JSONObject jsonObjectItemInShop = JSONObject.Parse(jsonDataObjects[index]);

            Texture2D texture = www.texture;
            Sprite spriteImage = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            foundImage.Image = spriteImage;

            //now we can get the values from json of any attribute.
            foundItem.Id = Convert.ToInt32(jsonObjectItem["id"].Number);
            foundItem.Name = jsonObjectItem["name"].Str;
            foundItem.Image = foundImage;
            foundItem.Category = foundCategory;

            foundItemInShop.Id = Convert.ToInt32(jsonObjectItemInShop["id"].Number);
            foundItemInShop.Item = foundItem;
            foundItemInShop.Shop = foundShop;
            foundItemInShop.Price = jsonObjectItemInShop["price"].Number;

            GameObject newItemListItem = itemObjectPool.GetObject();
            newItemListItem.transform.SetParent(contentPanel);

            ItemListItem itemListItem = newItemListItem.GetComponent<ItemListItem>();
            itemListItem.ShowItemInfo(foundItemInShop);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    //Invoke this function where to want to make request.
    void GetCategory()
    {
        foundCategory = new CategoryData();
        databaseUrl = "http://gmanager.herokuapp.com/api/category/get/" + categoryId;

        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataCategory(www));
    }

    IEnumerator GetDataCategory(WWW www)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONObject jsonObjectCategory = JSONObject.Parse(jsonData);

            if (jsonObjectCategory == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                foundCategory = new CategoryData();

                //now we can get the values from json of any attribute.
                foundCategory.Id = Convert.ToInt32(jsonObjectCategory["id"].Number);
                foundCategory.Name = jsonObjectCategory["name"].Str;
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    //Invoke this function where to want to make request.
    void GetShop()
    {
        foundShop = new ShopData();
        databaseUrl = "http://gmanager.herokuapp.com/api/shop/get/" + shopId;

        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataShop(www));
    }

    IEnumerator GetDataShop(WWW www)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONObject jsonObjectShop = JSONObject.Parse(jsonData);

            if (jsonObjectShop == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                foundShop = new ShopData();

                //now we can get the values from json of any attribute.
                foundShop.Id = Convert.ToInt32(jsonObjectShop["id"].Number);
                foundShop.Name = jsonObjectShop["description"].Str;
                foundShop.Location = jsonObjectShop["address"].Str;
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}