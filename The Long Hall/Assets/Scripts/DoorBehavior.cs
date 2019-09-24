using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorBehavior : MonoBehaviour
{
    public Animator animator;
    public GameObject buttonPrompt;
    public bool canBeOpened = true;
    private bool doorOpen;
    private bool touchingPlayer;

    AudioSource source;
    public AudioClip openingDoor;
    public AudioClip closingDoor;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        buttonPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingPlayer && canBeOpened)
        {
            buttonPrompt.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                if (doorOpen)
                {
                    animator.Play("DoorAnimationClosing");
                    source.PlayOneShot(closingDoor);
                    doorOpen = false;
                }
                else
                {
                    animator.Play("DoorAnimationOpening");
                    source.PlayOneShot(openingDoor);
                    doorOpen = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
            buttonPrompt.SetActive(false);
        }        
    }
}
