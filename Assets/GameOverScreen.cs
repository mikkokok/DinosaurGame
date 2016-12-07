using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{

    public static GameObject overscreen;
    // Use this for initialization
    void Start()
    {
        overscreen = GameObject.Find("GameOverScreen");
        overscreen.SetActive(false);
        //DestroyObject(GameObject.Find("GameOverScreen"));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void ShowEnd()
    {
        overscreen.SetActive(true);
    }

}
