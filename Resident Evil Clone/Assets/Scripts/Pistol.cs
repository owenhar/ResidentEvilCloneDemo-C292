using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    [SerializeField] GameObject bullet;
    [SerializeField] float bulletForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Fire()
    {
        base.Fire();
        GameObject go = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * bulletForce, ForceMode.Impulse);
    }

    protected override void Reload()
    {
        if (currentLoadedAmmo == ammoCapacity || currentSpareAmmo <= 0)
        {
            return;
        }
        if (currentSpareAmmo > 0)
        {
            currentLoadedAmmo = Mathf.Min(currentSpareAmmo, ammoCapacity + 1);
            currentSpareAmmo -= Mathf.Min(currentSpareAmmo, ammoCapacity + 1);
        }
        else
        {
            currentLoadedAmmo = Mathf.Min(currentSpareAmmo, ammoCapacity);
            currentSpareAmmo -= Mathf.Min(currentSpareAmmo, ammoCapacity);
        }
        
        if (currentLoadedAmmo <= 0)
        {
            canFire = false;
        }
        else
        {
            canFire = true;
        }
    }
}
