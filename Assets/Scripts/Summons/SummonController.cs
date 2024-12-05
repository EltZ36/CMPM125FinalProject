using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SummonController : MonoBehaviour
{
    Animator animator;
    public float speed;
    Rigidbody2D rb2d;
    public bool isVertical;
    public float changeTime;
    public SineWaveParticles SummonParticles; 
    float timer;
    //forward 1 backward -1
    int direction = 1;
    bool aggressive = true;

    public AudioClip hitClip;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        //randomly generate speed and time of direction change
        speed = Random.Range(0.8f, 1.8f);
        changeTime = Random.Range(5.0f, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //when timer reaches 0, reverse direction, reset timer
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            this.gameObject.SetActive(false);
        }

    }
    void FixedUpdate()
    {
        if(!aggressive){
            return;
        }
        Vector2 position = rb2d.position;
        if (isVertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
           // animator.SetFloat("MoveX", 0);
           // animator.SetFloat("MoveY", -direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
           // animator.SetFloat("MoveY", 0);
           // animator.SetFloat("MoveX", direction);
        }

        rb2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemy")
        {
            Debug.Log("HIT");
            other.gameObject.GetComponent<EnemyController>().Fix(); 
            //other.gameObject.SetActive(false);
        }
    }
    public void Fix(){
        aggressive = false;
        //remove enemy object from rigidbody physics sim
        rb2d.simulated = false;
        //animator.speed = 0;
    }
}
 