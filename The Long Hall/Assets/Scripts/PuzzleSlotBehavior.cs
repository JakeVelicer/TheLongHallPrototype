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
    [HideInInspector] public bool slotOccupied;
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
        if (touchingPiece)
        {
            AttachPiece();
        }
    }

    public void AttachPiece()
    {
        if (Input.GetMouseButtonDown(0) && !slotOccupied)
        {
            slotOccupied = true;
            puzzlePieceObject.transform.parent = null;
            playerHandScript.puzzlePiece = null;
            playerHandScript.canHold = true;

            puzzlePieceObject.transform.SetParent(this.transform, true);
            puzzlePieceObject.transform.position = transform.position;
            puzzlePieceObject.transform.rotation =
                Quaternion.Euler(transform.rotation.x, transform.rotation.y,
                puzzlePieceObject.transform.rotation.z);

            if (puzzlePieceObject.name == pieceName
            && puzzlePieceObject.transform.rotation.z <= pieceRotationZ + 1
            && puzzlePieceObject.transform.rotation.z >= pieceRotationZ - 1)
            {
                slotOccupiedCorrectly = true;
                puzzleBoard.AssignPuzzle(gameObject);
                Debug.Log("RightPiece");
            }
        }
    }

    public void RemovePiece()
    {
        slotOccupiedCorrectly = false;
        slotOccupied = false;
        puzzleBoard.RemovePuzzlePiece(gameObject);
        puzzlePieceObject.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = true;
            puzzlePieceObject = other.gameObject;
            playerHandScript.pieceTouchingPuzzleSlot = true;
            Debug.Log(playerHandScript.pieceTouchingPuzzleSlot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = false;
            puzzlePieceObject = null;
            playerHandScript.pieceTouchingPuzzleSlot = false;
            Debug.Log(playerHandScript.pieceTouchingPuzzleSlot);
        }        
    }
    
}
