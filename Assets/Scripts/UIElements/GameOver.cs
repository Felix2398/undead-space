using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI highScoreLabel;

    static GameOver instance;

    void Awake() {
        
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static GameOver GetInstance() {
        return instance;
    }

    public void setHighScore(string text) {
        highScoreLabel.text = "Highscore: " + text;
    }
}
