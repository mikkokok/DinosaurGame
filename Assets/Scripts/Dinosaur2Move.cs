using UnityEngine;
using System.Collections;
using System;

public class Dinosaur2Move : MonoBehaviour
{

    private float gameSpeed = 150;
    //private float maxSpeed = 40;
    private float maxSpeedMultiplier = 2;
    private bool isGrounded = true;
    private bool isFacingRight = true; // Assume player is facing right
    private int returnAnimRate = 3; // How many frames before animation stops
    private int spiketimer = 5; // How many frames before reducing candy again
    public static bool game_is_on = false; // Game is truly running at the start
    private Animator animations;
    Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        isFacing("right");
        GetComponent<SpriteRenderer>().enabled = false;
        body.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    // Update is called once per frame
    void Update()
    {
        // Start or not to start
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
        {
            DestroyObject(GameObject.Find("Startscreen"));
            GetComponent<SpriteRenderer>().enabled = true;
            game_is_on = true;
            body.constraints = RigidbodyConstraints2D.None;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
            UpdateInfo.startgame();
        }
        if (game_is_on)
        {
            Movement();
        }
        gameSpeed = 150;
        returnAnimRate--;
        if (returnAnimRate == 0)
        {
            setIdle();
        }
        if (spiketimer > 0)
        {
            spiketimer--;
        }
        if (Input.GetKey(KeyCode.R))
        {
            //EndingScreen.ShowEndScreenA(); // For testing
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
            body.velocity = new Vector2(body.velocity.y, 0);
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
    } // movement
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border" || col.gameObject.tag == "Box")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "Finish")
        {
            game_is_on = false;

            if (CoinCounter.GetCoinInt() <= 20)
            {
                EndingScreen.ShowEndScreenA();
            }
            if (CoinCounter.GetCoinInt() >= 20 && CoinCounter.GetCoinInt() < 50)
            {
                EndingScreen.ShowEndScreenB();
            }
            if (CoinCounter.GetCoinInt() >= 50)
            {
                EndingScreen.ShowEndScreenC();
            }
        }
        if (col.gameObject.tag == "Car")
        {
            CoinCounter.ReduceCandy();
        }
        if (col.gameObject.tag == "Candy")
        {
            CoinCounter.AddCandy();
        }
        if (col.gameObject.tag == "Mushroom")
        {
            CoinCounter.ReduceCandy();
        }
        if (col.gameObject.tag == "Spikes")
        {
            isGrounded = true;
            if (spiketimer <= 0)
            {
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
        EndingScreen.ShowEnd();
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
