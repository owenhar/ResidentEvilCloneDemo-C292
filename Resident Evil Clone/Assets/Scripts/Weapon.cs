using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected int currentLoadedAmmo;
    [SerializeField] protected int currentSpareAmmo;
    [SerializeField] protected bool canFire;

    [SerializeField] protected Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Reload() //virtual methods can be overridden by classes that inherit Weapon
    {
        if (currentLoadedAmmo == ammoCapacity || currentSpareAmmo <= 0)
        {
            return;
        }

        currentLoadedAmmo = Mathf.Min(currentSpareAmmo, ammoCapacity);
        currentSpareAmmo -= Mathf.Min(currentSpareAmmo, ammoCapacity);

        if (currentLoadedAmmo <= 0 )
        {
            canFire = false;
        } else
        {
            canFire = true;
        }
    }

    protected virtual void Fire()
    {
        if (!(canFire && ammoCapacity > 0))
        {
            canFire = false;
            return;
        }
        RaycastHit hit;
        currentLoadedAmmo--;
        if (currentLoadedAmmo <= 0 )
        {
           canFire = false;
        }
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500) && hit.transform)
        {
            Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
            if (hit.transform.CompareTag("Zombie"))
            {
                hit.transform.GetComponent<ZombieController>().TakeDamage(1);
            }
        }
    }
}

