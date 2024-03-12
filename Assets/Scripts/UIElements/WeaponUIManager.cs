using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class WeaponUIManager : MonoBehaviour, PlayerWeaponListener
{
    [SerializeField] private PlayerWeaponController playerWeaponController;
    [SerializeField] private GameObject panelPrefab;
    private Dictionary<GameObject, GameObject> instantiatedPanels = new Dictionary<GameObject, GameObject>();

    private void Awake() 
    {
        playerWeaponController.AddListener(this);
    }

    public void OnEquippedWeaponChange(GameObject currentWeapon)
    {
        foreach (GameObject value in instantiatedPanels.Values)
        {
            value.GetComponent<WeaponPanel>().SetActive(false);
        }

        instantiatedPanels[currentWeapon].GetComponent<WeaponPanel>().SetActive(true);
    }

    public void OnNewWeaponAdded(GameObject newWeapon)
    {
        GameObject panelInstance = Instantiate(panelPrefab);
        instantiatedPanels.Add(newWeapon, panelInstance);

        panelInstance.transform.SetParent(gameObject.transform, false);
        panelInstance.GetComponent<WeaponPanel>().SetWeapon(newWeapon);
        panelInstance.GetComponent<WeaponPanel>().SetActive(false);
    }

    public void OnWeaponRemoved(GameObject removedWeapon)
    {
        GameObject panel = instantiatedPanels[removedWeapon];
        Destroy(panel);
        instantiatedPanels.Remove(removedWeapon);
    }
}
