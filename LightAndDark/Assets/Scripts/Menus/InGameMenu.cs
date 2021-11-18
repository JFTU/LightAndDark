using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class InGameMenu : MonoBehaviour
{
    public Image background;
    public Button exitButton;
    public Text exitText;
    public Button resumeButton;
    public Text resumeText;
    public Text waitingForPlayers;
    public GameObject gameStart;
    public GameObject gameManager;

    private void Update()
    {
        bool gameEnd = gameManager.GetComponent<CounterAndTimer>().gameEnd;

        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnd)
        {
            if (!gameStart.GetComponent<GameStart>().startGame)
            {
                waitingForPlayers.GetComponent<Text>().enabled = false;
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            background.GetComponent<Image>().enabled = true;
            exitButton.GetComponent<Image>().enabled = true;
            resumeButton.GetComponent<Image>().enabled = true;
            resumeText.GetComponent<Text>().enabled = true;
            exitText.GetComponent<Text>().enabled = true;
        }

        if (!background.GetComponent<Image>().enabled && !gameStart.GetComponent<GameStart>().startGame)
        {
            waitingForPlayers.GetComponent <Text>().enabled = true;
        }

        if (gameStart.GetComponent<GameStart>().startGame)
        {
            waitingForPlayers.GetComponent<Text>().enabled = false;
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        background.GetComponent<Image>().enabled = false;
        exitButton.GetComponent<Image>().enabled = false;
        resumeButton.GetComponent<Image>().enabled = false;
        resumeText.GetComponent<Text>().enabled = false;
        exitText.GetComponent<Text>().enabled = false;
    }

    public void Exit()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.Disconnect();
        SceneManager.UnloadSceneAsync("Game");
    }
}
