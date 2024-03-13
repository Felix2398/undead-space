using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health {

    public static bool GameIsPaused = false;
    public GameObject GameOverMenuUI;


    public GameObject GameWaveUI;
    


    public void ApplyDamage(float damage)
    {
        SubtractLife(damage);


        if (health <= 0)
        {
            GameOverMenuUI.SetActive(true);
            GameWaveUI.SetActive(false);
            // Time.timeScale = 0f;
            GameIsPaused = true;
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameOver.GetInstance().setHighScore(GameManager.GetInstance().CurrentHighScore.ToString());
			GameOver.GetInstance().IsPlayerDead = true;
            MusicShuffler.GetInstance().PauseAudioSource();
        }
    }

    public void ApplyHealthBuff(float boost) {

        if(health + boost <= maxHealth) {
            health += boost;
        }
        else {
            health = maxHealth;
        }

        healthbar.GetComponent<Healthbar>().UpdateHealth(health / maxHealth);
    }

	public void QuitGame()
	{
		Application.Quit();
	}
	
	public void OpenMainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadSceneAsync(0);
	}
	
	public void RestartGame(){ 
		Time.timeScale = 1f;
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
