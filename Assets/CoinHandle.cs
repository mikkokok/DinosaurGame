﻿using UnityEngine;
using System.Collections;

public class CoinHandle : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("coinssound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            //Debug.Log("Coin found"); // For debugging
            CoinCounter.Addcoin();
        }
    }
}
