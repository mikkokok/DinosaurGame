using UnityEngine;
using System.Collections;

public class CandyHandle : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //Debug.Log("Coin found"); // For debugging
            CoinCounter.Addcoin();
        }
    }
}
