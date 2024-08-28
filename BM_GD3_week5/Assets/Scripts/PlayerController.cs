using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float playerSpeed;
    public GameObject focalPoint;
    float lowerBoundY = -10;
    Vector3 startPos;

    public bool hasPowerUp;
    public int repelForce;

    public GameObject powerUpIndicator;

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
            hasPowerUp = false;
            powerUpIndicator.SetActive(false);
        }

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.4f, 0);
        powerUpIndicator.transform.Rotate(0, 45 * Time.deltaTime, 0);



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
            {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCountDownRoutine());
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 repel = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(repel * repelForce, ForceMode.Impulse);
        }

    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }




}
