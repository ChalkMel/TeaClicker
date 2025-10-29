using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private string itemDescription;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private int itemCoef;
    [SerializeField] private int itemCost;
    [SerializeField] private TextMeshProUGUI itemCostText;
    private bool isAcquired;
    [SerializeField] private Button mainButton;

    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject deactive;
    [SerializeField] private GameObject bought;
    [SerializeField] private GameObject notEnough;
    [SerializeField] private Clicker Clicker;
    private void Start()
    {
        Clicker = FindFirstObjectByType<Clicker>();
        itemCostText.text = itemCost.ToString();
        itemNameText.text = itemName.ToString();
        mainButton.onClick.AddListener(OnMouseDown);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);
        itemDescriptionText.text = itemDescription;
        itemNameText.text = itemName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
    public void OnMouseDown()
    {
        if (isAcquired)
            return;

        if (Clicker.score >= itemCost)
        {
            Clicker.score -= itemCost; 
            Clicker.clickValue += itemCoef;
            isAcquired = true;
            StartCoroutine(ShowBoughtMessage());
            deactive.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowNotEnoughMessage());
        }
    }
    private IEnumerator ShowNotEnoughMessage()
    {
        notEnough.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnough.SetActive(false);
    }

    private IEnumerator ShowBoughtMessage()
    {
        bought.SetActive(true);
        yield return new WaitForSeconds(2f);
        bought.SetActive(false);
    }
}
