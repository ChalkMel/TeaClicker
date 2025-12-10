using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Sprite itemIconSprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private string itemDescriptionAfter;
    [SerializeField] private TextMeshProUGUI itemNameTextDesc;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private int itemCoef;
    [SerializeField] private TextMeshProUGUI itemCoefText;
    [SerializeField] private int itemCost;
    [SerializeField] private TextMeshProUGUI itemCostText;
    public bool isAcquired; 
    private bool isOn;
    [SerializeField] private Button mainButton;
    [SerializeField] private AudioSource audio;

    [SerializeField] private GameObject eventScreen;
    [SerializeField] private bool isEvent;

    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject deactive;
    [SerializeField] private GameObject onOff;
    [SerializeField] private GameObject bought;
    [SerializeField] private GameObject notEnough;
    [SerializeField] private Clicker Clicker;

    [SerializeField] private bool cup;
    [SerializeField] private Sprite newCup;
    [SerializeField] private Sprite initCup;
    [SerializeField] private SpriteRenderer render;
    private void Start()
    {
        Clicker = FindFirstObjectByType<Clicker>();
        if (cup)
            initCup = render.sprite;
        itemCostText.text = itemCost.ToString();
        itemNameText.text = itemName.ToString();
        itemIcon.sprite = itemIconSprite;
        mainButton.onClick.AddListener(OnMouseDown);
        itemDescriptionText = descriptionPanel.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        itemNameTextDesc = descriptionPanel.transform.Find("DescriptionTextName").GetComponent<TextMeshProUGUI>();
        itemCoefText = descriptionPanel.transform.Find("DescriptionCoef").GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);
        if (!isAcquired)
        {
            itemDescriptionText.text = $"\"{itemDescription}\"";
            itemNameTextDesc.text = itemName;
            itemCoefText.text = $"You will get +{itemCoef}";
        }
        else
        {
            itemDescriptionText.text = itemDescriptionAfter;
            itemNameTextDesc.text = itemName;
            itemCoefText.text = $"You got +{itemCoef}";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
    public void OnMouseDown()
    {
        if (isAcquired)
        {
            if (isOn && !cup)
            {
                for (int i = 0; i < Clicker.ingEnable.Count; i++)
                {
                    if (itemName == Clicker.ingEnable[i].name)
                    {
                        Clicker.ingEnable[i].SetActive(false);
                        isOn = false;
                        onOff.SetActive(isOn);
                    }
                }
                audio.Play();
                return;
            }
            else if (!isOn && !cup)
            {
                for (int i = 0; i < Clicker.ingEnable.Count; i++)
                {
                    if (itemName == Clicker.ingEnable[i].name)
                    {
                        Clicker.ingEnable[i].SetActive(true);
                        isOn=true;
                        onOff.SetActive(isOn);
                    }
                }
                audio.Play();
                return;
            }
            else if (cup) 
            {
                render.sprite = newCup;
                isOn = true;
                audio.Play();
                return;
                
            }
        }
           

        if (Clicker.score >= itemCost && !cup)
        {
            Clicker.score -= itemCost;
            Clicker.scoreText.text = Clicker.score.ToString();
            Clicker.clickValue += itemCoef;
            isAcquired = true;
            StartCoroutine(ShowBoughtMessage());
            deactive.SetActive(true);
            audio.Play();
            for (int i = 0; i < Clicker.ingEnable.Count; i++)
            {
                if (itemName == Clicker.ingEnable[i].name)
                {
                    Clicker.ingEnable[i].SetActive(true);
                    isOn = true;
                }
            }
            if(isEvent)
                eventScreen.SetActive(true);
        }
        else if (Clicker.score >= itemCost && cup)
            {
                Clicker.score -= itemCost;
                Clicker.scoreText.text = Clicker.score.ToString();
                Clicker.clickValue += itemCoef;
                isAcquired = true;
                StartCoroutine(ShowBoughtMessage());
                deactive.SetActive(true);
                onOff.SetActive(false);
                render.sprite = newCup;
                audio.Play();
            if (isEvent)
                eventScreen.SetActive(true);
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
