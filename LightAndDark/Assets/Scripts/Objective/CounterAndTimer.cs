using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CounterAndTimer : MonoBehaviour
{
    public int sunCounter = 0;
    public int moonCounter = 0;
    public Text sunScore;
    public Text moonScore;
    public Text timer;
    public float time;
    private PhotonView photonView;
    public GameObject gameStart;

    public Text winText;
    public Image background;
    public Button exitButton;
    public Text exitText;
    public bool gameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        time = 120;

        photonView = gameObject.GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void Update()
    {
        sunScore.text = "Sun: " + sunCounter.ToString();
        moonScore.text = "Moon: " + moonCounter.ToString();
        if (gameStart.GetComponent<GameStart>().startGame)
        {
            time -= Time.deltaTime;
            photonView.RPC("TimerSet", RpcTarget.All);
        }
    }

    [PunRPC]
    public void TimerSet()
    {
        if (time > 0)
        {
            
            timer.text = "Time Left: " + Mathf.FloorToInt(time).ToString();
        }
        else
        {
            timer.text = "Times up!";
            Cursor.visible = true;
            gameEnd = true;

            if (sunCounter > moonCounter)
            {
                winText.text = "Sun Wins!";

            }
            else if (moonCounter > sunCounter)
            {
                winText.text = "Moon Wins";
            }
            else
            {
                winText.text = "Its a Draw!";
            }

            winText.enabled = true;
            background.enabled = true;
            exitButton.GetComponent<Image>().enabled = true;
            exitText.GetComponent<Text>().enabled = true;
        }
    }
}
