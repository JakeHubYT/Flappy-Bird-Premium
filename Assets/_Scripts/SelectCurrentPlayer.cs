using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCurrentPlayer : MonoBehaviour
{
    public ShopManager shopManager;

    private void OnEnable()
    {
        Actions.OnSkinChanged += UpdateSkin;
    }
    private void OnDisable()
    {
        Actions.OnSkinChanged -= UpdateSkin;

    }

    void UpdateSkin()
    {
       // Debug.Log("Called");
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        //get current skin
        transform.GetChild(ShopManager.lastEquippedSkinNum).gameObject.SetActive(true);
    }
}
