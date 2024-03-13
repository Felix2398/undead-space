using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI highScoreLabel;
    public TextMeshProUGUI timeLabel;

    private bool isPlayerDead = false;
	
	public bool IsPlayerDead
	{
		get { return isPlayerDead;}
		set { isPlayerDead = value;}	
	}
	
    void Start() {

    }
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

    public void SetHighScore(string text) {
        highScoreLabel.text = "Highscore: " + text;
    }

    public void SetTime(string text) {
        timeLabel.text = text;
    }

    public void onPlayerDeath() {

        IsPlayerDead = true;
        GameManager.GetInstance().hideHighscore();
        GameManager.GetInstance().hideTimer();
        GameManager.GetInstance().hideWaveLabel();
        SetHighScore(GameManager.GetInstance().CurrentHighScore.ToString());
        GameManager.GetInstance().TimeLabel.GetComponent<TimeCalculator>().StopTime();
        SetTime(GameManager.GetInstance().TimeLabel.text.ToString());
		IsPlayerDead = true;
        MusicShuffler.GetInstance().PauseMusic();
    }
}
