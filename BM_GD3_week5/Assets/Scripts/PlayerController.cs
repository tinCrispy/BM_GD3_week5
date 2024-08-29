using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float playerSpeed;
    public GameObject focalPoint;
    float lowerBoundY = -10;
    Vector3 startPos;

    public bool hasRepelPowerUp;
    public int repelForce;
    public bool hasJumpPowerUp;
    public int jumpForce = 5;

    public GameObject repelPowerUpIndicator;
    public GameObject jumpPowerUpIndicator;
    

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
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector3(horizontalInput, verticalInput).normalized;

        rb.AddForce(focalPoint.transform.forward * playerSpeed * moveDirection.y);
        rb.AddForce(focalPoint.transform.right * playerSpeed * moveDirection.x);


        if (transform.position.y < lowerBoundY)
        {
            transform.position = startPos;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            hasRepelPowerUp = false;
            repelPowerUpIndicator.SetActive(false);
            hasJumpPowerUp = false;
        }

        repelPowerUpIndicator.transform.position = transform.position + new Vector3(0, -0.4f, 0);
        repelPowerUpIndicator.transform.Rotate(0, 45 * Time.deltaTime, 0);

        jumpPowerUpIndicator.transform.position = transform.position + new Vector3(0, 0, 0);
        jumpPowerUpIndicator.transform.Rotate(0, -45 * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space) && hasJumpPowerUp == true)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RepelPowerUp"))
        {
            Destroy(other.gameObject);
            hasRepelPowerUp = true;
            repelPowerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCountDownRoutine());
        }

        else if (other.CompareTag("JumpPowerUp"))
        {
            Destroy(other.gameObject);
            hasJumpPowerUp = true;
            jumpPowerUpIndicator.SetActive(true);                                                                     
            StartCoroutine(PowerUpCountDownRoutine());

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasRepelPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 repel = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(repel * repelForce, ForceMode.Impulse);
        }


    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasRepelPowerUp = false;
        repelPowerUpIndicator.SetActive(false);
        hasJumpPowerUp = false;
        jumpPowerUpIndicator.SetActive(false);
    }




}
