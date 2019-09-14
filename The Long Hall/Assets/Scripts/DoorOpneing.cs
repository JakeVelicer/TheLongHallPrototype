﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpneing : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("OpenDoor");
    }

    void OnTriggerExit(Collider other)
    {
        anim.enabled = true;
    }
   
    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}