using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerDeath : MonoBehaviour
{
    private GameObject gameManager;
    private Text deathText;
    private float? timeOfDeath = null;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        playerAnimator = gameObject.GetComponent<Animator>();
        deathText = GameObject.Find("YouDied").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (transform.position.y < -10)
            {
                Death();
            }

            if (timeOfDeath != null && timeOfDeath <= gameManager.GetComponent<CounterAndTimer>().time + 5f)
            {
                GetComponent<Movement>().enabled = false;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                playerAnimator.SetFloat("Speed", 0);
            }
            else
            {
                GetComponent<Movement>().enabled = true;
                deathText.enabled = false;
            }
        }
    }
    void Death()
    {
        deathText.enabled = true;
        timeOfDeath = gameManager.GetComponent<CounterAndTimer>().time;

        if (gameObject.tag == "Sun")
        {
            gameObject.transform.position = new Vector3(-17, 0, 0);
        }
        else if (gameObject.tag == "Moon")
        {
            gameObject.transform.position = new Vector3(17, 0, 0);
        }

    }
}
