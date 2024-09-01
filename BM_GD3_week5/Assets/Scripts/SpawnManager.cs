using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject bossEnemy;
    public GameObject enemyPrefab;
    public GameObject repelPowerUpPrefab;
    public GameObject jumpPowerUpPrefab;
    public GameObject minePowerUpPrefab;

    private float spawnRange = 100;
    public int enemyCount;
    int waveNumber = 3;
    public TMP_Text waveText; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(repelPowerUpPrefab, GenerateSpawnPosition(), repelPowerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
          enemyCount = FindObjectsOfType<EnemyMovement>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            waveNumber++;
            Instantiate(repelPowerUpPrefab, GenerateSpawnPosition(), repelPowerUpPrefab.transform.rotation);
            Instantiate(jumpPowerUpPrefab, GenerateSpawnPosition(), jumpPowerUpPrefab.transform.rotation);
            Instantiate(minePowerUpPrefab, GenerateSpawnPosition(), minePowerUpPrefab.transform.rotation);

            if (waveNumber % 5 == 0)
            {
                Instantiate(bossEnemy, GenerateSpawnPosition(), bossEnemy.transform.rotation); 
            }

       }

        int waveNumberTrue = waveNumber - 2;
        waveText.text = ("Wave: " + waveNumberTrue);

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
        Vector3 RandomPos = new Vector3(spawnPosX, 1, spawnPosZ);
 
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
