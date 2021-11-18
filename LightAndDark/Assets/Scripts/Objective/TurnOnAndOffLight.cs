using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TurnOnAndOffLight : MonoBehaviour
{
    private GameObject gameManager;
    private Light lightSource;
    private ParticleSystem particleEffects;
    private GameObject textPrompt;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
        particleEffects = GetComponent<ParticleSystem>();
        if (gameObject.tag == "LightUnLit")
        {
            particleEffects.Stop();
        }
        gameManager = GameObject.Find("GameManager");
        textPrompt = transform.GetChild(1).gameObject;
        photonView = gameObject.GetComponent<PhotonView>();
    }


    private void OnTriggerEnter(Collider other)
    {
        textPrompt.GetComponent<MeshRenderer>().enabled = true;

    }
    private void OnTriggerStay(Collider other)
    {
        textPrompt.transform.rotation = Quaternion.LookRotation(textPrompt.transform.position - other.transform.position - new Vector3(0,1,0));
        if (other.tag == "Sun" && Input.GetKey(KeyCode.F) && !lightSource.enabled)
        {
            photonView.RPC("SunLightOn", RpcTarget.All);
        }
        else if (other.tag == "Moon" && Input.GetKey(KeyCode.F) && lightSource.enabled)
        {
            photonView.RPC("MoonLightOff", RpcTarget.All);
        }
    }

    [PunRPC]
    public void SunLightOn()
    {
        lightSource.enabled = true;
        gameManager.GetComponent<CounterAndTimer>().sunCounter += 1;
        particleEffects.Play();
    }

    [PunRPC]
    public void MoonLightOff()
    {
        lightSource.enabled = false;
        gameManager.GetComponent<CounterAndTimer>().moonCounter += 1;
        particleEffects.Stop();
    }

    private void OnTriggerExit(Collider other)
    {
        textPrompt.GetComponent<MeshRenderer>().enabled = false;
    }
}
