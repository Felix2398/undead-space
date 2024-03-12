using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreAnimation : MonoBehaviour
{
  public float animationTime = 0.5f; // Dauer der Animation
    public Vector3 enlargedScale = new Vector3(1.5f, 1.5f, 1f); // Vergrößerte Skala
    private Vector3 originalScale; // Ursprüngliche Skala
    private TextMeshProUGUI highscoreText;
    private int lastHighscore = 0;

    void Start()
    {
        highscoreText = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        int currentHighscore = int.Parse(highscoreText.text);

        if (currentHighscore != lastHighscore)
        {
            StopCoroutine("AnimateText");
            StartCoroutine("AnimateText");
            lastHighscore = currentHighscore;
        }
    }

    IEnumerator AnimateText()
    {
        float timer = 0;

        while (timer <= animationTime)
        {
            // Skaliere den Text über die Zeit
            transform.localScale = Vector3.Lerp(originalScale, enlargedScale, timer / animationTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Stelle sicher, dass die Skalierung am Ende der Animation korrekt ist
        transform.localScale = enlargedScale;

        // Skaliere zurück zur ursprünglichen Größe
        timer = 0;
        while (timer <= animationTime)
        {
            transform.localScale = Vector3.Lerp(enlargedScale, originalScale, timer / animationTime);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
