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
  
        //sets the puzzle piece position to a fixed rotation and position
        puzzlePiece.transform.position = guide.transform.position;
        puzzlePiece.transform.rotation = guide.transform.rotation;

        




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
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            puzzlePiece.transform.Rotate(0, 0, 45f);
        }

        //rotate object to the right
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            puzzlePiece.transform.Rotate(0, 0, 45f);
        }
    }
}
