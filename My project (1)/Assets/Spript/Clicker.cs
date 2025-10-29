using TMPro;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    //[SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private int clickValue = 1;

    private void OnMouseDown()
    {
        score += clickValue;
        scoreText.text = score.ToString();
    }
}

