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

   public AudioSource source;
  //  public AudioClip footStep;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        InvokeRepeating("PlaySound", 0.0f, 0.5f);
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

        //checks to see if the player is moving
       
    }
    void PlaySound()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            //plays the footstep at different volume ranges
            source.volume = Random.Range(0.8f, 1f);
            source.pitch = Random.Range(0.8f, 1f);
            //plays the audio
            source.Play();
        }
    }
}
