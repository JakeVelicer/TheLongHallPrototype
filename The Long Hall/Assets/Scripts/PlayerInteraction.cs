using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float speed = 10;
    public bool canHold = true;
    public GameObject puzzlePiece;
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

        if (!canHold && puzzlePiece)
        {
            puzzlePiece.transform.position = guide.position;
            Rotate();
        }
           
        
    }//update

    //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PzPiece")
        {
            Debug.Log("PzPiece");
            if (!puzzlePiece) // if we don't have anything holding
                puzzlePiece = col.gameObject;

           
        }
    }

    //We can use trigger or Collision
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PzPiece")
        {
            if (canHold)
                puzzlePiece = null;
        }
    }


    private void Pickup()
    {
        if (!puzzlePiece)
            return;

        //We set the object parent to our guide empty object.
        puzzlePiece.transform.SetParent(guide);
        
        
        //Set gravity to false while holding it
        puzzlePiece.GetComponent<Rigidbody>().useGravity = false;
        puzzlePiece.GetComponent<Rigidbody>().isKinematic = true;

        //we apply the same rotation our main object (Camera) has.
        puzzlePiece.transform.localRotation = transform.rotation;
        //We re-position the ball on our guide object 
        puzzlePiece.transform.position = guide.position;

        canHold = false;
    }

    private void throw_drop()
    {
        if (!puzzlePiece)
            return;

        //Set our Gravity to true again.
        puzzlePiece.GetComponent<Rigidbody>().useGravity = true;
        puzzlePiece.GetComponent<Rigidbody>().isKinematic = false;
        // we don't have anything to do with our ball field anymore
        puzzlePiece = null; 
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
            puzzlePiece.transform.Rotate(10f, 0, 0);
        }

        //rotate object to the right
        if(Input.GetKey(KeyCode.X))
        {
            puzzlePiece.transform.Rotate(-10f, 0, 0);
        }
    }
}
