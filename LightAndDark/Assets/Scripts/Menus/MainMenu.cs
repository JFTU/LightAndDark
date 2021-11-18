using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.GetComponent<Button>().onClick.AddListener(Play);
        exitButton.GetComponent<Button>().onClick.AddListener(Exit);
    }

    void Play()
    {
        SceneManager.LoadScene("Loading");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    void Exit()
    {
        Application.Quit();
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
