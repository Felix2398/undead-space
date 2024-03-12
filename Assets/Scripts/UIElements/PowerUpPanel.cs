using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPanel : MonoBehaviour
{
    [SerializeField] Image powerUpImage;
    [SerializeField] Image fillImage;
    [SerializeField] Image dimImage;

    public Image PowerUpImage
    {
        get { return powerUpImage; }
        set { powerUpImage = value; }
    }

    public float lifetime; 

    void Start() {

        StartCoroutine(UpdateFillAmount());
    }

    public void SetPanelImage(Sprite imageSprite) {

        powerUpImage.sprite = imageSprite;
    }

    private IEnumerator UpdateFillAmount() {

        float timeElapsed = 0f;

        while (timeElapsed < lifetime) {

            timeElapsed += Time.deltaTime;
            float fillAmount = 1 - (timeElapsed / lifetime);
            fillImage.fillAmount = fillAmount;

            if (fillAmount >= 0.5) fillImage.color = Color.green;
            else if (fillAmount >= 0.25) fillImage.color = Color.yellow;
            else fillImage.color = Color.red;

            yield return null; 
        }

        Destroy(gameObject); 
    }

    public void SetActive(bool isActive)
    {
        dimImage.gameObject.SetActive(!isActive);
    }
}
