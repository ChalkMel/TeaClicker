using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float fadeSpeed = 2f;

    private TextMeshProUGUI text;
    private float timer = 0f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        Color color = text.color;
        color.a = 1f - (timer * fadeSpeed);
        text.color = color;
    }
}
