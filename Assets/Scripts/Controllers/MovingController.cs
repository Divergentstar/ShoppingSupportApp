using Assets.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class MovingController : MonoBehaviour
{
    public GameObject main;
    public GameObject shops;
    public GameObject mainCategories;
    public GameObject subCategories;
    public GameObject items;
    public GameObject market;
    public GameObject cashbox;
    public Text nameShop;
    public Text nameCategory;
    private int shopId = 1;
    private int categoryId = 3;
    private int amountSubcategories = 0;
    private GameObject changeAr;
    private GameObject link;
    private GameObject _cashbox;
    private GameObject _shops;
    private GameObject _mainCategories;
    private GameObject _subCategories;
    private GameObject _items;
    private WWW www;
    private string databaseUrl = "";
    private string jsonData = "";
    private string[] jsonDataObjects;
    private ShopData foundShop;
    private CategoryData foundCategory;
    private ImageData foundImage;

    // Use this for initialization
    void Start()
    {
        main.gameObject.SetActive(true);
        market.gameObject.SetActive(false);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = false;
        _mainCategories = GameObject.Find("mainCategories");
        _mainCategories = mainCategories;
        _mainCategories.GetComponent<Canvas>().enabled = false;
        _subCategories = GameObject.Find("subCategories");
        _subCategories = subCategories;
        _subCategories.GetComponent<Canvas>().enabled = false;
        _items = GameObject.Find("items");
        _items = items;
        _items.GetComponent<Canvas>().enabled = false;
    }

    public int GetShopId()
    {
        return shopId;
    }

    public int GetCategoryId()
    {
        return categoryId;
    }

    public void GetShops()
    {
        main.gameObject.SetActive(false);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = true;
    }

    public void GetMainCategories()
    {
        GetShop();
        Debug.Log(shopId);

        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = false;
        _mainCategories = GameObject.Find("mainCategories");
        _mainCategories = mainCategories;
        _mainCategories.GetComponent<Canvas>().enabled = true;
    }

    public void GetSubcategoriesOrItems()
    {
        GetCategory();
        Debug.Log(categoryId);
        CountSubcategories();
        Debug.Log(amountSubcategories);

        _mainCategories = GameObject.Find("mainCategories");
        _mainCategories = mainCategories;
        _mainCategories.GetComponent<Canvas>().enabled = false;

        if (amountSubcategories > 0)
        {
            GetSubcategories();
        }
        else
        {
            GetItems();
        }
    }

    public void GetSubcategories()
    {
         _subCategories = GameObject.Find("subCategories");
         _subCategories = subCategories;
         _subCategories.GetComponent<Canvas>().enabled = true;
    }

    public void GetItems()
    {
        _subCategories = GameObject.Find("subCategories");
        _subCategories = subCategories;
        _subCategories.GetComponent<Canvas>().enabled = false;
        _items = GameObject.Find("items");
        _items = items;
        _items.GetComponent<Canvas>().enabled = true;
    }

    // Нажали на иконку магазина, переход в магазин
    public void GetMarket()
    {
        main.gameObject.SetActive(false);
        market.gameObject.SetActive(true);
    }

    // Переход к кассе
    public void GetCashbox()
    {
        main.gameObject.SetActive(false);
        market.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(true);
        _cashbox = GameObject.Find("paymentcash");
    }

    // Переход обратно на главный эеран из магазина
    public void BackToMain()
    {
        market.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = false;
    }

    public void BackToShops()
    {
        _mainCategories = GameObject.Find("mainCategories");
        _mainCategories = mainCategories;
        _mainCategories.GetComponent<Canvas>().enabled = false;
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = true;
    }

    public void BackToMainCategories()
    {
        _subCategories = GameObject.Find("subCategories");
        _subCategories = subCategories;
        _subCategories.GetComponent<Canvas>().enabled = false;
        _items = GameObject.Find("items");
        _items = items;
        _items.GetComponent<Canvas>().enabled = false;
        _mainCategories = GameObject.Find("mainCategories");
        _mainCategories = mainCategories;
        _mainCategories.GetComponent<Canvas>().enabled = true;
    }

    // Переход в магазин из кассы
    public void BackToMarket()
    {
        main.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(false);
        market.gameObject.SetActive(true);
    }

    // Переход к сдаче
    public void GetChange()
    {
        cashbox.gameObject.SetActive(false);
        //changeAr = GetComponent<Canvas> ();
        //		_cashbox = GameObject.Find("Plane3");
        //_cashbox = cashbox;
        //Debug.Log ("_cashbox1: " + _cashbox);
        //		_cashbox.GetComponent<Canvas>().enabled = false;

        link = GameObject.FindGameObjectWithTag("manager");
        changeAr = link.GetComponent<ManagerController>()._changeArea;

        //changeAr.GetComponent<Canvas>().enabled = true;
        changeAr.gameObject.SetActive(true);
    }

    // Переход обратно от сдачи в кассу
    public void BackToCashbox()
    {
        link = GameObject.FindGameObjectWithTag("manager");
        changeAr = link.GetComponent<ManagerController>()._changeArea;
        changeAr.gameObject.SetActive(false);
        cashbox = link.GetComponent<ManagerController>()._cashboxArea;

        //changeAr.GetComponent<Canvas>().enabled = false;
        cashbox.gameObject.SetActive(true);
        //Debug.Log ("_cashbox: " + _cashbox);
        //_cashbox.GetComponent<Canvas>().enabled = true;
    }

    public void Restart()
    {
        Application.LoadLevel("ShoppingSupportApp");
    }

    void Update()
    {
        // Выход из игры по нажатию кнопки "Назад" на планшете
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    //Invoke this function where to want to make request.
    void GetShop()
    {
        databaseUrl = "http://gmanager.herokuapp.com/api/shop/list/";
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

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectShop = JSONObject.Parse(jsonDataObjects[i]);

                    if (jsonObjectShop["description"].Str == nameShop.text)
                    {
                        shopId = Convert.ToInt32(jsonObjectShop["id"].Number);
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
    void GetCategory()
    {
        databaseUrl = "http://gmanager.herokuapp.com/api/category/list/";
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataCategories(www));
    }

    IEnumerator GetDataCategories(WWW www)
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

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectCategory = JSONObject.Parse(jsonDataObjects[i]);

                    if (jsonObjectCategory["name"].Str == nameCategory.text)
                    {
                        categoryId = Convert.ToInt32(jsonObjectCategory["id"].Number);
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
    void CountSubcategories()
    {
        amountSubcategories = 0;
        databaseUrl = "http://gmanager.herokuapp.com/api/category/list/";
        //sending the request to url
        www = new WWW(databaseUrl);
        StartCoroutine(GetDataSubcategories(www));
    }

    IEnumerator GetDataSubcategories(WWW www)
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

                    //Data is in json format, we need to parse the Json.
                    JSONObject jsonObjectCategory = JSONObject.Parse(jsonDataObjects[i]);

                    if (Convert.ToInt32(jsonObjectCategory["parent_id"].Number) == categoryId)
                    {
                        amountSubcategories++;
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