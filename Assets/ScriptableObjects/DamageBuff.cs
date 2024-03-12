using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/DamageBuff")]
public class DamageBuff : PowerupEffect
{
    public float amount;
    public float time;
    public Sprite image;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerPowerUp>().ApplyDamageBuff(amount, time, image);
        GameManager.GetInstance().IncrementHighscore(100);
    }
}
