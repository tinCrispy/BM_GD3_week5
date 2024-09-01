using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{


    bool isActive = false;
    //   public float moveSpeed = 25;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
            if (isActive == false)
                {
                     StartCoroutine(ActivateMine());
                }
    }
   //     transform.Translate(Vector3.up * -moveSpeed * Time.deltaTime);
    

    void OnTriggerEnter(Collider other)
    {
        if (isActive == true)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
    }

    IEnumerator ActivateMine()
    {
        yield return new WaitForSeconds(2);
        isActive = true;
        Debug.Log("mine is active");
    }
}
