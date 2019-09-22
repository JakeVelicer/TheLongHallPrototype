using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] voiceLines;
    public GameObject buttonPrompt;
    private Transform player;
    private bool touchingPlayer;
    private bool dialoguePlaying;
    private int currentLine;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        buttonPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt
        (new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (touchingPlayer && !dialoguePlaying && currentLine < voiceLines.Length)
        {
            buttonPrompt.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(PlayDialogue());
            }
        }
        else if (touchingPlayer && dialoguePlaying)
        {
            buttonPrompt.SetActive(false);
        }
    }

    private IEnumerator PlayDialogue()
    {
        dialoguePlaying = true;
        audioSource.clip = voiceLines[currentLine];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        dialoguePlaying = false;
        currentLine++;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
            buttonPrompt.SetActive(false);
        }        
    }
}
