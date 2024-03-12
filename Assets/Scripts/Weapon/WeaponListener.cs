using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponListener
{
    public void OnWeaponDeletion(GameObject weapon)
    {
        return;
    }

    public void OnAmmoChange(int currentAmmoCount, int maxAmmoCount)
    {
        return;
    }
}
