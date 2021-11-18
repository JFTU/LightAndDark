using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    public GameObject mainCamera;
    private bool alteredCamera;
    public GameObject attack;
    private bool onFloor = true;
    private Photon.Pun.PhotonView photonView;
    private AudioSource shotSource;
    public bool is_Hit = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerAnimator = gameObject.GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        shotSource = GetComponent<AudioSource>();
        alteredCamera = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool Movement = false;
        if (photonView.IsMine)
        {
            Vector3 speed = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                Movement = true;
                transform.position += transform.forward * 0.05f;
                playerAnimator.SetFloat("Speed", 1);
                if (Input.GetKeyUp(KeyCode.W))
                {
                    Movement = false;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                Movement = true;
                transform.position += transform.forward * -0.05f;
                playerAnimator.SetFloat("Speed", 1);
                if (Input.GetKeyUp(KeyCode.S))
                {
                    Movement = false;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                Movement = true;
                transform.position += transform.right * 0.05f;
                playerAnimator.SetFloat("Speed", 1);
                if (Input.GetKeyUp(KeyCode.D))
                {
                    Movement = false;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                Movement = true;
                transform.position += transform.right * -0.05f;
                playerAnimator.SetFloat("Speed", 1);
                if (Input.GetKeyUp(KeyCode.A))
                {
                    Movement = false;
                }
            }
            if (onFloor && !is_Hit)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }

            if (!Movement)
            {
                playerAnimator.SetFloat("Speed", 0);
            }

            if (playerRigidbody.velocity == Vector3.zero && playerRigidbody.angularVelocity == Vector3.zero)
            {
                is_Hit = false;
            }

            //playerRigidbody.AddForce(speed*2);

            //playerAnimator.SetFloat("Speed", playerRigidbody.velocity.magnitude);

            transform.rotation = Quaternion.Euler(0, Input.mousePosition.x, transform.rotation.z);
            transform.rotation = transform.rotation;
        }
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(1) && !alteredCamera)
            {
                mainCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x = 2;
                mainCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.y = -0.1f;
                alteredCamera = true;
            }
            else if (Input.GetMouseButtonDown(1) && alteredCamera)
            {
                mainCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x = 0.5f;
                mainCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.y = -0.4f;
                alteredCamera = false;
            }

            if (alteredCamera)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    photonView.RPC("Attack", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    public void Attack()
    {
        GameObject currentAttack = Instantiate(attack, transform.position + transform.forward*2 + new Vector3(0, 1f, 0), transform.rotation);
        Rigidbody currentAttackRigidbody = currentAttack.GetComponent<Rigidbody>();
        currentAttackRigidbody.velocity = new Vector3(0, 0, 0);
        currentAttackRigidbody.AddForce(transform.forward * 10, ForceMode.Impulse);
        shotSource.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            onFloor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            onFloor = false;
        }
    }
}
