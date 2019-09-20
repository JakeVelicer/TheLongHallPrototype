using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Animator animator;
    public bool canBeOpened = true;
    private bool doorOpen;
    private bool touchingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingPlayer && canBeOpened)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (doorOpen)
                {
                    animator.Play("DoorAnimationClosing");
                    doorOpen = false;
                }
                else
                {
                    animator.Play("DoorAnimationOpening");
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
        }        
    }
}
