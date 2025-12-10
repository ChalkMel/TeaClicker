using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class CupSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private string itemDescription;
    [SerializeField] private TextMeshProUGUI itemNameTextDesc;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private int itemCoef;
    [SerializeField] private TextMeshProUGUI itemCoefText;
    [SerializeField] private int itemCost;
    [SerializeField] private TextMeshProUGUI itemCostText;
    private bool isAcquired;
    [SerializeField] private Button mainButton;

    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject deactive;
    [SerializeField] private GameObject bought;
    [SerializeField] private GameObject notEnough;
    [SerializeField] private Clicker Clicker;
    [SerializeField] private Sprite newCup;
    [SerializeField] private SpriteRenderer render;
    private void Start()
    {
        Clicker = FindFirstObjectByType<Clicker>();
        render = FindFirstObjectByType<SpriteRenderer>();
        itemCostText.text = itemCost.ToString();
        itemNameText.text = itemName.ToString();
        mainButton.onClick.AddListener(OnMouseDown);
        itemDescriptionText = descriptionPanel.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        itemNameTextDesc = descriptionPanel.transform.Find("DescriptionTextName").GetComponent<TextMeshProUGUI>();
        itemCoefText = descriptionPanel.transform.Find("DescriptionCoef").GetComponent<TextMeshProUGUI>();

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);

        itemDescriptionText.text = itemDescription;
        itemNameTextDesc.text = itemName;
        itemCoefText.text = $"You will get +{itemCoef}";
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
            Clicker.scoreText.text = Clicker.score.ToString();
            Clicker.clickValue += itemCoef;
            isAcquired = true;
            StartCoroutine(ShowBoughtMessage());
            deactive.SetActive(true);
            render.sprite = newCup;
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
