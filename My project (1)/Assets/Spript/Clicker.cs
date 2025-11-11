using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    //[SerializeField] private GameObject floatingTextPrefab;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int clickValue = 1;
    public List<GameObject> ingEnable = new List<GameObject>();

    private void OnMouseDown()
    {
        score += clickValue;
        scoreText.text = score.ToString();
    }
}

