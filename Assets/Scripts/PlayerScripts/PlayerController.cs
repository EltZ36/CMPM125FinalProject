using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction MoveAction;
    public InputAction SummonAction;
    [SerializeField] private GameObject Pet;
    SpawnLogic spawner;
    public GameObject spawnManager;
    // public int maxHealth = 10;

    // int currentHeath;
    Rigidbody2D rb2d;

    Vector2 move;

    // AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        // currentHeath = maxHealth;
        MoveAction.Enable();
        SummonAction.Enable();

        rb2d = GetComponent<Rigidbody2D>();
        spawner = spawnManager.GetComponent<SpawnLogic>();

    }

    // Update is called once per frame
    void Update()
    {
        //read move input
        move = MoveAction.ReadValue<Vector2>();
        if (SummonAction.triggered)
        {
            Summon(spawner.summonList1);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb2d.position + move * 6.0f * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    void Summon(List<GameObject> list)
    {
        Debug.Log("Summoning");
        GameObject newsummon = spawner.GetSummon(list);
        if (newsummon != null)
        {
            newsummon.transform.position = transform.position + new Vector3(-2,0,0);
            newsummon.transform.rotation = transform.rotation;
            newsummon.SetActive(true);
            // Still needs/assumes way for summon to die
        }
        else
        {
            Debug.Log("problem summoning");
        }
        //Instantiate(Pet, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
    }
}