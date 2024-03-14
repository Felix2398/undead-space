using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelectCamera : MonoBehaviour
{
    public Material[] skins; // Array, das die Materialien der Skins enthält
    private int currentIndex = 0; // Index des aktuell ausgewählten Skins
    
    public Transform[] skinPositions; // Array, das die Positionen der Skins enthält
    public float cameraMoveSpeed = 5f; // Geschwindigkeit, mit der die Kamera sich bewegen soll

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToNextSkin();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToPreviousSkin();
        }
    }

    public void SelectSkinButton()
    {
        PlayerPrefs.SetInt("SelectedSkinIndex", currentIndex);
        SkinManager.GetInstance().SetSkin(skins[currentIndex]);
        SceneManager.LoadSceneAsync(1);
    }

    public void MoveToNextSkin()
    {
        if (currentIndex < skins.Length - 1)
        {
            currentIndex++;
            Vector3 newPosition = new Vector3(skinPositions[currentIndex].position.x, transform.position.y, transform.position.z);
            MoveCameraToPosition(newPosition);
            
        }
    }

    public void MoveToPreviousSkin()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Vector3 newPosition = new Vector3(skinPositions[currentIndex].position.x, transform.position.y, transform.position.z);
            MoveCameraToPosition(newPosition);
        }
    }

    void MoveCameraToPosition(Vector3 targetPosition)
    {
        // Bewegen Sie die Kamera schrittweise zur Zielposition, ohne Interpolation
        transform.position = targetPosition;
    }
}
