using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float speed = 10;
    public bool canHold = true;
    public GameObject puzzlePiece;
    public Transform guide;
    public bool pieceTouchingPuzzleSlot;
      
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!canHold && !pieceTouchingPuzzleSlot)
                throw_drop();
            else
                Pickup();
        }

        if (!canHold && puzzlePiece)
        {
            puzzlePiece.transform.position = guide.position;
            Rotate();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PzPiece")
        {
            if (!puzzlePiece) // if we don't have anything holding
                puzzlePiece = col.gameObject;
           
        }
    }

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
        if (puzzlePiece.transform.parent != null)
        {
            puzzlePiece.transform.parent.GetComponent<PuzzleSlotBehavior>().RemovePiece();
        }
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
            puzzlePiece.transform.Rotate(0, 0, -90f);
        }

        //rotate object to the right
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            puzzlePiece.transform.Rotate(0, 0, 90f);
        }
    }
}
