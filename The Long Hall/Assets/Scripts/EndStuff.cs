using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStuff : MonoBehaviour
{
    public Animator npcAnimator;
    public Animator playerAnimator;
    public GameObject player;
    public GameObject mainCamera;
    public bool isHappening;
    public AudioSource audioSourceEnd;
    public AudioSource scaryMusic;
    public GameObject image;

    public void EndSequence()
    {
        if (!isHappening)
        {
            isHappening = true;
            playerAnimator.enabled = true;
            audioSourceEnd.Play();
            player.GetComponent<PlayerMovement>().enabled = false;
            mainCamera.GetComponent<CameraController>().enabled = false;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.transform.position = new Vector3(14.31f, 1.7f, -0.11f);
            npcAnimator.Play("NPCBadPush");
            playerAnimator.Play("PlayerEnd");
            StartCoroutine(imageDisplay());
        }
    }

    private IEnumerator imageDisplay()
    {
        yield return new WaitForSeconds(0.5f);
        scaryMusic.Play();
        yield return new WaitForSeconds(1.7f);
        image.SetActive(true);
    }
}
