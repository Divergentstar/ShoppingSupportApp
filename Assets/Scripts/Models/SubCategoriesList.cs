using Assets.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;
using Boomlagoon.JSON;

public class SubCategoriesList : MonoBehaviour
{

    public Transform contentPanel;
    public SimpleObjectPool categoryObjectPool;
    private WWW www;
    private string databaseUrl = "http://gmanager.herokuapp.com/api/category/list/";
    private string jsonData = "";
    private string[] jsonDataObjects;
    private int mainCategoryId = 3;
    private CategoryData foundMaincategory;
    private CategoryData foundSubcategory;
    private ImageData foundImage;

    // Use this for initialization
    void Start()
    {
        GetMainCategoryId();
        GetSubCategories();
    }

    void GetMainCategoryId()
    {
        MovingController movingController = GetComponent<MovingController>();
        mainCategoryId = movingController.GetCategoryId();
    }

    //Invoke this function where to want to make request.
    void GetSubCategories()
    {
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataSubCategories(www));
    }

    IEnumerator GetDataSubCategories(WWW www)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONArray jsonArrayCategories = JSONArray.Parse(jsonData);

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

            if (jsonArrayCategories == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                for (int i = 0; i < jsonArrayCategories.Length; i++)
                {
                    foundSubcategory = new CategoryData();
                    foundImage = new ImageData();

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectCategory = JSONObject.Parse(jsonDataObjects[i]);

                    //foundImage.Image = spriteColruyt;

                    if (Convert.ToInt32(jsonObjectCategory["parent_id"].Number) == mainCategoryId)
                    {
                        GetMainCategory(i);
                    }
                }
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    //Invoke this function where to want to make request.
    void GetMainCategory(int index)
    {
        databaseUrl = "http://gmanager.herokuapp.com/api/category/get/" + mainCategoryId;
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataMainCategory(www, index));
    }

    IEnumerator GetDataMainCategory(WWW www, int index)
    {
        //Wait for request to complete
        yield return www;

        if (www.error == null)
        {
            jsonData = www.text;
            Debug.Log(jsonData);

            //Data is in json format, we need to parse the Json.
            JSONObject jsonObjectMainCategory = JSONObject.Parse(jsonData);

            if (jsonObjectMainCategory == null)
            {
                Debug.Log("No data converted");
            }
            else
            {
                foundMaincategory = new CategoryData();

                //Data is in json format, we need to parse the Json.
                JSONObject jsonObjectCategory = JSONObject.Parse(jsonDataObjects[index]);

                //now we can get the values from json of any attribute.
                foundMaincategory.Id = Convert.ToInt32(jsonObjectMainCategory["id"].Number);
                foundMaincategory.Name = jsonObjectMainCategory["name"].Str;

                foundSubcategory.Id = Convert.ToInt32(jsonObjectCategory["id"].Number);
                foundSubcategory.Name = jsonObjectCategory["name"].Str;
                foundSubcategory.Image = foundImage;
                foundSubcategory.MainCategory = foundMaincategory;

                GameObject newCategoryListItem = categoryObjectPool.GetObject();
                newCategoryListItem.transform.SetParent(contentPanel);

                SubCategoryListItem categoryListItem = newCategoryListItem.GetComponent<SubCategoryListItem>();
                categoryListItem.ShowCategoryInfo(foundSubcategory);
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
