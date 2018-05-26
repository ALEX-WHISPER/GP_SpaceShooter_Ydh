﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootingSkills {
    public Skill_PowerUp m_PowerUp;
    public Skill_RateUp m_RateUp;
    public Skill_UltiDiamond m_UltiDiamond;
}

//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    public ShootingSkills m_ShootingSkills;

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1;
    [Range(1, 4)]
    public int fireRatePower = 1;

    public Guns guns;
    bool shootingIsActive = true; 
    [HideInInspector] public int maxweaponPower = 4; 
    public static PlayerShooting instance;

    private GameObject m_UltiDiamondInstance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    private void OnEnable() {
        if (m_ShootingSkills.m_PowerUp) m_ShootingSkills.m_PowerUp.FireSkill_PowerUp += UpdatePowerLevel;
        if (m_ShootingSkills.m_UltiDiamond) m_ShootingSkills.m_UltiDiamond.FireSkill_UltiDiamond += FireUlti_Diamond;
        if (m_ShootingSkills.m_RateUp) m_ShootingSkills.m_RateUp.FireSkill_RateUp += UpdateFireRateLevel;
    }

    private void OnDisable() {
        if (m_ShootingSkills.m_PowerUp) m_ShootingSkills.m_PowerUp.FireSkill_PowerUp -= UpdatePowerLevel;
        if (m_ShootingSkills.m_UltiDiamond) m_ShootingSkills.m_UltiDiamond.FireSkill_UltiDiamond -= FireUlti_Diamond;
        if (m_ShootingSkills.m_RateUp) m_ShootingSkills.m_RateUp.FireSkill_RateUp -= UpdateFireRateLevel;
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();                                                         
                nextFire = Time.time + 1 / fireRate;
            }
        }
    }

    private void FireRatePowerUp() {
        switch(fireRatePower) {
            case 1:
                break;
            case 2:
            case 3:
            case 4:
                fireRate += 1f;
                break;
            default:
                break;
        }
    }
    
    //method for a shot
    void MakeAShot() 
    {
        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
        {
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;
            case 2:
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
                break;
            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                break;
            case 4:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                break;
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        PoolManager.GetInstance.ReuseObject(lazer, pos, Quaternion.Euler(rot));
        //Instantiate(lazer, pos, Quaternion.Euler(rot));
    }

    #region Skill Handler
    private void UpdateFireRateLevel(int levelValue) {
        fireRatePower = levelValue;
        FireRatePowerUp();
    }

    private void UpdatePowerLevel(int powerValue) {
        weaponPower = powerValue;
    }

    private void FireUlti_Diamond(UltiDiamondInfo ultiInfo) {
        StartCoroutine(CreateUltiDiamond(ultiInfo));
    }

    IEnumerator CreateUltiDiamond(UltiDiamondInfo ultiInfo) {
        if (m_UltiDiamondInstance && !m_UltiDiamondInstance.activeSelf) {
            m_UltiDiamondInstance.SetActive(true);
        } else {
            Debug.Log("m_UltiDiamondInstance is null, now instantiate it");
            m_UltiDiamondInstance = Instantiate(ultiInfo.m_UltiDiamondPrefab, transform.position, Quaternion.identity, transform);
        }

        yield return new WaitForSeconds(ultiInfo.m_UltiDuration);

        m_UltiDiamondInstance.SetActive(false);
    }
    #endregion
}
