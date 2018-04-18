using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour {

	public GameObject main;
    public GameObject shops;
    public GameObject categories;
	public GameObject market;
	public GameObject cashbox;
	private GameObject changeAr;
	private GameObject link;
	private GameObject _cashbox;
    private GameObject _shops;

    // Use this for initialization
    void Start()
    {
        main.gameObject.SetActive(true);
        market.gameObject.SetActive(false);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = false;
    }

    public void GetShops()
    {
        main.gameObject.SetActive(false);
        shops.gameObject.SetActive(true);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = true;
    }

    // Нажали на иконку магазина, переход в магазин
    public void GetMarket ()
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
		_cashbox = GameObject.Find ("paymentcash");
	}

	// Переход обратно на главный эеран из магазина
	public void BackToMain () 
	{
		market.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(false);
		main.gameObject.SetActive(true);
        _shops = GameObject.Find("shops");
        _shops = shops;
        _shops.GetComponent<Canvas>().enabled = false;
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
	public void BackToCashbox () 
	{
		link = GameObject.FindGameObjectWithTag("manager");
		changeAr = link.GetComponent<ManagerController> ()._changeArea;
		changeAr.gameObject.SetActive (false);
		cashbox = link.GetComponent<ManagerController> ()._cashboxArea;

		//changeAr.GetComponent<Canvas>().enabled = false;
		cashbox.gameObject.SetActive (true);
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
}
