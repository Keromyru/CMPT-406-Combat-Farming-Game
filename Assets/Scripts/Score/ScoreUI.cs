using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + Score.getScore().ToString();
        Score.scoreEvent.AddListener(updateScore);
    }

    private void updateScore()
    {
        scoreText.text = "Score: " + Score.getScore().ToString();
    }
}
