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

public static void AddSilvercoin () {
	coincount = coincount + 5;
	}

public static void Losecoin (){
		if(coincount == 0) return;
		coincount = coincount -1;
	}
	public static void Losecoin (int m){
		if(coincount - m < 0){
			coincount = 0;
		}
		else coincount = coincount -m;
	}



    public static string GetCoin () {
        return coincount.ToString();
    }
} // CoinCounter
