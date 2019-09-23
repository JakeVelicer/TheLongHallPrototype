using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieces : MonoBehaviour
{
    public NPCBehavior npcBehavior;
    public GameObject[] puzzlePc;
    public int partsToComplete;
    private int parts;

    void Start()
    {
       // npcBehavior.enabled = false;
        foreach (GameObject part in puzzlePc)
        {
            part.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PzPiece"))
        {
            foreach (GameObject part in puzzlePc)
            {
                if (other.gameObject.name == part.name)
                {
                    Destroy(other.gameObject);
                    part.SetActive(true);
                    parts++;

                    if (parts >= partsToComplete)
                    {
                        npcBehavior.enabled = true;
                    }
                }
            }
        }
    }
}
