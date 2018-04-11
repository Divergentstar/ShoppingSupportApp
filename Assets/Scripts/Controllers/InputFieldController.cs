using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour {

	public float buySum;	// сумма покупки
	public Text curCostTxt;	// отраженная стоимость товара
	public Text txt;	// поле вывода общей суммы покупки
	public static float curCost = 20f;	// стоимость выбранного товара   
	private float inputCost;	// введенная стоимость товара
	public GameObject inputField;
	public GameObject mess;
	public GameObject[] beerArr;	// массив элементов с напитками
	private Transform parentObj;	// области для размещения объектов
	private GameObject obj;
	static int productID = 0;	// id выбранного продукта

    public void GetInputString(string inputText)
	{
		inputCost = float.Parse (inputText);
		//Debug.Log ("inputCost " + inputCost);
		TestCost ();
	}

	public void onClickProduct1 ()
	{
		productID = 0;
		curCost = float.Parse(curCostTxt.text);
		//Debug.Log ("curCost " + curCost);

		inputField.gameObject.SetActive (true);
		mess.gameObject.SetActive (true);
     }

	public void onClickProduct2 ()
	{
		productID = 1;
		curCost = float.Parse(curCostTxt.text);
		//Debug.Log ("curCost " + curCost);

		inputField.gameObject.SetActive (true);
		mess.gameObject.SetActive (true);
    }

	public void onClickProduct3 ()
	{
		productID = 2;
		curCost = float.Parse(curCostTxt.text);
		//Debug.Log ("curCost " + curCost);

		inputField.gameObject.SetActive (true);
		mess.gameObject.SetActive (true);
    }

	public void onClickProduct4 ()
	{
		productID = 3;
		curCost = float.Parse(curCostTxt.text);
		//Debug.Log ("curCost " + curCost);

		inputField.gameObject.SetActive (true);
		mess.gameObject.SetActive (true);
    }

	public void onClickProduct5 ()
	{
		productID = 4;
		curCost = float.Parse(curCostTxt.text);
		//Debug.Log ("curCost " + curCost);

		inputField.gameObject.SetActive (true);
		mess.gameObject.SetActive (true);
    }

	public void onClickProduct6 ()
	{
		productID = 5;
		curCost = float.Parse(curCostTxt.text);
		//Debug.Log ("curCost " + curCost);

		inputField.gameObject.SetActive (true);
		mess.gameObject.SetActive (true);
    }

	public void TestCost()
	{
		//Debug.Log ("inputCost test " + inputCost);
		//Debug.Log ("curCost test " + curCost);
		if (curCost == inputCost) {	// стоимость товара введена правльно, считаем сумму всей покупки
			buySum += curCost;
			txt.text = "Summ is " + buySum.ToString ();
            txt.color = new Color(0, 0, 0);

            // Кладем продукт в корзину
            parentObj = GameObject.Find("Basket").transform;
			obj = Instantiate (beerArr[productID], beerArr [productID].transform.position, Quaternion.identity) as GameObject;
			obj.transform.parent = parentObj;
		} else {
			txt.text = curCost.ToString();
            txt.color = new Color(255, 0, 0);
        }
	}
}
