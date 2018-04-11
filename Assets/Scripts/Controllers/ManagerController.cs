using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour {

	public GameObject _bag;	// кошелек
	public GameObject _inputField;	// поле ввода цены товара
	public GameObject _showMoneyBtn;
	public GameObject _changeArea;	// экран выдачи сдачи
	public GameObject _cashboxArea;	// экран оплаты покупки 
	public int countEnterCashbox;	// счетчик входов на экран оплаты покупки
	public int countEnterChange;	// счетчик входов на экран выдачи сдачи

	void Awake ()
	{
		if (GameObject.Find ("changecash") != null) {
			_changeArea = GameObject.Find ("changecash");
		}

		if (GameObject.Find ("paymentcash") != null) {
			_cashboxArea = GameObject.Find ("paymentcash");
		}

		countEnterCashbox = 0;
		countEnterChange = 0;
	}

	// Use this for initialization
	void Update () {
		if (GameObject.FindGameObjectWithTag ("wallet") != null) { // получаем ссыдку на кошелек
			_bag = GameObject.FindGameObjectWithTag ("wallet");
		}
			
		if (GameObject.FindGameObjectWithTag ("inputField") != null) {	// получаем ссылку на поле ввода
			_inputField = GameObject.FindGameObjectWithTag ("inputField");
		}
	}
}
