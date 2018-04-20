using Assets.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class MovingController : MonoBehaviour
{

    public int shopId = 1;
    public GameObject main;
    public GameObject shops;
    public GameObject categories;
    public GameObject market;
    public GameObject cashbox;
    public Text nameShop;
    private GameObject changeAr;
    private GameObject link;
    private GameObject _cashbox;
    private GameObject _shops;
    private GameObject _categories;
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
        _categories = GameObject.Find("categories");
        _categories = categories;
        _categories.GetComponent<Canvas>().enabled = false;
    }

    public void GetShops()
    {
        main.gameObject.SetActive(false);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = true;
    }

    public void GetCategories()
    {
        GetShop();
        Debug.Log(shopId);
        main.gameObject.SetActive(false);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = false;
        _categories = GameObject.Find("categories");
        _categories = categories;
        _categories.GetComponent<Canvas>().enabled = true;
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
        _categories = GameObject.Find("categories");
        _categories = categories;
        _categories.GetComponent<Canvas>().enabled = false;
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = true;
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
}