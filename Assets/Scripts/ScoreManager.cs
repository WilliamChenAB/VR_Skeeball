using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public static int score;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    void FixedUpdate ()
    {
        text.text = score.ToString().PadLeft(3, '0');
	}
}
