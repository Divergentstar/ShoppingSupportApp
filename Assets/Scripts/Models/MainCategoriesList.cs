using Assets.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;
using Boomlagoon.JSON;

public class MainCategoriesList : MonoBehaviour {

    public Sprite spritegroentenfruit;
    public Sprite spritesoep;
    public Sprite spriteparfum;
    public Sprite spritechips;
    public Sprite spriteonderhoud;
    public Sprite spritebeenhouwer;
    public Sprite spritediepvries;
    public Sprite spritehuisdieren;
    public Sprite spritezuivel;
    public Sprite spritedranken;
    public Sprite spritenietvoeding;
    public Sprite spritevis;
    public Sprite spritewijn;
    public Sprite spritesnoep;
    public Sprite spriteontbijt;
    public Sprite spritebaby;
    public Sprite spritekruiden;
    public Sprite spritestandard;
    public Transform contentPanel;
    public SimpleObjectPool categoryObjectPool;
    private WWW www;
    private string databaseUrl = "http://gmanager.herokuapp.com/api/category/list/";
    private string jsonData = "";
    private string[] jsonDataObjects;
    private CategoryData foundCategory;
    private ImageData foundImage;

    // Use this for initialization
    void Start () {
        GetMainCategories();
	}

    //Invoke this function where to want to make request.
    void GetMainCategories()
    {
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataMainCategories(www));
    }

    IEnumerator GetDataMainCategories(WWW www)
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
                    foundCategory = new CategoryData();
                    foundImage = new ImageData();

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectCategory = JSONObject.Parse(jsonDataObjects[i]);

                    switch(jsonObjectCategory["name"].Str)
                    {
                        case "Groenten en fruit":
                            foundImage.Image = spritegroentenfruit;
                            break;
                        case "Conserven en Soepen":
                            foundImage.Image = spritesoep;
                            break;
                        case "Lichaamsverzorging / Parfumerie":
                            foundImage.Image = spriteparfum;
                            break;
                        case "Chips/Borrelhapjes":
                            foundImage.Image = spritechips;
                            break;
                        case "Onderhoud / Huishouden":
                            foundImage.Image = spriteonderhoud;
                            break;
                        case "Colruyt-beenhouwerij":
                            foundImage.Image = spritebeenhouwer;
                            break;
                        case "Diepvries":
                            foundImage.Image = spritediepvries;
                            break;
                        case "Huisdieren":
                            foundImage.Image = spritehuisdieren;
                            break;
                        case "Zuivel":
                            foundImage.Image = spritezuivel;
                            break;
                        case "Dranken":
                            foundImage.Image = spritedranken;
                            break;
                        case "Niet-voeding":
                            foundImage.Image = spritenietvoeding;
                            break;
                        case "Bereidingen/Charcuterie/Vis/Veggie":
                            foundImage.Image = spritevis;
                            break;
                        case "Wijn & Bubbels":
                            foundImage.Image = spritewijn;
                            break;
                        case "Koekjes/Chocolade/Snoep":
                            foundImage.Image = spritesnoep;
                            break;
                        case "Brood/Ontbijt":
                            foundImage.Image = spriteontbijt;
                            break;
                        case "Baby":
                            foundImage.Image = spritebaby;
                            break;
                        case "Kruidenierswaren / Droge voeding":
                            foundImage.Image = spritekruiden;
                            break;
                        default:
                            foundImage.Image = spritestandard;
                            break;
                    }

                    if (Convert.ToInt32(jsonObjectCategory["parent_id"].Number) == 0)
                    {
                        //now we can get the values from json of any attribute.
                        foundCategory.Id = Convert.ToInt32(jsonObjectCategory["id"].Number);
                        foundCategory.Name = jsonObjectCategory["name"].Str;
                        foundCategory.Image = foundImage;

                        GameObject newCategoryListItem = categoryObjectPool.GetObject();
                        newCategoryListItem.transform.SetParent(contentPanel);

                        MainCategoryListItem categoryListItem = newCategoryListItem.GetComponent<MainCategoryListItem>();
                        categoryListItem.ShowCategoryInfo(foundCategory);
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