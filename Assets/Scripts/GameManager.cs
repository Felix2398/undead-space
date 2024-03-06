using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text currentWaveLabel;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrentWaveLabel(int waveNum) {
        currentWaveLabel.text = "Welle " + waveNum.ToString();
    }
}
