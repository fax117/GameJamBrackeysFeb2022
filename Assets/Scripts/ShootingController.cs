using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Bullet options")] 
    public float rpm = 1f;

    [Header("Bullet GameObjects")] 
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform bulletSpawn;

    [Header("Effects")]
    [SerializeReference] private ParticleSystem _muzzleFlash;

    private float _nextFire = 0f;
    public bool canShoot { get; set; }
    
    public void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + 60/rpm;
            GameObject g = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            if (gameObject.CompareTag("Player")) Instantiate(_muzzleFlash, bulletSpawn.position, transform.rotation);
        }
    }
}
