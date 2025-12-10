using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private AudioSource audio;

    void Start()
    {
        playButton.onClick.AddListener(Play);
        exitButton.onClick.AddListener(Exit);
        audio.Play();
    }

    void Play()
    {
        SceneManager.LoadScene("Game");
    }
    void Exit()
    {
        Application.Quit();
    }
}