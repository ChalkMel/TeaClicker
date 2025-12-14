using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int clickValue = 1;
    [SerializeField] ParticleSystem particles;
    public List<GameObject> ingEnable = new List<GameObject>();
    [SerializeField] private AudioSource audio;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float textLifeTime = 3f;
    
    [SerializeField] private float cupScaleUp = 1.15f;
    [SerializeField] private float animationSpeed = 0.15f;

    public bool disable;
    private Vector3 originalScale;
    private bool isAnimating = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (!disable)
        {
            score += clickValue;
            scoreText.text = score.ToString();
            particles.Play();
            ShowFloatingText(clickValue);

            if (!isAnimating)
            {
                StartCoroutine(AnimateCup());
            }
        }
        else
            return;
    }
    private void ShowFloatingText(int valueToShow)
    {
        GameObject floatingText = Instantiate(floatingTextPrefab, canvas.transform);

        TextMeshProUGUI textComponent = floatingText.GetComponent<TextMeshProUGUI>();
        textComponent.text = "+" + valueToShow.ToString(); // <-- “еперь показываем переданное значение

        RectTransform rectTransform = floatingText.GetComponent<RectTransform>();
        Vector3 mousePosition = Input.mousePosition;
        rectTransform.position = mousePosition;

        StartCoroutine(MoveFloatingText(floatingText));
        Destroy(floatingText, textLifeTime);
    }

    private IEnumerator MoveFloatingText(GameObject floatingText)
    {
        float timer = 0f;
        Vector3 startPosition = floatingText.transform.position;

        float moveUpAmount = 100f;
        float moveSideAmount = Random.Range(-50f, 50f);

        while (timer < textLifeTime)
        {
            float progress = timer / textLifeTime;

            float newX = startPosition.x + moveSideAmount * progress;
            float newY = startPosition.y + moveUpAmount * progress;

            floatingText.transform.position = new Vector3(newX, newY, startPosition.z);

            floatingText.transform.localScale = Vector3.one * (1f + progress * 0.5f);

            timer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator AnimateCup()
    {
        isAnimating = true;
        audio.Play();
        float timer = 0f;
        while (timer < animationSpeed)
        {
            float progress = timer / animationSpeed;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * cupScaleUp, progress);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        isAnimating = false;
    }
}