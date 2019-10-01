using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
   // public GameObject fuckingPuzzle;
    //public GameObject boundary;
    //public bool enter;
    void Start()
    {
        //boundary = GetComponent<GameObject>();
    }
    private void OnTriggerExit(Collider col)
    {
       
        if(col.gameObject.tag == "Boundary1")
        {
            
            transform.position = new Vector3(-30.97f, 1.63f, 0.361f);
            //Debug.Log("trigger");
        }

        if(col.gameObject.tag == "Boundary2")
        {
            transform.position = new Vector3(-9.64f, 1.3f, 0.46f);
        }

        if(col.gameObject.tag == "Boundary3")
        {
            transform.position = new Vector3(10.07f, 1.3f, -0.07f);
        }
    }
}
