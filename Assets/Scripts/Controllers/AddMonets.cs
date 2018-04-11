using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMonets : MonoBehaviour {

	public GameObject[] money;
	private GameObject obj; 
	private GameObject _listMoney; 
	private Transform[] parentObj;

	void Start() {
		GameObject.Find ("paymentcash").SetActive( false);
		GameObject.Find ("changecash").SetActive( false);
		Add ();
	}

	public void Add() {
		parentObj = new Transform[14];
		parentObj[0] = GameObject.Find("fiveCentArea").transform;
		parentObj[1] = GameObject.Find("twoCentArea").transform;
		parentObj[2] = GameObject.Find("oneCentArea").transform;
		parentObj[3] = GameObject.Find("fiveEuroArea").transform;
		parentObj[4] = GameObject.Find("twoEuroArea").transform;
		parentObj[5] = GameObject.Find("oneEuroArea").transform;
		parentObj[6] = GameObject.Find("tenCentArea").transform;
		parentObj[7] = GameObject.Find("tenEuroArea").transform;
		parentObj[8] = GameObject.Find("twentyCentArea").transform;
		parentObj[9] = GameObject.Find("twentyEuroArea").transform;
		parentObj[10] = GameObject.Find("fiftyCentArea").transform;
		parentObj[11] = GameObject.Find("fiftyEuroArea").transform;
		parentObj[12] = GameObject.Find("100EuroArea").transform;
		parentObj[13] = GameObject.Find("200EuroArea").transform;

		for (int i = 0; i < money.Length; i++) {
			for (int j = 0; j < 8; j++) {
				obj = Instantiate (money[i], money [i].transform.position, Quaternion.identity) as GameObject;
				obj.transform.parent = parentObj[i];
			}
		}
	}
}
