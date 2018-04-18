using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour {

	public GameObject main;
	public GameObject shops;
    public GameObject categories;
    public GameObject items;
	public GameObject cashbox;
	private GameObject changeAr;
	private GameObject link;
	private GameObject _cashbox;

    // Use this for initialization
    void Start()
    {
        main.gameObject.SetActive(true);
        shops.gameObject.SetActive(false);
        //categories.gameObject.SetActive(false);
        //items.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(false);
    }

    public void GetShops()
    {
        main.gameObject.SetActive(false);
        shops.gameObject.SetActive(true);
        //categories.gameObject.SetActive(false);
        //items.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(false);
    }

    // Нажали на иконку магазина, переход в магазин
    public void GetMarket ()
	{
        main.gameObject.SetActive(false);
        shops.gameObject.SetActive(false);
        //categories.gameObject.SetActive(false);
        //items.gameObject.SetActive(true);
        cashbox.gameObject.SetActive(false);
    }

	// Переход к кассе
	public void GetCashbox() 
	{
        main.gameObject.SetActive(false);
        shops.gameObject.SetActive(false);
        //categories.gameObject.SetActive(false);
        //items.gameObject.SetActive(false);
        cashbox.gameObject.SetActive (true);
		_cashbox = GameObject.Find ("paymentcash");
	}

	// Переход обратно на главный эеран из магазина
	public void BackToMain () 
	{
        main.gameObject.SetActive(true);
        shops.gameObject.SetActive(false);
        //categories.gameObject.SetActive(false);
        //items.gameObject.SetActive(false);
        cashbox.gameObject.SetActive(false);
    }

	// Переход в магазин из кассы
	public void BackToMarket() 
	{
        main.gameObject.SetActive(false);
        shops.gameObject.SetActive(false);
        //categories.gameObject.SetActive(false);
        //items.gameObject.SetActive(true);
        cashbox.gameObject.SetActive(false);
    }

	// Переход к сдаче
	public void GetChange() 
	{
		cashbox.gameObject.SetActive (false);
		//changeAr = GetComponent<Canvas> ();
//		_cashbox = GameObject.Find("Plane3");
		//_cashbox = cashbox;
		//Debug.Log ("_cashbox1: " + _cashbox);
//		_cashbox.GetComponent<Canvas>().enabled = false;

		link = GameObject.FindGameObjectWithTag ("manager");
		changeAr = link.GetComponent<ManagerController> ()._changeArea;

		//changeAr.GetComponent<Canvas>().enabled = true;
		changeAr.gameObject.SetActive (true);
	}

	// Переход обратно от сдачи в кассу
	public void BackToCashbox () 
	{
		link = GameObject.FindGameObjectWithTag ("manager");
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
        Application.LoadLevel("Main");
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
