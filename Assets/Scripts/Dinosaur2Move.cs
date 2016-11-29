using UnityEngine;
using System.Collections;
using System;

public class Dinosaur2Move : MonoBehaviour
{

    private float gameSpeed = 100;
    //private float maxSpeed = 40;
    private float maxSpeedMultiplier = 2;
    private bool isGrounded = true;
    private bool isFacingRight = true; // Assume player is facing right
    private int returnAnimRate = 3; // How many frames before animation stops
    private int spiketimer = 5; // How many frames before reducing candy again
    public static bool game_is_on = true; // Game is truly running at the start
    private Animator animations;
    Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        isFacing("right");
    }

    // Update is called once per frame
    void Update()
    {
        if (game_is_on) { 
        Movement();
        }
        gameSpeed = 100;
        returnAnimRate--;
        if (returnAnimRate == 0)
        {
            setIdle();
        }
	   if (spiketimer > 0){
		spiketimer--;
		}
    }

    void Movement()
    {
        // Sprint
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (gameSpeed < (gameSpeed * maxSpeedMultiplier))
            {
                gameSpeed = gameSpeed * maxSpeedMultiplier;
            }
        }
        // Walking/running right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * gameSpeed * Time.deltaTime);
            isFacing("right");
            setAnim("IsRunningRight");
        }
        // Walking/running left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * gameSpeed * Time.deltaTime);
            isFacing("left");
            setAnim("IsRunningLeft");
        }
        // Jumping 
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded)
        {
            setIdle();
            returnAnimRate = 3;
            isGrounded = false;
            body.AddForce(new Vector2(0, 180), ForceMode2D.Impulse);
            GameObject.Find("jump").GetComponent<AudioSource>().Play();
        }
        // Going down
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (isGrounded)
            {
                setIdle();
                transform.Translate(Vector2.down * gameSpeed * Time.deltaTime);
            }
        }
        // Try to animate jump
        if (isGrounded == false) // means we have jumping on going
        {
            if (isFacingRight) // we should be jumping right
            {
                setAnim("IsJumpingRight");
            }
            else // we are jumping left
            {
                setAnim("IsJumpingLeft");
            }
        }
        // Exiting game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    } // movement
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            isGrounded = true;
            //spiketimer = 20; not needed
	    
        }
	if (col.gameObject.tag == "Box")
        {
            isGrounded = true;  
	}
        if (col.gameObject.tag == "Finish")
        {
            endgame("You win! Press esc to quit");
            //Debug.Log("You win!");
        }
        if (col.gameObject.tag == "Car")
        {
            endgame("You lose! Press esc to quit");
            Debug.Log("You lose");
        }
        if (col.gameObject.tag == "Candy")
        {
            CoinCounter.AddCandy();
        }
	   if (col.gameObject.tag == "Mushroom")
        {
		 Debug.Log("Spiketimer: "+spiketimer);
            if (spiketimer <= 0) { 
            CoinCounter.ReduceCandy();
            spiketimer = 20;
            }

        }

        if (col.gameObject.tag == "Spikes")
        {
            isGrounded = true;
            Debug.Log("Spiketimer: "+spiketimer);
            if (spiketimer <= 0) { 
            CoinCounter.ReduceCandy();
            spiketimer = 20;
            }
        }
    }
    public static void endgame(string input)
    {
        game_is_on = false;
        UpdateInfo.game_end = true;
        UpdateInfo.UpdateText(input);
    }
    private void setIdle()
    {
        animations.SetBool("IsRunningRight", false);
        animations.SetBool("IsRunningLeft", false);
        animations.SetBool("IsJumpingRight", false);
        animations.SetBool("IsJumpingLeft", false);
    }
    // Helper method to set animation and attempt to reduce copying code all over
    private void setAnim(string anim)
    {
        setIdle();
        returnAnimRate = 3;
        animations.SetBool(anim, true);
    }
    // Method for changing idle state
    private void isFacing(string facing)
    {
        if (facing == "right")
        {
            isFacingRight = true;
            animations.SetBool("IsFaceToRight", true);
        }
        if (facing == "left")
        {
            isFacingRight = false;
            animations.SetBool("IsFaceToRight", false);
        }
    }

}
