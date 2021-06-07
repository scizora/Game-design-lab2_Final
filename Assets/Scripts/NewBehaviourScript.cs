using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D marioBody;
    private bool onGroundState = false;
    private AudioSource marioAudio;
    public float upSpeed;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    //commented for leb2
    //public Transform enemyLocation;
    //public Text scoreText;

    //private int score = 0;
    private bool countScoreState = false;
    public float maxSpeed;
    // Start is called before the first frame update


    private Animator marioAnimator;
    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();

        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        marioAnimator.SetBool("onGround", onGroundState);
    }


    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
      
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            Debug.Log(onGroundState);
            marioAnimator.SetBool("onGround", onGroundState);
            //scoreText.text = "Score: " + score.ToString();

        };

        if (col.gameObject.CompareTag("Obstacles"))
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //Vector2 movement = new Vector2(moveHorizontal, 0);
        //marioBody.AddForce(movement * speed);

        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }
        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

 
        marioAnimator.SetFloat("xspeed", Mathf.Abs(marioBody.velocity.x));
        // Debug.Log(marioBody.velocity);

        // when jumping, and Gomba is near Mario and we haven't registered our score
        //if (!onGroundState && countScoreState)
        //{
        //    if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //    {
        //        countScoreState = false;
        //        score++;
        //        Debug.Log(score);
        //    }
        //}
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            //countScoreState = true; //check if Gomba is underneath
            marioAnimator.SetBool("onGround", onGroundState);
        }

     
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }

    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }


}
