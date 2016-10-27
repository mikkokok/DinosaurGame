using UnityEngine;
using System.Collections;
using System;

public class Dinosaur2Move : MonoBehaviour
{

    private float gameSpeed = 100;
    private float maxSpeed = 40;
    private float maxSpeedMultiplier = 2;
    private bool isGrounded = true;
    private bool isIdle = true; // Assume player is not moving
    private bool isFacingRight = true; // Assume player is facing right
    private int returnAnimRate = 3; // How many frames before animation stops
    private Animator animation;
    Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isIdle = true;
        Movement();
        gameSpeed = 100;
        returnAnimRate--;
        if (returnAnimRate == 0)
        {
            setIdle();
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
            isFacingRight = true;
            setAnim("IsRunningRight");
        }
        // Walking/running left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * gameSpeed * Time.deltaTime);
            isFacingRight = false;
            setAnim("IsRunningLeft");
        }
        // Jumping 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            if (isGrounded)
            {
                setIdle();
                returnAnimRate = 3;
                isGrounded = false;
                body.AddForce(new Vector2(0, 180), ForceMode2D.Impulse);
            }
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
        isGrounded = true;
        if (col.gameObject.tag == "Finish")
        {
            endgame("You win! Press esc to quit");
            //Debug.Log("You win!");
        }
        if (col.gameObject.tag == "Border" || col.gameObject.tag == "Car")
        {
            endgame("You lose! Press esc to quit");
            Debug.Log("You lose");
        }
    }
    private void endgame(string input)
    {
        UpdateInfo.game_end = true;
        UpdateInfo.UpdateText(input);
    }
    private void setIdle()
    {
        animation.SetBool("IsRunningRight", false);
        animation.SetBool("IsRunningLeft", false);
        animation.SetBool("IsJumpingRight", false);
        animation.SetBool("IsJumpingLeft", false);
    }
    // Helper method to set animation and attempt to reduce copying code all over
    private void setAnim(string anim)
    {
        setIdle();
        returnAnimRate = 3;
        animation.SetBool(anim, true);
    }

}
