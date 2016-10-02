using UnityEngine;
using System.Collections;

public class Dinosaur2Move : MonoBehaviour {

    private float gameSpeed = 5;
    private float maxSpeed = 30;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        gameSpeed = 5;
	}
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (gameSpeed < maxSpeed) { 
            gameSpeed = gameSpeed * 3;
            }
        }
        if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * gameSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * gameSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * gameSpeed * Time.deltaTime * 2);
         }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * gameSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    } // movement
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Finish")
        {
            endgame("You win! Press esc to quit");
            //Debug.Log("You win!");
        }
        if (col.gameObject.tag == "Border")
        {
            endgame("You lose! Press esc to quit");
            //Debug.Log("You win!");
        }
    }
    private void endgame (string input)
    {
        UpdateInfo.game_end = true;
        UpdateInfo.UpdateText(input);
    }

}
