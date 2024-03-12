using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponPanel : MonoBehaviour, WeaponListener
{
    [SerializeField] Image weaponImage;
    [SerializeField] Image fillImage;
    [SerializeField] Image dimImage;

    public void SetWeapon(GameObject weapon)
    {
        WeaponController weaponController = weapon.GetComponent<WeaponController>();
        weaponController.AddListener(this);
        weaponImage.sprite = weaponController.weaponSprite;

        float currentAmmoCount = weaponController.GetCurrentAmmo();
        float maxAmmoCount = weaponController.GetMaxAmmo();
        SetFill(currentAmmoCount, maxAmmoCount);
    }

    public void OnAmmoChange(int currentAmmoCount, int maxAmmoCount)
    {
        SetFill((float) currentAmmoCount, (float) maxAmmoCount);
    }

    private void SetFill(float current, float max)
    {
        float fillAmount = current / max;
        if (fillAmount >= 0.5) fillImage.color = Color.green;
        else if (fillAmount >= 0.25) fillImage.color = Color.yellow;
        else fillImage.color = Color.red;
        fillImage.fillAmount = fillAmount;
    }

    public void SetActive(bool isActive)
    {
        dimImage.gameObject.SetActive(!isActive);
    }
}
