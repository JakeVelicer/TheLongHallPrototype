using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public string mouseXInputName, mouseYInputName;
    public float mouseSensitivity;
    private float mouseX;
    private float mouseY;
    private float xAxisClamp = 0;

    void Awake()
    {
        LookCursor();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    private void LookCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CameraRotation()
    {
        mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 60)
        {
            xAxisClamp = 60;
            mouseY = 0;
            ClampXAxisRotation(270);
        }
        else if (xAxisClamp < -60)
        {
            xAxisClamp = -60;
            mouseY = 0;
             ClampXAxisRotation(60);
        }

        transform.Rotate(Vector3.left * mouseY);
        player.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotation(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
