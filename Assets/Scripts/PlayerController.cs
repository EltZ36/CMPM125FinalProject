using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Input bind to arrow keys in unity editor
    public InputAction MoveAction;
    public AudioSource backgroundMusic;
    public SummonManager SummonManager;
   
    public InputAction LaunchAction;
    public InputAction talkAction;
    public InputAction respawnR;
    public int maxHealth = 5;
    int currentHealth;

    public Quest currentQuest;
    public QuestGiver QuestGiver;
    public int playercoins = 0;

    public int health { get { return currentHealth; } }

    Rigidbody2D rb2D;
    Vector2 move;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;

    AudioSource audioSource;

    // Initialising variables for Game Over Menu 
    // public float myHealth;
    // public Slider healthBar; 
    public GameObject gameOverMenu;
    public AudioClip gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        /*
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        */
       

        MoveAction.Enable();
        LaunchAction.Enable();
        talkAction.Enable();
        respawnR.Enable();

        LaunchAction.performed += Launch;
        talkAction.performed += FindFriend;
        respawnR.performed += RespawnPerformed;

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //character movement using arrow keys up down right left
        //read VALUES from moveAction arrow keys input
        move = MoveAction.ReadValue<Vector2>();

        //if player is moving
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            //get the direction based on which way player is moving
            moveDirection.Set(move.x, move.y);
            //set x and y in 1 or -1
            moveDirection.Normalize();
        }

        //pass move direction to animator parameter
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     StartGeneration();
        // }
        if(currentQuest.completed)
        {
            QuestGiver.gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        //deltaTime makes movement framerate independent
        Vector2 position = (Vector2)rb2D.position + move * 6.0f * Time.deltaTime;
        rb2D.MovePosition(position);
    }

    //currentHealth can not be set to a value below 0 or above maxHealth
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //check health
        if(currentHealth == 0){
            GameOver();
        }
        Debug.Log("Player current Health: " + currentHealth + "/" + maxHealth);
        UIhandler.instance.SetHealthValue(currentHealth / (float)maxHealth);

        if(currentHealth == 0f){
            GameOver();
        }
    }

    void Launch(InputAction.CallbackContext context)
    {
        //instantiate projectile game object, identity means no rotation
        GameObject projectileObject = PoolManager.Instance.GetProjectile();
        // GameObject projectileObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);
        projectileObject.transform.position = rb2D.position + Vector2.up * 0.5f; // Set position
        projectileObject.transform.rotation = Quaternion.identity;
        //get myProjectile script
        MyProjectile projectile = projectileObject.GetComponent<MyProjectile>();
        projectile.Launch(moveDirection, 300);
        //play launch animation when launch
        animator.SetTrigger("Launch");
    }

    void FindFriend(InputAction.CallbackContext context)
    {
        //only check with NPC layer
        //ray length is 1.5f
        RaycastHit2D hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null && hit.collider.gameObject.tag != "foxnpc")
        {
            Debug.Log("Raycast has hit the object" + hit.collider.gameObject);
            //call npc script to display dialogue
            NonPlayerCharScript npcScript = hit.collider.gameObject.GetComponent<NonPlayerCharScript>();
            // Locate the two text boxes in the visual tree
            if (npcScript != null && currentQuest.completed == false)
            { 
                UIhandler.instance.DisplayDialogue();
                QuestGiver.OpenQuestWindow();
                QuestGiver.AcceptQuest();
            }
            if (npcScript != null && currentQuest.completed == true)
            {
                UIhandler.instance.DisplayThanks();
                //add coins to the canvas; 
                //CoinsAdd.instance.addCoins();
                playercoins += 1;
            }
        }
        else if (hit.collider != null && hit.collider.gameObject.tag == "foxnpc")
        {
            //call npc script to display dialogue
            NonPlayerCharScript npcScript = hit.collider.gameObject.GetComponent<NonPlayerCharScript>();
            // Locate the two text boxes in the visual tree
            UIhandler.instance.DisplayFoxChat();
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void GameOver(){
        Debug.Log("Game Over!");
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
        PlaySound(gameOverSound);
        gameOverMenu.SetActive(true);
        
    }

    void RespawnPerformed(InputAction.CallbackContext context)
    {
        SummonManager.Instance.StartGeneratingSummons();

        if (currentQuest.isActive)
        {
            //increments the summon count
            currentQuest.goal.Summon();
            if (currentQuest.goal.IsReached())
            {
                currentQuest.Complete();
            }
        }
    }
}
