using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//uses a singleton pattern to ensure only one instance of 
//game manager exists at any time
public class SummonManager : MonoBehaviour
{
    // Singleton instance of GameManager
    //mark private only this class can modify this instance
    public static SummonManager Instance { get; private set; }

    //enemyPrefab movement script is modified to generate random speed
    //and change direction at a random time
    public GameObject summonPrefab;
    public Transform Player; 
    // Time between spawns
    public float spawnRate = 2f;    
    private bool isSpawning = false;
    public int totalSummons;
    public AudioClip summonSound;

    // Ensure that there's only one instance of the GameManager
    //awake: when this script is being loaded
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGeneratingSummons()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnSummonsCoroutine());
        }
    }

//the coroutine keeps track of the time 
    IEnumerator SpawnSummonsCoroutine()
    {
        float endTime = Time.time + 2f;  
        // Run for 6 seconds

        while (Time.time < endTime)
        {
            SpawnSummon();
            yield return new WaitForSeconds(spawnRate);  
        }

        isSpawning = false;
    }

    void SpawnSummon()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(0, 2f), 0, 0);
        // Vector2 spawnPosition2 = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 20f));

        Instantiate(summonPrefab, Player.position + spawnPosition, Quaternion.identity);
        // Instantiate(enemyPrefab2, spawnPosition2, Quaternion.identity);
        Debug.Log("Summon spawned at: " + spawnPosition + " using Coroutine.");
    }
}



