using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class ShopListItem : MonoBehaviour {

    public Text nameShop;
    public Image iconImage;

    // Use this for initialization
    void Start () {

    }

    public void ShowShopInfo(ShopData shop)
    {
        nameShop.text = shop.Name;
        iconImage.sprite = shop.ShopNetwork.Image.Image;
    }
}
