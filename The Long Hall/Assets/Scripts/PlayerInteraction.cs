using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float speed = 10;
    public bool canHold = true;
    public GameObject bodyPart;
    public Transform guide;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!canHold)
                throw_drop();
            else
                Pickup();
        }

        if (!canHold && bodyPart)
            bodyPart.transform.position = guide.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PzPiece")
        {
            Debug.Log("PzPiece");
            
            if (!bodyPart)
                bodyPart = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PzPiece")
        {
            if (canHold)
                bodyPart = null;
        }
    }

    private void Pickup()
    {
        if (!bodyPart)
            return;

        bodyPart.transform.SetParent(guide);
        
        bodyPart.GetComponent<Rigidbody>().useGravity = false;
        bodyPart.GetComponent<Rigidbody>().isKinematic = true;

        bodyPart.transform.localRotation = transform.rotation;
        bodyPart.transform.position = guide.position;

        canHold = false;
    }

    private void throw_drop()
    {
        if (!bodyPart)
            return;

        bodyPart.GetComponent<Rigidbody>().useGravity = true;
        bodyPart.GetComponent<Rigidbody>().isKinematic = false;
        bodyPart = null;

        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        guide.GetChild(0).parent = null;
        canHold = true;
    }
}
