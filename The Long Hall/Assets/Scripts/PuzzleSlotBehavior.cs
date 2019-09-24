using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlotBehavior : MonoBehaviour
{
    public PuzzleBoardBehavior puzzleBoard;
    public string pieceName;
    public float pieceRotationZ;
    [HideInInspector] public bool slotOccupiedCorrectly;
    private bool touchingPiece;
    private bool slotOccupied;
    private GameObject puzzlePieceObject;
    private PlayerInteraction playerHandScript;

    // Start is called before the first frame update
    void Start()
    {
        playerHandScript = GameObject.Find("Hand").GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(touchingPiece);
        if (touchingPiece)
        {
            if (Input.GetButtonDown("Interact") && !slotOccupied)
            {
                slotOccupied = true;
                puzzlePieceObject.transform.parent = null;
                playerHandScript.puzzlePiece = null;
                playerHandScript.canHold = true;
                puzzlePieceObject.transform.SetParent(this.transform, true);
                Debug.Log("Attached");

                if (puzzlePieceObject.name == pieceName
                && puzzlePieceObject.transform.rotation.z == pieceRotationZ)
                {
                    slotOccupiedCorrectly = true;
                    puzzleBoard.AssignPuzzle(gameObject);
                    Debug.Log("RightPiece");
                }
            }
            else if (Input.GetButtonDown("Interact") && slotOccupied)
            {
                puzzlePieceObject.transform.SetParent(GameObject.Find("Holder").transform);
                slotOccupiedCorrectly = true;
                slotOccupied = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = true;
            puzzlePieceObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = false;
            puzzlePieceObject = null;
        }        
    }
    
}
