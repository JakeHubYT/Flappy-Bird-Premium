using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{


    public static int Money;

    public AudioClip shopMusic;

    public AudioClip buySound;
    public AudioClip errorSound;
    public AudioClip equipSound;



    public GameObject shopParent;

    public GameObject player;
    public GameManager gameManager;
    public static int lastEquippedSkinNum;

    public Item[] itemsInShop;
    public static bool[] isLocked = new bool[100];

    bool playingMusic = false;

 


    public TextMeshProUGUI[] moneyText;

    int index = 0;
    private void OnEnable()
    {
        Actions.OnCollectPoint += AddToMoney;
    }
    private void OnDisable()
    {
        Actions.OnCollectPoint -= AddToMoney;
    }

    private void Awake()
    {
        UpdateMoneyUI();

        Actions.OnSkinChanged();

        DisablePreviousSkins();
        player.transform.GetChild(lastEquippedSkinNum).gameObject.SetActive(true);
    }

    private void Update()
    {
        ResetShopCommand();
        GiveMoneyCommand();

        if(shopParent.activeSelf && !playingMusic)
        {
          
            AudioManager.Instance.PlayMusic(shopMusic, true);
            Debug.Log("Playing Music");

            playingMusic = true;
        }
        else if (!shopParent.activeSelf && playingMusic == true) {
            playingMusic = false;
            AudioManager.Instance.FadeOut(2f);
           
        }
    }

    private void GiveMoneyCommand()
    {
       if(Input.GetKeyDown(KeyCode.M)) 
        {
            Money += 100;
        }
    }

    public bool AffordItemCheck(int price, GameObject skin, bool itemIsLocked, int arrayIndex, Animator itemAnimator)
    {

       

        //anim already unlocked
        if (!itemIsLocked) 
        {
            AudioManager.Instance.PlaySound(equipSound);

            Debug.Log("Already Unlocked, Equipping Skin") ;
            itemAnimator.SetTrigger("Equip");

            EquipSkin(skin);



            Actions.OnSkinChanged();
            return false;  

        
        }

        if(Money >= price)
        {
            Debug.Log("Yes Buy");

            AudioManager.Instance.PlaySound(buySound);

            //anim buy
            itemAnimator.SetTrigger("Buy");

            Money -= price;
            EquipSkin(skin);
            UpdateMoneyUI();
            isLocked[arrayIndex] = true;

            Actions.OnSkinChanged();
            return true;
        }
        else
        {

            AudioManager.Instance.PlaySound(errorSound);

            //anim no buy shake
            itemAnimator.SetTrigger("CantBuy");

            Debug.Log("No Buy");

            Actions.OnSkinChanged();
            return false;
        }
       

    }

    public void EquipSkin(GameObject skin)
    {
        
        //check if skin is under parent
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if(player.transform.GetChild(i).name == skin.name)
            {
                lastEquippedSkinNum = i;

                DisablePreviousSkins();
                skin.SetActive(true);
            }
        }
    }

    private void DisablePreviousSkins()
    {
        for (int i = 0; i < player.transform.childCount; i++)
        {
            player.transform.GetChild(i).gameObject.SetActive(false);
        }
      
    }

    void AddToMoney()
    {
        Money += 1;
        UpdateMoneyUI();
    }

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
