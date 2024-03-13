using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private bool isSpeedBuffActive = false;

    private PlayerMovementController playerMovementController;
    private PlayerStateController playerStateController;
    public GameObject powerupUIManager;

    void Start() 
    {
        playerMovementController = gameObject.GetComponent<PlayerMovementController>(); 
        playerStateController = gameObject.GetComponent<PlayerStateController>(); 
    }

    public void ApplyHealthBuff(float amount)
    {
        gameObject.GetComponent<PlayerHealth>().ApplyHealthBuff(amount);
    }

    private Coroutine speedBuffCoroutine;

    public void ApplySpeedBuff(float amount, float time, Sprite img)
    {
        if (!isSpeedBuffActive)
        {
            powerupUIManager.GetComponent<PowerUpManager>().AddPowerUpPanel(img, time);
            isSpeedBuffActive = true;
            speedBuffCoroutine = StartCoroutine(SpeedBuffCoroutine(amount, time));
        }
    }

    private IEnumerator SpeedBuffCoroutine(float amount, float time)
    {
        isSpeedBuffActive = true;
        playerMovementController.SprintSpeed += amount;
        playerMovementController.RunningSpeed += amount;
        onSpeedChange(playerMovementController.SprintSpeed, playerMovementController.RunningSpeed);

        yield return new WaitForSeconds(time);

        playerMovementController.SprintSpeed -= amount;
        playerMovementController.RunningSpeed -= amount;
        onSpeedChange(playerMovementController.SprintSpeed, playerMovementController.RunningSpeed);
        isSpeedBuffActive = false;
    }

    private void onSpeedChange(float sprintSpeed, float runningSpeed)
    {
        playerMovementController.CurrentSpeed = playerStateController.GetCurrentState() switch
        {
            PlayerState.IS_RUNNING => runningSpeed,
            PlayerState.IS_SPRINTING => sprintSpeed,
            _ => 0
        };
    }

    public void ApplyDamageBuff(float amount, float time, Sprite img) {
        GameObject currentWeapon = gameObject.GetComponent<PlayerWeaponController>().getCurrentWeapon();
        WeaponController wc = currentWeapon.GetComponent<WeaponController>();
        if(!wc.IsWeaponDamageInscreased) powerupUIManager.GetComponent<PowerUpManager>().AddPowerUpPanel(img, time);
        wc.IncreaseDamage(amount, time);        
    }

    public void ApplyNewWeapon(GameObject weapon) {
        gameObject.GetComponent<PlayerWeaponController>().AddNewWeapon(weapon);
    }
}
