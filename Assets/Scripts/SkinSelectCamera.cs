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
    public Renderer playerRenderer;

    void Start()
    {
        // Abonnieren des SceneManager-Events für das Laden einer neuen Szene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Callback für das Laden einer neuen Szene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Finden und Speichern der Referenz auf den Player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerRenderer = playerObject.GetComponent<Renderer>();
            // Wende den Standard-Skin an, wenn der Player gefunden wird
            ApplySkin(currentIndex);
        }
        else
        {
            Debug.LogError("Player nicht gefunden! Stelle sicher, dass der Player das Tag 'Player' hat.");
        }
    }

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

    
    void ApplySkin(int index)
    {
        if (playerRenderer != null && index >= 0 && index < skins.Length)
        {
            // Wende das Material des ausgewählten Skins auf den Player an
            playerRenderer.material = skins[index];
        }
    }

   

    public void SelectSkinButton()
    {
        PlayerPrefs.SetInt("SelectedSkinIndex", currentIndex);
        SceneManager.LoadSceneAsync(1);
    }
    public void MoveToNextSkin()
    {
        if (currentIndex < skins.Length - 1)
        {
            currentIndex++;
            Vector3 newPosition = new Vector3(skinPositions[currentIndex].position.x, transform.position.y, transform.position.z);
            MoveCameraToPosition(newPosition);
            ApplySkin(currentIndex);
        }
    }

    public void MoveToPreviousSkin()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Vector3 newPosition = new Vector3(skinPositions[currentIndex].position.x, transform.position.y, transform.position.z);
            MoveCameraToPosition(newPosition);
            ApplySkin(currentIndex);
        }
    }

    void MoveCameraToPosition(Vector3 targetPosition)
    {
        // Bewegen Sie die Kamera schrittweise zur Zielposition, ohne Interpolation
        transform.position = targetPosition;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
