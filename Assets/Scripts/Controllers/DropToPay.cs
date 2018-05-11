using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DropToPay : MonoBehaviour, IDropHandler {
   
    private Vector3 newPosition;    // позиция только что положенных денег, которые совпали по номиналу

    // Перенос денег при оплате покупки
    public void OnDrop (PointerEventData eventData) 
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();

		if (d != null) {
			d.parentToReturnTo = this.transform;
			if (d.parentToReturnTo.gameObject.tag == d.gameObject.tag && newPosition != d.parentToReturnTo.gameObject.transform.position) {    // в тегах лежит номинал денег
                eventData.pointerDrag.gameObject.transform.position = d.parentToReturnTo.gameObject.transform.position;
                newPosition = d.parentToReturnTo.gameObject.transform.position;
                d.enabled = false;  // отключение возможности перетаскивать текущий элемент 
            } else {
				d.parentToReturnTo =  GameObject.FindGameObjectWithTag("walletMoneyArea").transform;
			}
		}
	}
}
