using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerUp : MonoBehaviour
{
    private bool isSpeedBuffActive = false;
    private bool isDamageIncreased = false;

    private PlayerMovementController playerMovementController;
    private PlayerStateController playerStateController;
    public GameObject powerupUIManager;

    public TextMeshProUGUI levelLabel;
    public Image levelUpBackground;

    private int playerLevel = 1;

    void Start() 
    {
        playerMovementController = gameObject.GetComponent<PlayerMovementController>(); 
        playerStateController = gameObject.GetComponent<PlayerStateController>(); 
        levelLabel.text = playerLevel.ToString();
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

        PlayerWeaponController weaponController = gameObject.GetComponent<PlayerWeaponController>();

        if(!isDamageIncreased) {
            isDamageIncreased = true;
            powerupUIManager.GetComponent<PowerUpManager>().AddPowerUpPanel(img, time);
            float defaultDamageMultiplier = weaponController.damageMultiplier;
            weaponController.damageMultiplier = weaponController.damageMultiplier * amount;
            StartCoroutine(ResetDamageAfterTime(defaultDamageMultiplier, time, weaponController));
        }
    }

    private IEnumerator ResetDamageAfterTime(float defaultDamageMultiplier, float time, PlayerWeaponController wc)
    {
        yield return new WaitForSeconds(time);
        isDamageIncreased = false;
        wc.damageMultiplier = defaultDamageMultiplier;
    }

    public void ApplyNewWeapon(GameObject weapon) {
        gameObject.GetComponent<PlayerWeaponController>().AddNewWeapon(weapon);
    }

    public void ApplyLevelUpBuff(float amount) {

        PlayerWeaponController weaponController = gameObject.GetComponent<PlayerWeaponController>();
        weaponController.damageMultiplier += amount;
        playerLevel++;
        levelLabel.text = playerLevel.ToString();
        levelUpBackground.GetComponent<LevelUpAnimator>().StartLevelUpAnimation();
    }
}
