using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    [SerializeField] public int total_summons;
    public List<GameObject> summonList1;
    public GameObject summonObject;
    private static SpawnLogic _instance; // make a static private variable of the component data type
    public static SpawnLogic Instance { get { return _instance; } } // make a public way to access the private variable
    private void Awake()
    {
        if (_instance != null && _instance != this)
        { // if there is already a value assigned to the private variable and its not this, destroy this
            Destroy(this.gameObject);
        }
        else
        { // if there is no value assigned to the private variable, assign this as the reference
            _instance = this;

        }
    }
    private void Start()
    {
        summonList1 = new List<GameObject>();
        GameObject tmp1;
        for (int i = 0; i < total_summons; i++)
        {
            tmp1 = Instantiate(summonObject);
            tmp1.SetActive(false);
            summonList1.Add(tmp1);
        }
    }

    public GameObject GetSummon(List<GameObject> list)
    {
        for (int i = 0; i < total_summons; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }
        return null;
    }
    public int NumSummons(List<GameObject> list)
    {
        int count = 0;
        for (int i = 0; i < total_summons; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }
}