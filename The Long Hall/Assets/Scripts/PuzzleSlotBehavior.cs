using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlotBehavior : MonoBehaviour
{
    public PuzzleBoardBehavior puzzleBoard;
    public string pieceName;
    public float pieceRotationZ;
    private bool touchingPiece;
    private bool slotOccupied;
    private GameObject puzzlePieceObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingPiece)
        {
            if (Input.GetButtonDown("Interact") && !slotOccupied)
            {
                slotOccupied = true;
                puzzlePieceObject.transform.SetParent(this.transform);
                if (puzzlePieceObject.name == pieceName
                && puzzlePieceObject.transform.rotation.z == pieceRotationZ)
                {
                    //Tell master puzzle script this slot is correct
                }
            }
            else if (Input.GetButtonDown("Interact") && slotOccupied)
            {
                puzzlePieceObject.transform.SetParent(GameObject.Find("Holder").transform);
                slotOccupied = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PuzzlePiece") && !slotOccupied)
        {
            touchingPiece = true;
            puzzlePieceObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PuzzlePiece") && !slotOccupied)
        {
            touchingPiece = false;
            puzzlePieceObject = null;
        }        
    }
    
}
