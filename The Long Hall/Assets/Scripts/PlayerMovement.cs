using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float horizInput;
    private float vertInput;
    private Vector3 ForwardMovement;
    private Vector3 RightMovement;
    private Rigidbody rb;
    private Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        // Stores the Input of the player applied to a movement axis
        ForwardMovement = transform.forward * vertInput;
        RightMovement = transform.right * horizInput;

        // Stores the combined input, direction, and speed in velocity
        velocity = (RightMovement + ForwardMovement).normalized * speed;

        // Moves the players rigidbody by the velocity variable and multiplies it by Time.deltaTime
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    }
}
