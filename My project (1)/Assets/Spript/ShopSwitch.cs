using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopSwitch : MonoBehaviour
{
    [SerializeField] private GameObject cupShop;
    [SerializeField] private GameObject ingShop;
    [SerializeField] private Button cupShopButton;
    [SerializeField] private Button ingShopButton;

    private void Start()
    {
        cupShopButton.onClick.AddListener(SwitchToCupShop);
        ingShopButton.onClick.AddListener(SwitchToIngShop);
    }
    void SwitchToCupShop()
    {
        cupShop.transform.SetSiblingIndex(1);
        ingShop.transform.SetSiblingIndex(0);
        cupShopButton.transform.SetSiblingIndex(4);
        ingShopButton.transform.SetSiblingIndex(0);
    }
    void SwitchToIngShop()
    {
        ingShop.transform.SetSiblingIndex(1);
        cupShop.transform.SetSiblingIndex(0);
        cupShopButton.transform.SetSiblingIndex(0);
        ingShopButton.transform.SetSiblingIndex(4);
    }
}
