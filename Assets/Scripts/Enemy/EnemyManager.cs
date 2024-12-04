using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//uses a singleton pattern to ensure only one instance of 
//game manager exists at any time
public class EnemyManager : MonoBehaviour
{
    // Singleton instance of GameManager
    //mark private only this class can modify this instance
    public static EnemyManager Instance { get; private set; }

    //enemyPrefab movement script is modified to generate random speed
    //and change direction at a random time
    public GameObject enemyPrefab;
    // public GameObject enemyPrefab2;
    // public GameObject enemyPrefab3;
    // Time between spawns
    public int totalEnemies;
    private int enemiesFixed;
    public AudioClip completionSound;
    public AudioSource backgroundMusic;
    public GameObject completionPanel;

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
    public void EnemyFixed()
    {
        enemiesFixed++;
        if (enemiesFixed >= totalEnemies)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
        AudioSource.PlayClipAtPoint(completionSound, Camera.main.transform.position);
        // Optionally display a congratulations message to the player.
        completionPanel.SetActive(true);
    }
}




