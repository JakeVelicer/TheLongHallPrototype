using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoardBehavior : MonoBehaviour
{
    public DoorBehavior door;
    public GameObject[] puzzleSlots;
    public bool[] slotFilledCorrectly;

    public void AssignPuzzle(GameObject puzzleSlot)
    {
        int trueAmount = 0;
        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            if (puzzleSlots[i].name == puzzleSlot.name)
            {
                if (puzzleSlot.GetComponent<PuzzleSlotBehavior>().slotOccupiedCorrectly)
                {
                    slotFilledCorrectly[i] = true;
                }
            }
            if (slotFilledCorrectly[i] == true)
            {
                trueAmount++;
            }
        }
        if (trueAmount == slotFilledCorrectly.Length)
        {
            door.canBeOpened = true;
            if (gameObject.CompareTag("3rdPuzzle"))
            {
                gameObject.GetComponent<EndStuff>().EndSequence();
            }
        }
    }

    public void RemovePuzzlePiece(GameObject puzzleSlot)
    {
        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            if (puzzleSlots[i].name == puzzleSlot.name)
            {
                slotFilledCorrectly[i] = false;
            }
        }
    }
}
