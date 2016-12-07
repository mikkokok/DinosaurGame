using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UpdateInfo : MonoBehaviour
{
    public static Text text;
    public static bool game_end = false;
    public static bool game_is_on = false;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        //text.text = "Coins: " + CoinCounter.GetCoin() + "Candies: " + CoinCounter.GetCandy();
        text.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        if (game_is_on && game_end != true)
        {
            //Debug.Log("Text updating");
            UpdateText();
        }
    }
    public static void UpdateText()
    {
        //Debug.Log("Text updating");
        text.text = "Coins: " + CoinCounter.GetCoin() + " Candies: " + CoinCounter.GetCandy();
    }
    public static void UpdateText(string endgame)
    {
        text.fontSize = 15;
        text.color = Color.red;
        text.text = endgame;
    }
    public static void startgame()
    {
        game_is_on = true;
    }
}
