using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChangeController : MonoBehaviour
{

    private static float _sumChange;
    public GameObject[] money;
    private GameObject btn;
    private GameObject link;
    private List<float> bestComb;
    private List<float> allMoney;
    private Transform parentChangeArea;
    private GameObject obj;
    private float minPrice;
    private static float sumChangeForLoad;
    private static string sumChangeStr;

    //	void OnEnable ()
    //	{
    ////		GetValueFromScript ();
    //		allMoney = new List<float> (){1f,2f,1f,0.1f,0.1f,0.2f,0.5f,0.1f,0.01f};
    //		MakeAllSets (allMoney);
    //		//if (link.GetComponent<ManagerController> ().countEnterChange <= 1) {
    //			SetChange ();
    //		//}
    //	}

    private void Start()
    {
        _sumChange = 0;
        sumChangeForLoad = 0;
        sumChangeStr = "";
        bestComb = new List<float>();
    }

    // Получение переменных из скрипта менеджера игры
    void GetValueFromScript()
    {
        // Получаем ссылку на кноаку из третьего канваса
        link = GameObject.FindGameObjectWithTag("manager");
        //		if (link.GetComponent<ManagerController> ()._showMoneyBtn != null) {
        //			Debug.Log (link.GetComponent<ManagerController> ()._showMoneyBtn);
        //			btn = link.GetComponent<ManagerController> ()._showMoneyBtn;
        // Получение суммы сдачи

        if (link.GetComponent<ManagerController>()._cashboxArea != null)
        {
            //Debug.Log (link.GetComponent<ManagerController> ()._cashboxArea);
            btn = link.GetComponent<ManagerController>()._cashboxArea;
            _sumChange = btn.GetComponent<GetMoneyCombination>().sumChange;
            sumChangeStr = _sumChange.ToString("0.00");
            sumChangeForLoad = float.Parse(sumChangeStr);
            //Debug.Log(sumChangeForLoad);
        }
        link.GetComponent<ManagerController>().countEnterChange++;
    }

    // Выгрузка комбинации сдачи на экран
    public void SetChange()
    {
        GetValueFromScript();
        if (link.GetComponent<ManagerController>().countEnterChange <= 1 && sumChangeForLoad > 0)
        {
            //allMoney = new List<float>() { 1f, 2f, 1f, 0.1f, 0.1f, 0.2f, 0.5f, 0.1f, 0.01f };

            /*Combination comb = new Combination(sumChangeForLoad);
            comb.MakeAllSets(allMoney);

            bestComb = comb.GetBestSet(); {1f, 0,5f, ...}*/

            allMoney = new List<float>() { 200f, 100f, 50f, 20f, 10f, 5f, 2f, 1f, 0.5f, 0.2f, 0.1f, 0.05f, 0.01f };

            foreach (float coin in allMoney) {
                while (sumChangeForLoad >= coin) {
                    sumChangeForLoad -= coin;
                  
                    if (sumChangeForLoad > 99f)
                    {
                        sumChangeStr = sumChangeForLoad.ToString("000.00");
                    }
                    else if (sumChangeForLoad > 9f) 
                    {
                        sumChangeStr = sumChangeForLoad.ToString("00.00");
                    } 
                    else
                    {
                        sumChangeStr = sumChangeForLoad.ToString("0.00");
                        Debug.Log(sumChangeForLoad);
                    } 
                    sumChangeForLoad = float.Parse(sumChangeStr);

                    bestComb.Add(coin);
                }
            }

            parentChangeArea = GameObject.FindGameObjectWithTag("changeArea").transform;
            if (bestComb != null)
            {
                
                foreach (float item in bestComb)
                {
                    Debug.Log("bestComb item " + item);
                    if (item == 1f)
                    {
                        obj = Instantiate(money[0], money[0].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 2f)
                    {
                        obj = Instantiate(money[1], money[1].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 5f)
                    {
                        obj = Instantiate(money[2], money[2].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 10f)
                    {
                        obj = Instantiate(money[3], money[3].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 20f)
                    {
                        obj = Instantiate(money[4], money[4].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 50f)
                    {
                        obj = Instantiate(money[5], money[5].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 100f)
                    {
                        obj = Instantiate(money[6], money[6].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 200f)
                    {
                        obj = Instantiate(money[7], money[7].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 0.01f)
                    {
                        obj = Instantiate(money[8], money[8].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 0.02f)
                    {
                        obj = Instantiate(money[9], money[9].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 0.05f)
                    {
                        obj = Instantiate(money[10], money[10].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 0.1f)
                    {
                        obj = Instantiate(money[11], money[11].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 0.2f)
                    {
                        obj = Instantiate(money[12], money[12].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                    else if (item == 0.5f)
                    {
                        obj = Instantiate(money[13], money[13].transform.position, Quaternion.identity) as GameObject;
                        obj.transform.parent = parentChangeArea;
                    }
                }
            }
        }
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
    //		if (CalcPrice (money) >= _sumChange) {
    //			bestComb = money;
    //			minPrice = CalcPrice (money);
    //		}
    //	} else {
    //		if (CalcPrice (money) >= _sumChange && CalcPrice (money) < minPrice) {
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
