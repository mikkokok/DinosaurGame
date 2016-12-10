using UnityEngine;
using System.Collections;

public class CoinCounter : MonoBehaviour
{
    public static int coincount = 0;
    public static int candycount = 3;
    public static void Addcoin()
    {
        coincount++;
        //Debug.Log("coincount: "+coincount ); // For debugging
    }
    public static void AddCandy()
    {
        candycount++;
        //Debug.Log("Candy collected"); // For debugging
    }
    public static void ReduceCandy()
    {
        candycount--;
        if (candycount <= 0)
        {
            Dinosaur2Move.endgame("");
        }
        //Debug.Log("Candy reduced: amount "+candycount); // For debugging
    }
    public static string GetCandy()
    {
        return candycount.ToString();
    }
    public static string GetCoin()
    {
        return coincount.ToString();
    }
    public static int GetCoinInt()
    {
        return coincount;
    }
} // CoinCounter
