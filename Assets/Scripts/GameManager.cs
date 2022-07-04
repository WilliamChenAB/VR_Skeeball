using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int points;
    public int ballsLeft;

    private bool gameOver = false;
    private float bufferTime = 3f;

    void Start()
    {
        points = 0;
        ballsLeft = 5;
    }

    void FixedUpdate()
    {
        if (gameOver)
        {
            bufferTime -= Time.fixedDeltaTime;
            if (bufferTime <= 0)
            {
            }
        }
    }

    public void AddPoints (int points)
    {
        this.points += points;
        ballsLeft -= 1;
        if (ballsLeft <= 0)
            gameOver = true;
    }
}
