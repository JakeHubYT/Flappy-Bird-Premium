using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{


    public static int Money;

  

    public AudioClip buySound;
    public AudioClip errorSound;
    public AudioClip equipSound;



    public GameObject shopParent;

    public Transform skinParent;
    public GameManager gameManager;
    public static int lastEquippedSkinNum;

    public Item[] itemsInShop;
    public static bool[] isLocked = new bool[100];

    bool playingMusic = false;

 


    public TextMeshProUGUI[] moneyText;

    int index = 0;
    private bool doubleCoins = false;

    private void OnEnable()
    {
        Actions.OnCollectPoint += AddToMoney;

        Actions.OnDoubleCoins += DoubleCoins;
        Actions.OnDoubleCoinsEnd += NormalCoins;
    }
    private void OnDisable()
    {
        Actions.OnCollectPoint -= AddToMoney;

        Actions.OnDoubleCoins -= DoubleCoins;
        Actions.OnDoubleCoinsEnd -= NormalCoins;
    }

    private void Awake()
    {
        UpdateMoneyUI();

        Actions.OnSkinChanged();

        DisablePreviousSkins();
        skinParent.GetChild(lastEquippedSkinNum).gameObject.SetActive(true);
    }

    private void Update()
    {
        ResetShopCommand();

       
    }

    public void GiveMoneyCommand()
    {
            Money += 100;
            UpdateMoneyUI();
    }

    public bool AffordItemCheck(int price, bool itemIsLocked, int arrayIndex, Animator itemAnimator)
    {

       

        //anim already unlocked
        if (!itemIsLocked) 
        {
            AudioManager.Instance.PlaySound(equipSound, 1, true);

            Debug.Log("Already Unlocked, Equipping Skin") ;
            itemAnimator.SetTrigger("Equip");

            Actions.OnSkinChanged();
            return true;  

        
        }

        if(Money >= price)
        {
            Debug.Log("Yes Buy");

            AudioManager.Instance.PlaySound(buySound, 1, true);

            //anim buy
            itemAnimator.SetTrigger("Buy");

            Money -= price;
            UpdateMoneyUI();
            isLocked[arrayIndex] = true;

           
            return true;
        }
        else
        {

            AudioManager.Instance.PlaySound(errorSound, 1, true);

            //anim no buy shake
            itemAnimator.SetTrigger("CantBuy");

            Debug.Log("No Buy");

           
            return false;
        }
       

    }

    public void EquipSkin(GameObject skin)
    {
        
        //check if skin is under parent
        for (int i = 0; i < skinParent.childCount; i++)
        {
            if(skinParent.GetChild(i).name == skin.name)
            {
                lastEquippedSkinNum = i;

                DisablePreviousSkins();
                skin.SetActive(true);
            }
        }
    }

    private void DisablePreviousSkins()
    {
        for (int i = 0; i < skinParent.childCount; i++)
        {
            skinParent.GetChild(i).gameObject.SetActive(false);
        }
      
    }

    #region Add To Coins Logic

    void AddToMoney()
    {
        if(doubleCoins)
        {
            Money += 1;
        }
        Money += 1;
        UpdateMoneyUI();
    }

    public void DoubleCoins()
    {
        doubleCoins = true;
    }
    public void NormalCoins()
    {
        doubleCoins = false;
    }

    #endregion

    void UpdateMoneyUI()
    {
        for (int i = 0; i < moneyText.Length; i++)
        {
            moneyText[i].text = "$" + Money.ToString();
        }

    }

    public int GetItemIndexInArray(string itemName)
    {
        int index = -1;

        foreach (var item in itemsInShop)
        {
            index++;

            if(item.name == itemName)
            {
                return index;
            }
        }
        Debug.LogError("No Matching Item for " + itemName);
        return 1000001;
       


    }

    public bool CheckLockedStatus(int itemIndexInArray)
    {
        return !isLocked[itemIndexInArray];
        
    }

    public void ResetShopCommand()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < isLocked.Length; i++)
            {
                isLocked[i] = false;
            }
                
            
        }
    }
}
