﻿using UnityEngine;
using System.Collections;

public class CoinCounter : MonoBehaviour
{

    public static int coincount = 0;
    public static int candycount = 3;


    public static void Addcoin()
    {
        coincount++;
        //Debug.Log("coincount: "+coincount ); // For debugging
        if (coincount == 10)
        {
            Destroy(GameObject.FindWithTag("Car"));
        }
    }
    public static void AddCandy()
    {
        candycount++;
        Debug.Log("Candy collected"); // For debugging
    }
    public static void ReduceCandy()
    {
        candycount--;
        Debug.Log("Candy reduced"); // For debugging
    }
    public static string GetCandy()
    {
        return candycount.ToString();
    }
    public static void AddSilvercoin()
    {
        for (int i = 0; i < 5; i++)
        {
            Addcoin();
        }
    }

    public static void Losecoin()
    {
        if (coincount == 0) return;
        coincount = coincount - 1;
    }
    public static void Losecoin(int m)
    {
        if ((coincount - m) < 0)
        {
            coincount = 0;
        }
        else coincount = coincount - m;
    }

    public static string GetCoin()
    {
        return coincount.ToString();
    }
} // CoinCounter
