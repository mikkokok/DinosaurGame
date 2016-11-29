using UnityEngine;
using System.Collections;

public class MoveCar : MonoBehaviour {
	int dir // -1 = goes to left, 1 goes to right

	// Use this for initialization
	void Start () {
	dir = -1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.left * 100 * -1 * dir * Time.deltaTime);
    }
	 void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Checkpoint"){
            dir = dir * -1;
}
}
