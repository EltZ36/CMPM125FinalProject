using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction MoveAction;
    public InputAction SummonAction;
    [SerializeField] private GameObject Pet; 

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


    }

    // Update is called once per frame
    void Update()
    {
        //read move input
        move = MoveAction.ReadValue<Vector2>();
        if (SummonAction.triggered)
        {
            Summon();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb2d.position + move * 6.0f * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    void Summon()
    {
        Debug.Log("Summoning");
        Instantiate(Pet, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
    }
}