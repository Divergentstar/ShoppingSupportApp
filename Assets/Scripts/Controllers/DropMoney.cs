using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMoney : MonoBehaviour, IDropHandler {

	public Text countText;
	public List <float> listMoney;	// список для хранения сложенных денег номиналам в евро в кошелек  

	public void OnDrop (PointerEventData eventData) {
		//Debug.Log ("On drop");
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();

        if (listMoney.Count > 9)
        {
            d = null;
        }

        if (d != null) {
			d.parentToReturnTo = this.transform;
            
			if (d.parentToReturnTo == GameObject.Find("Bag").transform) {	// проверка области, в которую попала монета, если это кошелек ...  
				if (eventData.pointerDrag.gameObject.CompareTag ("oneCent")) {
					listMoney.Add (0.01f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("twoCent")) {
					listMoney.Add (0.02f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("fiveCent")) {
					listMoney.Add (0.05f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("tenCent")) {
					listMoney.Add (0.1f);;
				} else if (eventData.pointerDrag.gameObject.CompareTag ("twentyCent")) {
					listMoney.Add (0.2f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("fiftyCent")) {
					listMoney.Add (0.5f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("oneEuro")) {
					listMoney.Add (1f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("twoEuro")) {
					listMoney.Add (2f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("fiveEuro")) {
					listMoney.Add (5f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("tenEuro")) {
					listMoney.Add (10f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("twentyEuro")) {
					listMoney.Add (20f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("fiftyEuro")) {
					listMoney.Add (50f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("100Euro")) {
					listMoney.Add (100f);
				} else if (eventData.pointerDrag.gameObject.CompareTag ("200Euro")) {
					listMoney.Add (200f);
				}
				// делаем монетки невидимыми в кошельке
				//eventData.pointerDrag.gameObject.SetActive(false);

				// выводим сумму денег в кошельке
//				countText.text = "Summ: " + count.ToString();
			}
		}
	}
}
