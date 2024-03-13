using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject player;    

    // Update is called once per frame
    void Update()
    { 
	    if (!GameOver.GetInstance().IsPlayerDead)
	    {
			if(Input.GetKeyDown(KeyCode.Escape)){
				
				if(GameIsPaused){
					Resume();   
				}
				else {
				 	Pause();

					player.GetComponent<PlayerMovementController>().LookAtMouse = false;
		 		}
			}	
	    }
    }
	

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		player.GetComponent<PlayerMovementController>().LookAtMouse = true;
		MusicShuffler.GetInstance().ResumeMusic();
	}

	void Pause() 
	{
		player.GetComponent<PlayerMovementController>().LookAtMouse = false;
		MusicShuffler.GetInstance().PauseMusic();
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
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
}
