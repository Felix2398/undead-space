using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    
    public GameObject panel; // Referenz zum Container-Panel

    public void CreateTimedImage(Sprite imageSprite, float time)
    {
        // Erstelle ein neues GameObject für das Bild
        GameObject imageObject = new GameObject("TimedImage");
        imageObject.transform.SetParent(panel.transform, false); // Füge es als Kind des Panels hinzu

        // Füge eine Image-Komponente hinzu und weise das Sprite zu
        Image imageComponent = imageObject.AddComponent<Image>();
        imageComponent.sprite = imageSprite;

        RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(150, 300);

        TimeDisappear timedDisappear = imageObject.AddComponent<TimeDisappear>();
        timedDisappear.lifetime = time; // Setze die Lebenszeit basierend auf dem übergebenen Parameter
    }
}
