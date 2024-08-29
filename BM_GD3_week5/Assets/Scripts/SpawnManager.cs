using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 20;
    public int enemyCount;
    int waveNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
          enemyCount = FindObjectsOfType<EnemyMovement>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            waveNumber++;
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
       }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("enemyCount =" + enemyCount);
            Debug.Log("waveNumber =" + waveNumber);
        }
    }
    private Vector3 GenerateSpawnPosition()
    { 
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);
        Vector3 RandomPos = new Vector3(spawnPosX, 0, spawnPosZ);
 
       return RandomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

      
    }




}
