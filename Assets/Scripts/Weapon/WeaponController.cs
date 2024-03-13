using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    List<WeaponListener> listeners = new List<WeaponListener>();
    
    private Transform firingPoint;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] public Sprite weaponSprite;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private int damagePerShot;
    [SerializeField] private int enemyPenetrationCount;
    [SerializeField] private int projectilesPerShot;
    [SerializeField] private int shotSpeed;
    [SerializeField] private float range;
    [SerializeField] private float horizontalSpread;
    [SerializeField] private float verticalSpread;

    [SerializeField] public float shootingCooldown = 0.5f; // Cooldown-Zeit in Sekunden zwischen den Schüssen
    private float shootingTimer; // Timer, der verfolgt, wann zuletzt geschossen wurde
    private bool isWeaponDamageInscreased = false;
    public bool hasInfiniteAmmo = false;

    public bool IsWeaponDamageInscreased
    {
        get { return isWeaponDamageInscreased; }
    }


    [SerializeField] private int maxAmmoCount;
    [SerializeField] private int startAmmoCount;
    [SerializeField] private int currentAmmoCount;

    private void FixedUpdate() 
    {
        shootingTimer += Time.deltaTime;
    }

    public void SetFiringPoint(Transform firingPoint)
    {
        this.firingPoint = firingPoint;
    }

    public void Fire(float damageMultiplier)
    {
        if (shootingTimer < shootingCooldown) return;
        shootingTimer = 0f;

        muzzleFlash.Play();

        for (int i = 0; i < projectilesPerShot; i++)
        {
            ShootProjectile(damageMultiplier);
        }

        if (!hasInfiniteAmmo)
        {
            currentAmmoCount--;
            ChangeAmmoCount(currentAmmoCount);
        }
    }

    public void ShootProjectile(float damageMultiplier)
    {
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.SetDirection(calcProjectileSpread());
        projectileController.SetSpeed(shotSpeed);
        projectileController.SetDamage(damagePerShot * damageMultiplier);
        Debug.Log(damagePerShot * damageMultiplier);
        projectileController.SetRange(range);
        projectileController.SetEnemyPenetrationCount(enemyPenetrationCount);

    }

    private Vector3 calcProjectileSpread()
    {
        float horizontalOffset = Random.Range(-horizontalSpread, horizontalSpread);
        float verticalOffset = Random.Range(-verticalSpread, verticalSpread);
        Vector3 direction = Quaternion.Euler(verticalOffset, horizontalOffset, verticalOffset) * firingPoint.forward;
        return direction;
    }

    private void ChangeAmmoCount(int newAmmoCount)
    {
        currentAmmoCount = Mathf.Min(newAmmoCount, maxAmmoCount);
        if (currentAmmoCount <= 0)
        {
            removeWeapon();
        }

        listeners.ForEach(listener => listener.OnAmmoChange(currentAmmoCount, maxAmmoCount));
    }

    private void removeWeapon()
    {
        foreach (WeaponListener listener in listeners)
        {
            listener.OnWeaponDeletion(this.gameObject);
        }
        Destroy(this.gameObject);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmoCount;
    }

    public int GetMaxAmmo()
    {
        return maxAmmoCount;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }


    public void AddListener(WeaponListener weaponListener)
    {
        listeners.Add(weaponListener);
    }

    public void SetCurrentAmmoToMaxAmmo()
    {
        ChangeAmmoCount(maxAmmoCount);
    }

    public void SetCurrentAmmoToStartAmmo()
    {
        ChangeAmmoCount(startAmmoCount);
    }
}
