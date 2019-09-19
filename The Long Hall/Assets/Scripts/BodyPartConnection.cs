using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartConnection : MonoBehaviour
{
    public NPCBehavior npcBehavior;
    public GameObject[] bodyParts;
    public int partsToComplete;
    private int parts;

    // Start is called before the first frame update
    void Start()
    {
        npcBehavior.enabled = false;
        foreach (GameObject part in bodyParts)
        {
            part.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("BodyPart"))
        {
            foreach (GameObject part in bodyParts)
            {
                if(other.gameObject.name == part.name)
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
