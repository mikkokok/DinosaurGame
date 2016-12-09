using UnityEngine;
using System.Collections;

public class EndingScreen : MonoBehaviour
{

    public static GameObject overscreen;
    public static GameObject endingscreena;
    public static GameObject endingscreenb;
    public static GameObject endingscreenc;
    // Use this for initialization
    void Start()
    {
        overscreen = GameObject.Find("GameOverScreen");
        overscreen.SetActive(false);
        endingscreena = GameObject.Find("EndscreenA");
        endingscreena.SetActive(false);
        endingscreenb = GameObject.Find("EndscreenB");
        endingscreenb.SetActive(false);
        endingscreenc = GameObject.Find("EndscreenC");
        endingscreenc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void ShowEnd()
    {
        overscreen.SetActive(true);
    }
    public static void ShowEndScreenA()
    {
        //Debug.Log("Showing end screen A");
        endingscreena.SetActive(true);
    }
    public static void ShowEndScreenB()
    {
        //Debug.Log("Showing end screen B");
        endingscreenb.SetActive(true);
    }
    public static void ShowEndScreenC()
    {
        //Debug.Log("Showing end screen C");
        endingscreenc.SetActive(true);
    }
}