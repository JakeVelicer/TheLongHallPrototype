using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlotBehavior : MonoBehaviour
{
    public PuzzleBoardBehavior puzzleBoard;
    public string pieceName;
    public float pieceRotationZ;
    [HideInInspector] public bool slotOccupiedCorrectly;
    [HideInInspector] public bool slotOccupied;
    private GameObject puzzlePieceObject;
    private PlayerInteraction playerHandScript;
    private MeshRenderer mesh;
    private bool touchingPiece;

    // Start is called before the first frame update
    void Start()
    {
        playerHandScript = GameObject.Find("Hand").GetComponent<PlayerInteraction>();
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
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
            mesh.enabled = false;

            puzzlePieceObject.transform.SetParent(this.transform, true);
            puzzlePieceObject.transform.position = transform.position;
            puzzlePieceObject.transform.rotation =
            Quaternion.Euler(0, 90, GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z);

            if (puzzlePieceObject.name == pieceName
            && puzzlePieceObject.transform.rotation.z <= pieceRotationZ + 2
            && puzzlePieceObject.transform.rotation.z >= pieceRotationZ - 2)
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
            mesh.enabled = true;
            puzzlePieceObject = other.gameObject;
            playerHandScript.pieceTouchingPuzzleSlot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = false;
            mesh.enabled = false;
            puzzlePieceObject = null;
            playerHandScript.pieceTouchingPuzzleSlot = false;
        }        
    }
    
}
