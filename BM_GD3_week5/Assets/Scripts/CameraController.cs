using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;
 //   private float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {
  //      Cursor.visible = false;
  //      Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
  //      float mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);

        transform.position = GameObject.FindObjectOfType<PlayerController>().transform.position;
    }
}
