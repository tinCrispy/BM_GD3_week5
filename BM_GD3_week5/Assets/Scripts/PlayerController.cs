using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float playerSpeed;
    public GameObject focalPoint;
    float lowerBoundY = -10;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * playerSpeed * verticalInput);

        if (transform.position.y < lowerBoundY)
        {
            transform.position = startPos;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

    }
   



}
