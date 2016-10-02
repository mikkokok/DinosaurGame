using UnityEngine;
using System.Collections;

public class CoinCounter : MonoBehaviour
{

    public static int coincount;

    public static void Addcoin () {
        coincount++;
        //Debug.Log("coincount: "+coincount ); // For debugging
        if (coincount == 10)
        {
            Destroy(GameObject.FindWithTag ("Car"));
        }
    }
    public static string GetCoin () {
        return coincount.ToString();
    }
} // CoinCounter
