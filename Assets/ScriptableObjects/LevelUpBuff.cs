using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/LevelUpBuff")]
public class LevelUpBuff : PowerupEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerPowerUp>().ApplyLevelUpBuff(amount);
        GameManager.GetInstance().IncrementHighscore(1000);
    }
}
