using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

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
    public bool hasMinePowerUp;
    public int minesCount = 1;
    public int livesCount = 3;

    public GameObject repelPowerUpIndicator;
    public GameObject jumpPowerUpIndicator;
    public GameObject projectile;

    public TMP_Text minesText;
    public TMP_Text livesText;
    public GameObject gameOverScreen;
   

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        hasJumpPowerUp = false;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        //player movement

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector3(horizontalInput, verticalInput).normalized;

        rb.AddForce(focalPoint.transform.forward * playerSpeed * moveDirection.y);
        rb.AddForce(focalPoint.transform.right * playerSpeed * moveDirection.x);

        //if you fall off the edge

        if (transform.position.y < lowerBoundY)
        {
            transform.position = startPos;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            hasRepelPowerUp = false;
            repelPowerUpIndicator.SetActive(false);
            hasJumpPowerUp = false;
            livesCount--;
            GameOverCheck();
        }

        //powerUp indicator movement

        repelPowerUpIndicator.transform.position = transform.position + new Vector3(0, -0.4f, 0);
        repelPowerUpIndicator.transform.Rotate(0, 45 * Time.deltaTime, 0);

        jumpPowerUpIndicator.transform.position = transform.position + new Vector3(0, 0, 0);
        jumpPowerUpIndicator.transform.Rotate(0, -45 * Time.deltaTime, 0);

        //jump

        if (Input.GetKeyDown(KeyCode.Space) && hasJumpPowerUp == true)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        //lay mine

        if (Input.GetKeyDown(KeyCode.E) && minesCount > 0)
            {   
                Instantiate(projectile, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, projectile.transform.rotation.z ));
                minesCount--;
            }

        //UI
        minesText.text = "Mines: " + minesCount;
        livesText.text = "Lives: " + livesCount;
    }


    // collecting powerUps

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

        if (other.CompareTag("MinePowerUp"))
        {
            Destroy(other.gameObject);
            hasMinePowerUp = true;
            minesCount =+ 3;
        }
    }

    //Repel power

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasRepelPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 repel = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(repel * repelForce, ForceMode.Impulse);
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 repelled =(transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(repelled * (repelForce*2), ForceMode.Impulse);
        }


    }

    //PowerUp timer

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(25);
        hasRepelPowerUp = false;
        repelPowerUpIndicator.SetActive(false);
        hasJumpPowerUp = false;
        jumpPowerUpIndicator.SetActive(false);
    }

    void GameOverCheck()
    {
        if (livesCount == 0)
        {
            gameOverScreen.SetActive(true );
            Destroy(gameObject);
            Debug.Log("GAME OVER");
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }


}
