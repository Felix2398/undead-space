using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreLabel;
    public TextMeshProUGUI time;


    static InGameUIManager instance;

    public static InGameUIManager GetInstance() {
        return instance;
    }
    
    void Awake() {
        
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void hideHighscore() {
        highscoreLabel.enabled = false;
    }

    public void hideTimer() {
        time.enabled = false;
    }
}
