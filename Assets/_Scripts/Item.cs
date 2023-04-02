using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int price = 32;
    public GameObject shopItem;
    


    int itemIndexInArray;

    public bool isLocked = true;
    public GameObject lockIcon;

    TextMeshProUGUI priceTxt;
    ShopManager shopManager;
    Animator anim;


    private void Awake()
    {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();

        priceTxt = FindChildWithTag("ItemPrice").GetComponent<TextMeshProUGUI>(); 
        priceTxt.text = "$" + price.ToString();
        UpdateLockedState();

        itemIndexInArray = shopManager.GetItemIndexInArray(this.name);


        isLocked = shopManager.CheckLockedStatus(itemIndexInArray);
        
        UpdateLockedState();
      
        anim = GetComponent<Animator>();

    }

    public void UseShopItem()
    {
      
            if (shopManager.AffordItemCheck(price, shopItem, isLocked, itemIndexInArray, anim) != false)
            {

                isLocked = false;
                UpdateLockedState();

            }
        


    }

    void UpdateLockedState()
    {

        if(!isLocked)
        {
            lockIcon.SetActive(false);
        }
    
    }




    Transform FindChildWithTag(string tag)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == tag)
            {
                return child;
            }
          
        }
        return null;
    }
}
