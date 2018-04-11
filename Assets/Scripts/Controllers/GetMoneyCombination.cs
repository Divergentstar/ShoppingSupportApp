using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoneyCombination : MonoBehaviour
{

	private List<float> _listMoney;
	private List<int> _listCent;
	private static float _buySum;
	private GameObject link;
	private GameObject wallet;
	private GameObject linkInputField;
	private GameObject inputField;

	private List<float> bestComb;
	private static float minPrice;
	private static float sum = 0f;

	private Transform parentCombArea;
	private Transform parentWalletArea;
	private GameObject obj;
	public GameObject[] moneyComb;
	public GameObject[] money;
	public float sumChange;

	void OnEnable ()
	{
		GetValueFromScript ();	// получаем переменные
        Combination comb = new Combination(_buySum);
        comb.MakeAllSets(_listMoney);
        bestComb = comb.GetBestSet();
       // MakeAllSets (_listMoney);	// получаем комбинацию из денег для оплаты
		if (link.GetComponent<ManagerController> ().countEnterCashbox <= 1) {
			SetMonCombination ();
			SetWalletMoney ();
		}
	}

	public void GetSumChange ()
	{
        sum = 0;

		foreach (float item in bestComb) {
			sum += item;	// сумма денег в найденной комбинации
		}
		sumChange = sum - _buySum;	// сдача
		//Debug.Log("sumChange " + sumChange);
	}

	// Выгрузка подобранной комбинации денег
	public void SetMonCombination ()
	{
		parentCombArea = GameObject.FindGameObjectWithTag ("moneyCombArea").transform;

		if (bestComb != null) {	// выгружаем комбинации
			//Debug.Log("Выгружаем евро");
			foreach (float item in bestComb) {
				//Debug.Log (item);
				//Debug.Log (parentCombArea);
				if (item == 1f) {
					obj = Instantiate (moneyComb [0], moneyComb [0].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 2f) {
					obj = Instantiate (moneyComb [1], moneyComb [1].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 5f) {
					obj = Instantiate (moneyComb [2], moneyComb [2].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 10f) {
					obj = Instantiate (moneyComb [3], moneyComb [3].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 20f) {
					obj = Instantiate (moneyComb [4], moneyComb [4].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 50f) {
					obj = Instantiate (moneyComb [5], moneyComb [5].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 100f) {
					obj = Instantiate (moneyComb [6], moneyComb [6].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 200f) {
					obj = Instantiate (moneyComb [7], moneyComb [7].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 0.01f) {
					obj = Instantiate (moneyComb [8], moneyComb [8].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 0.02f) {
					obj = Instantiate (moneyComb [9], moneyComb [9].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 0.05f) {
					obj = Instantiate (moneyComb [10], moneyComb [10].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 0.1f) {
					obj = Instantiate (moneyComb [11], moneyComb [11].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 0.2f) {
					obj = Instantiate (moneyComb [12], moneyComb [12].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				} else if (item == 0.5f) {
					obj = Instantiate (moneyComb [13], moneyComb [13].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentCombArea;
				}
			}
		}

		GetSumChange ();
	}

	// Выгрузка всех денег из кошелька
	public void SetWalletMoney ()
	{
		parentWalletArea = GameObject.FindGameObjectWithTag ("walletMoneyArea").transform;

		if (_listMoney != null) {	// выгружаем комбинации из евро
			//Debug.Log ("Выгружаем деьги из кошелька");
			foreach (float item in _listMoney) {
				//Debug.Log (item);
				//Debug.Log (parentWalletArea);
				// евро
				if (item == 1f) {	
					obj = Instantiate (money [0], money [0].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 2f) {
					obj = Instantiate (money [1], money [1].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 5f) {
					obj = Instantiate (money [2], money [2].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 10f) {
					obj = Instantiate (money [3], money [3].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 20f) {
					obj = Instantiate (money [4], money [4].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 50f) {
					obj = Instantiate (money [5], money [5].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 100f) {
					obj = Instantiate (money [6], money [6].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 200f) {
					obj = Instantiate (money [7], money [7].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				// центы
				} else if (item == 0.01f) {	
					obj = Instantiate (money [8], money [8].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 0.02f) {
					obj = Instantiate (money [9], money [9].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 0.05f) {
					obj = Instantiate (money [10], money [10].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 0.1f) {
					obj = Instantiate (money [11], money [11].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 0.2f) {
					obj = Instantiate (money [12], money [12].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				} else if (item == 0.5f) {
					obj = Instantiate (money [13], money [13].transform.position, Quaternion.identity) as GameObject;
					obj.transform.parent = parentWalletArea;
				}
			}
		}
	}

	// Получение переменных из других скриптов
	void GetValueFromScript ()
	{
		// Получаем ссылку на кошелек из первого канваса
		link = GameObject.FindGameObjectWithTag ("manager");
		if (link.GetComponent<ManagerController> ()._bag != null) {
			//Debug.Log (link.GetComponent<ManagerController> ()._bag);
			wallet = link.GetComponent<ManagerController> ()._bag;
			// Получение списков с сохраненными деньгами в кошелек из скрипта DropMoney
			_listMoney = new List<float> (wallet.GetComponent<DropMoney> ().listMoney);
			foreach (float i in _listMoney) {
				//Debug.Log ("item: " + i);
			}
		}

		// Получаем ссыдку на поле ввода из второго канваса
		if (link.GetComponent<ManagerController> ()._inputField != null) {
			//Debug.Log (link.GetComponent<ManagerController> ()._inputField);
			inputField = link.GetComponent<ManagerController> ()._inputField;
			// Получение суммы всей покупки из скрипта InputFieldController
			_buySum = inputField.GetComponent<InputFieldController> ().buySum;
			//Debug.Log ("_buySum " + _buySum);
		}

		link.GetComponent<ManagerController> ().countEnterCashbox++;	// +1 вход
	}

	///* Получеам сумму из набора
	// * param[in] List<int> money - набор денег
	// * return sumPrice - сумма
	//*/
	//float CalcPrice (List<float> money)
	//{
	//	float sumPrice = 0;
	//	foreach (float i in money) {
	//		sumPrice += i;
	//	}
	//	return sumPrice;
	//}

	///* Проверяем набор на наименьшую разницу с требуемой ценой
	// * param[in] List<int> money - набор денег
	// * param[in] measure - модификатор, задающий номинал набора (0 - евро, 1 - центы)
	//*/
	//void CheckSet (List<float> money)
	//{
	//	if (bestComb == null) {
	//		if (CalcPrice (money) >= _buySum) {
	//			bestComb = money;
	//			minPrice = CalcPrice (money);
	//		}
	//	} else {
	//		if (CalcPrice (money) >= _buySum && CalcPrice (money) < minPrice) {
	//			bestComb = money;
	//			minPrice = CalcPrice (money);
	//		}
	//	}
	//}

	///* Получение лучшей комбинации 
	// * param[in] List<float> money - набор денег
	// * param[in] measure - модификатор, задающий номинал набора (0 - евро, 1 - центы)
	//*/
	//void MakeAllSets (List<float> money)
	//{
	//	if (money.Count > 0) {
	//		CheckSet (money);
	//	}
			
	//	for (int i = 0; i < money.Count; i++) {
	//		List<float> newSet = new List<float> (money);
	//		newSet.RemoveAt (i);
	//		MakeAllSets (newSet);
	//	}
	//}
}
