using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject MenuUI;
	public GameObject InformationUI;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenSkinScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
	public void GetInformation()
	{
		MenuUI.SetActive(false);	
		InformationUI.SetActive(true);
	}
	
	public void ActivateMenuUI()
	{
		MenuUI.SetActive(true);	
		InformationUI.SetActive(false);
	}
}
