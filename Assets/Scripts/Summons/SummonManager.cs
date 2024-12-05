using System;
using System.Collections.Generic;
using UnityEngine;

//modified with the help of gpt to fix the summon issue. 
public class SummonManager : MonoBehaviour
{
    public static SummonManager Instance { get; private set; }

    public GameObject summonPrefab;
    public Transform Player;
    public int totalSummons = 1; // Maximum number of summons allowed
    public AudioClip summonSound;
    private GameObject currentSummon; // Keep track of the current active summon
    private List<GameObject> summonList;

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

    private void Start()
    {
        summonList = new List<GameObject>();
        for (int i = 0; i < totalSummons; i++)
        {
            GameObject tmp = Instantiate(summonPrefab);
            tmp.SetActive(false);
            summonList.Add(tmp);
        }
    }

    public void StartGeneratingSummons()
    {
        // Check if there is already an active summon
        if (currentSummon != null && currentSummon.activeInHierarchy)
        {
            Debug.Log("A summon is already active.");
            return;
        }

        // Spawn a summon if none exists
        SpawnSummon();
    }

    private void SpawnSummon()
    {
        // Get an inactive summon from the pool
        GameObject newSummon = GetSummon(summonList);
        if (newSummon != null)
        {
            currentSummon = newSummon;
            newSummon.transform.position = Player.position + transform.right;
            var summonController = newSummon.GetComponent<SummonController>();
            newSummon.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No available summons in the pool.");
        }
    }

    private GameObject GetSummon(List<GameObject> list)
    {
        foreach (var summon in list)
        {
            if (!summon.activeInHierarchy)
            {
                return summon;
            }
        }
        return null;
    }

    public void DespawnSummon()
    {
        if (currentSummon != null)
        {
            currentSummon.SetActive(false);
            currentSummon = null;
        }
    }
}
