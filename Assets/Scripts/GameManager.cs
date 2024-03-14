using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importieren des TextMeshPro-Namespace

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI currentWaveLabel;

    public TextMeshProUGUI nextWaveWarningText;

    public TextMeshProUGUI highScoreLabel;
    public TextMeshProUGUI timeLabel;

    public TextMeshProUGUI TimeLabel
    {
        get { return timeLabel; }
    }

    private int currentHighScore = 0;

    public int CurrentHighScore
    {
        get { return currentHighScore; }
    }

    static GameManager instance;

    public static GameManager GetInstance() {
        return instance;
    }
    
    void Awake() {
        
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreLabel.text = currentHighScore.ToString("D6");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrentWaveLabel(int waveNum) {

        if(waveNum == 1) {
           currentWaveLabel.gameObject.SetActive(true);
           currentWaveLabel.GetComponent<TextFadeIn>().FadeInText("Wave " + waveNum.ToString());
        }

        currentWaveLabel.text = "Wave " + waveNum.ToString();
    }

    public void DisplayNextWaveWarning() {
        nextWaveWarningText.GetComponent<TextFadeInOut>().DisplayText("Next Wave is coming!");
    }

    public void IncrementHighscore(int points) {

        currentHighScore += points;
        highScoreLabel.text = currentHighScore.ToString("D6");
    }

    public void hideHighscore() {
        highScoreLabel.enabled = false;
    }

    public void hideTimer() {
        timeLabel.enabled = false;
    }

    public void hideWaveLabel() {
        currentWaveLabel.enabled = false;
    }
}
