using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerWeaponListener
{
    public void OnEquippedWeaponChange(GameObject currentWeapon);
    public void OnNewWeaponAdded(GameObject newWeapon);
    public void OnWeaponRemoved(GameObject removedWeapon);
}
