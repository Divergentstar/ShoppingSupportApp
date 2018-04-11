using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProducts : MonoBehaviour {
	public GameObject[] beerArrBtn;	// массив элементов с напитками
	private Transform[] parentObj;	// области для размещения объектов
	private GameObject obj;


	// Use this for initialization
	void Start () {
		//Debug.Log ("start");
		parentObj = new Transform[3];
		parentObj[0] = GameObject.Find("Product1").transform;
		parentObj[1] = GameObject.Find("Product2").transform;
		parentObj[2] = GameObject.Find("Product3").transform;

		for (int i = 0; i < beerArrBtn.Length; i++) {
			int index = Random.Range (0, 6);	// индекс выгружаемого объекта 
			obj = Instantiate (beerArrBtn[0], beerArrBtn [0].transform.position, Quaternion.identity) as GameObject;
			obj.transform.parent = parentObj[i];
		}
	}
}
