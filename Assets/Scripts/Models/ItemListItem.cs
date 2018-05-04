using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

public class ItemListItem : MonoBehaviour {

    public Text nameItem;
    public Text priceItem;
    public Image iconImage;

    // Use this for initialization
    void Start()
    {

    }

    public void ShowItemInfo(ItemInShopData itemInShop)
    {
        string price = itemInShop.Price + "";

        nameItem.text = itemInShop.Item.Id + " - " + itemInShop.Item.Name;
        priceItem.text = "€ " + price.Replace(".", ",");
        iconImage.sprite = itemInShop.Item.Image.Image;
    }
}
