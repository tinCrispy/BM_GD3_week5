using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    } 

    // Update is called once per frame
    void Update()
    {
        rb.AddForce((player.transform.position - transform.position).normalized * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        if (GetComponent<Rigidbody>().velocity.sqrMagnitude < 1)
        {
            StartCoroutine(amIStuckRoutine());

        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CollisionDetector"))
        {
            Destroy(gameObject) ;
            Debug.Log("enemy deleted");
        }
    }

    IEnumerator amIStuckRoutine()
    {
        yield return new WaitForSeconds(5);

        if (GetComponent<Rigidbody>().velocity.sqrMagnitude < 1)
        {
            Destroy(gameObject);
        }
    }
}
