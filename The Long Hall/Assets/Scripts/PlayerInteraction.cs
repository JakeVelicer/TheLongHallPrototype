using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float speed = 10;
    public bool canHold = true;
    public GameObject bodyPart;
    public Transform guide;
    //GameObject glue = new GameObject("glue");

    void Update()

    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!canHold)
                throw_drop();
            else
                Pickup();
        }//mause If

        if (!canHold && bodyPart)
        {
            bodyPart.transform.position = guide.position;
            Rotate();
        }
           
        
    }//update

    //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "BodyPart")
        {
            Debug.Log("BodyPart");
            if (!bodyPart) // if we don't have anything holding
                bodyPart = col.gameObject;

           
        }
    }

    //We can use trigger or Collision
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "BodyPart")
        {
            if (canHold)
                bodyPart = null;
        }
    }


    private void Pickup()
    {
        if (!bodyPart)
            return;

        //We set the object parent to our guide empty object.
        bodyPart.transform.SetParent(guide);
        
        
        //Set gravity to false while holding it
        bodyPart.GetComponent<Rigidbody>().useGravity = false;
        bodyPart.GetComponent<Rigidbody>().isKinematic = true;

        //we apply the same rotation our main object (Camera) has.
        bodyPart.transform.localRotation = transform.rotation;
        //We re-position the ball on our guide object 
        bodyPart.transform.position = guide.position;

        canHold = false;
    }

    private void throw_drop()
    {
        if (!bodyPart)
            return;

        //Set our Gravity to true again.
        bodyPart.GetComponent<Rigidbody>().useGravity = true;
        bodyPart.GetComponent<Rigidbody>().isKinematic = false;
        // we don't have anything to do with our ball field anymore
        bodyPart = null; 
        //Apply velocity on throwing
        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        //Unparent our ball
        guide.GetChild(0).parent = null;
        canHold = true;
    }

    private void Rotate()
    {
        // rotate object to the left
        if(Input.GetKey(KeyCode.Z))
        {
            bodyPart.transform.Rotate(10f, 0, 0);
        }

        //rotate object to the right
        if(Input.GetKey(KeyCode.X))
        {
            bodyPart.transform.Rotate(-10f, 0, 0);
        }
    }
}
