using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlotBehavior : MonoBehaviour
{
    public PuzzleBoardBehavior puzzleBoard;
    public string pieceName;
    public float pieceRotationZ;
    public float optionPieceRotationZ2;
    [HideInInspector] public bool slotOccupiedCorrectly;
    [HideInInspector] public bool slotOccupied;
    public GameObject puzzlePieceObject;
    private PlayerInteraction playerHandScript;
    private MeshRenderer mesh;
    private bool touchingPiece;

    public AudioSource sound;
    public AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
        playerHandScript = GameObject.Find("Hand").GetComponent<PlayerInteraction>();
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        if (puzzlePieceObject != null)
        {
            AttachPiece();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingPiece && !slotOccupied)
        {
            if (Input.GetMouseButtonDown(0))
            {
                sound.PlayOneShot(click);
                AttachPiece();
            }
            playerHandScript.pieceTouchingPuzzleSlot = true;
        }
        else
        {
            playerHandScript.pieceTouchingPuzzleSlot = false;
        }

    }

    public void AttachPiece()
    {
        slotOccupied = true;
        puzzlePieceObject.transform.parent = null;
        playerHandScript.puzzlePiece = null;
        playerHandScript.canHold = true;
        mesh.enabled = false;

        puzzlePieceObject.GetComponent<Rigidbody>().useGravity = false;
        puzzlePieceObject.GetComponent<Rigidbody>().isKinematic = true;

        puzzlePieceObject.transform.SetParent(this.transform, true);
        puzzlePieceObject.transform.position = transform.position;
        puzzlePieceObject.transform.rotation =
        Quaternion.Euler(0, 90, GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z);

        Debug.Log(GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z);

        if (puzzlePieceObject.name == pieceName)
        {
            if((GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z <= pieceRotationZ + 2
            && GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z >= pieceRotationZ - 2)
            || (GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z <= optionPieceRotationZ2 + 2
            && GameObject.Find(puzzlePieceObject.name).transform.localEulerAngles.z >= optionPieceRotationZ2 - 2))
            {
                slotOccupiedCorrectly = true;
                puzzleBoard.AssignPuzzle(gameObject);
                Debug.Log("RightPiece");
            }
        }
    }

    public void RemovePiece()
    {
        puzzlePieceObject.transform.parent = null;
        slotOccupiedCorrectly = false;
        slotOccupied = false;
        puzzleBoard.RemovePuzzlePiece(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = true;
            mesh.enabled = true;
            puzzlePieceObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PzPiece") && !slotOccupied)
        {
            touchingPiece = false;
            mesh.enabled = false;
            puzzlePieceObject = null;
        }        
    }
    
}
