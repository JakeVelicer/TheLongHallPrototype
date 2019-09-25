﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoardBehavior : MonoBehaviour
{

    public GameObject[] puzzleSlots;
    public DoorBehavior door;
    public bool[] slotFilledCorrectly;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
