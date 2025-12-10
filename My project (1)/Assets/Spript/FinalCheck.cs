using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalCheck : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Button replayBut;
    [SerializeField] private Button menuBut;
    [SerializeField] private Button ending;
    [SerializeField] private Slot cup;
    [SerializeField] private Slot ing;

    void Start()
    {
        ending.onClick.AddListener(Win);
    }

    void Update()
    {
        if(cup.isAcquired && ing.isAcquired)
        {
            button.SetActive(true);
        }
    }
    private void Win()
    {
        winScreen.SetActive(true);
        replayBut.onClick.AddListener(Replay);
        menuBut.onClick.AddListener(Menu);
    }
    private void Replay()
    {
        SceneManager.LoadScene("Game");
    }
    private void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
