using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour, PlayerStateListener, WeaponListener
{
    List<PlayerWeaponListener> listeners = new List<PlayerWeaponListener>();

    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject weaponHolderObject;
    [SerializeField] private GameObject startWeaponPrefab;
    [SerializeField] private List<GameObject> startWeaponPrefabs;

    private GameObject currentWeapon;
    private List<GameObject> currentWeapons = new List<GameObject>();
    
    private int weaponIndex;
    private bool weaponIsEnabled;

    private void Awake() 
    {
        GetComponent<PlayerStateController>().AddListener(this);
    }

    void Start()
    {
        InstantiateWeapon(startWeaponPrefab);
        startWeaponPrefabs.ForEach(w => InstantiateWeapon(w));

        weaponIndex = 0;
        currentWeapon = currentWeapons[weaponIndex];
        EquipWeapon();
        currentWeapon.SetActive(true);
    }

    private void InstantiateWeapon(GameObject weaponPrefab)
    {
        GameObject weapon = Instantiate(weaponPrefab, weaponHolderObject.transform);
        currentWeapons.Add(weapon);
        weapon.SetActive(false);

        WeaponController weaponController = weapon.GetComponent<WeaponController>();
        weaponController.SetFiringPoint(firingPoint);
        weaponController.AddListener(this);
        
        listeners.ForEach(listeners => listeners.OnNewWeaponAdded(weapon));
    }

    public void EquipNextWeapon()
    {
        weaponIndex = (weaponIndex + 1) % currentWeapons.Count;
        EquipWeapon();
    }

    public void EquipPreviousWeapon()
    {
        if (weaponIndex == 0) weaponIndex = currentWeapons.Count - 1;
        else weaponIndex -= 1;
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        currentWeapon.SetActive(false);
        currentWeapon = currentWeapons[weaponIndex];
        currentWeapon.SetActive(true);

        listeners.ForEach(listener => listener.OnEquippedWeaponChange(currentWeapon));
    }
  
    public void FireCurrentWeapon()
    {
        if (!weaponIsEnabled) return;
        currentWeapon.GetComponent<WeaponController>().Fire();
    }

    public void OnWeaponStatusChange(bool isEnabled)
    {
        weaponIsEnabled = isEnabled;
        weaponHolderObject.SetActive(isEnabled);
    }

    public void OnWeaponDeletion(GameObject weapon)
    {
        currentWeapons.Remove(weapon);
        listeners.ForEach(listener => listener.OnWeaponRemoved(weapon));

        if (currentWeapons.Count == 0) InstantiateWeapon(startWeaponPrefab);
        EquipNextWeapon();
    }

    public void ApplyDamageBuff(float amount, float time) {

        // foreach (var weapon in startWeaponPrefabs)
        // {
        //     weapon.GetComponent<WeaponController>().IncreaseDamage(amount, time);
        // }

        // currentWeaponItem.weaponPrefab.GetComponent<WeaponController>().IncreaseDamage(amount, time);
    }


    public void SetWeaponOwned(WeaponType type) {

        /*
        foreach (WeaponItem weaponPrefab in weaponsCollection)
        {
            if(weaponPrefab.type == type) {
                weaponPrefab.isOwned = true;
                // weaponUIManager.ApplyWeaponOwned(type);
            }
        }
        */
    }

    public void AddListener(PlayerWeaponListener playerWeaponListener)
    {
        this.listeners.Add(playerWeaponListener);
    }

    public void AddNewWeapon(GameObject newWeaponPrefab)
    {
        WeaponType weaponType = newWeaponPrefab.GetComponent<WeaponController>().GetWeaponType();
        if (WeaponTypeExists(weaponType)) return;
        else InstantiateWeapon(newWeaponPrefab);
    }

    private bool WeaponTypeExists(WeaponType weaponType)
    {
        foreach (GameObject weapon in currentWeapons)
        {
            if (weapon.GetComponent<WeaponController>().GetWeaponType() == weaponType)
            {
                return true;
            }
        }
        return false;
    }
}
