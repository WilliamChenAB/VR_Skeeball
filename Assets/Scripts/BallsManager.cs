using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallsManager : MonoBehaviour {

    public static int ballsLeft;

    private TextMeshProUGUI text;
    private int zerosToAdd;

    void Awake ()
    {
        text = GetComponent<TextMeshProUGUI>();
        ballsLeft = 5;
    }

	void FixedUpdate ()
    {
        text.text = ballsLeft.ToString();
	}
}
