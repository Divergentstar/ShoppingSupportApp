using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

public class SubCategoryListItem : MonoBehaviour
{
    public Text nameCategory;
    public Image iconImage;

    // Use this for initialization
    void Start()
    {

    }

    public void ShowCategoryInfo(CategoryData category)
    {
        nameCategory.text = category.Name;
        //iconImage.sprite = category.Image.Image;
    }
}