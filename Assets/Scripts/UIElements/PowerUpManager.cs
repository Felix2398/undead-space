using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    
    public GameObject powerUpPanelPrefab; // Referenz zum Power-Up-Panel-Prefab

    // Methode zum Hinzufügen eines Power-Up-Panels zum Container
    public void AddPowerUpPanel(Sprite img, float time)
    {
        GameObject panelInstance = Instantiate(powerUpPanelPrefab);
        panelInstance.transform.SetParent(gameObject.transform, false);
        panelInstance.GetComponent<PowerUpPanel>().SetPanelImage(img);
        panelInstance.GetComponent<PowerUpPanel>().lifetime = time;
        panelInstance.GetComponent<PowerUpPanel>().SetActive(true);
    }
}
