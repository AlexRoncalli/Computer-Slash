using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mininionController : MonoBehaviour
{
    public GameObject flipModel;

    //audio options
    public AudioClip[] idleSound;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;

    public float detectionTime;
    float startRun;
    bool firstDetection;

    //movement option
    public float runSpeed;
    public float walkSpeed;

    SpriteRenderer m_SpriteRenderer;
    public bool facingRight;
    public bool flipX;

    float moveSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;

    bool Detected;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();

        running = false;
        Detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if (Random.Range(0, 10) > 5) Flip();
    }

    void FixedUpdate()
    {
        if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true; 
            }
        }
        if (Detected && !facingRight) myRB.velocity = new Vector3((moveSpeed * -1), myRB.velocity.y, 0);
        else if (Detected && facingRight) myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);

        if(!running && Detected)
        {
            if (startRun < Time.time)
            {
                moveSpeed = runSpeed;
                myAnim.SetTrigger("mdetected");
                running = true;
            }
        }
        /*
        if (!running)
        {
            if(Random.Range(0,10)>5 && nextIdleSound < Time.time)
            {
                AudioClip tempClip = idleSound[Random.Range(0, idleSound.Length)];
                enemyMovementAS.clip = tempClip;
                enemyMovementAS.Play();
                nextIdleSound = idleSoundTime + Time.time;
            }
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("mdetected", Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            firstDetection = false;
            if (running)
            {
                myAnim.SetTrigger("mdetected");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    } 

    //virar o mininion
    void Flip()
    {
        facingRight = !facingRight;

        
         m_SpriteRenderer = GetComponent<SpriteRenderer>();
         m_SpriteRenderer.flipX = !facingRight;

        

        //Vector3 theScale = flipModel.transform.localScale;
        //theScale.z *= -1;
        //flipModel.transform.localScale = theScale;
    }
}
