using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameStateManager : MonoBehaviour {

    public Text scoreText;
    private int framesElapsed;
    private int secondsElapsed;

    private bool gameOver = false;

	// Use this for initialization
	void Start () {
        framesElapsed = 0;
        secondsElapsed = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!gameOver) {
            framesElapsed++;
            secondsElapsed += framesElapsed / 60;
            framesElapsed = framesElapsed % 60;
            scoreText.text = "Seconds: " + secondsElapsed + " frames: " + framesElapsed;
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            gameOver = true;
            scoreText.text = "Completed in " + secondsElapsed + " seconds, " +  framesElapsed + " frames";
        }
    }
}
