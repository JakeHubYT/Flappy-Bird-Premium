using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int price = 32;
    bool used = false;

    public GameObject shopItem;
    public Ability ability;


    int itemIndexInArray;

    public bool isLocked = true;
    public GameObject lockIcon;

    TextMeshProUGUI priceTxt;
    ShopManager shopManager;
    
   public Animator anim;


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
    private void Update()
    {
        if(price <= 0 && used == false)
        {
            isLocked = false;
            used = true;
        }
    }
    public void UseShopItem()
    {

        if (shopItem == null)
        {
            if (shopManager.AffordItemCheck(price, isLocked, itemIndexInArray, anim) != false)
            {
                isLocked = false;

                AbilityManager.Instance.EquipAbility(ability);
                UpdateLockedState();

            }
            
        }
        else
        {
            if (shopManager.AffordItemCheck(price, isLocked, itemIndexInArray, anim) != false)
            {

                isLocked = false;

                shopManager.EquipSkin(shopItem);
                Actions.OnSkinChanged();
                UpdateLockedState();

            }
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
