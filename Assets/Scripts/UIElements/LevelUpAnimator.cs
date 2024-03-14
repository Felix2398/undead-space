using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpAnimator : MonoBehaviour
{
    public Image levelImage; // Referenz zum schwarzen Bild UI-Element
    public float animationDuration = 1.0f; // Dauer der Animation

    void Start()
    {
    }

    public void StartLevelUpAnimation()
    {
        StartCoroutine(AnimateLevelUp());
    }

    IEnumerator AnimateLevelUp()
    {
        float timer = 0;
        Color originalColor = levelImage.color;
        Vector3 originalScale = levelImage.transform.localScale;

        while (timer < animationDuration)
        {
            // Animation Logik hier
            float progress = timer / animationDuration;
            float scaleFactor = 1 + 0.5f * Mathf.Sin(Mathf.PI * progress); // Skalierung zwischen 1 und 1.5

            // Ändere Farbe und Skalierung über Zeit
            levelImage.color = Color.Lerp(originalColor, Color.white, Mathf.PingPong(timer * 2, 1));
            levelImage.transform.localScale = originalScale * scaleFactor;

            timer += Time.deltaTime;
            yield return null;
        }

        // Setze die Eigenschaften auf ihre ursprünglichen Werte zurück
        levelImage.color = originalColor;
        levelImage.transform.localScale = originalScale;
    }
}
