using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/WeaponBuff")]
public class WeaponPowerUp : PowerupEffect
{
    public GameObject weapon;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerPowerUp>().ApplyNewWeapon(weapon);
        GameManager.GetInstance().IncrementHighscore(200);
    }
}
