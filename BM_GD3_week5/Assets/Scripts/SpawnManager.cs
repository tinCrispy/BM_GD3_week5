using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 20;
    // Start is called before the first frame update
    void Start()
    {
          Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 GenerateSpawnPosition()
    { 
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);
        Vector3 RandomPos = new Vector3(spawnPosX, 0, spawnPosZ);
 
       return RandomPos;
    }




}
