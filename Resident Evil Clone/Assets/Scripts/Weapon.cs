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
    [SerializeField] public Enums.MagazineType magazineType;

    [SerializeField] protected Magazine magazine;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Reload(Magazine mag) //virtual methods can be overridden by classes that inherit Weapon
    {
        magazine = mag;
        //if (currentLoadedAmmo == ammoCapacity || currentSpareAmmo <= 0)
        //{
        //    return;
        //}

        //currentLoadedAmmo = Mathf.Min(currentSpareAmmo, ammoCapacity);
        //currentSpareAmmo -= Mathf.Min(currentSpareAmmo, ammoCapacity);

        //if (currentLoadedAmmo <= 0 )
        //{
        //    canFire = false;
        //} else
        //{
        //    canFire = true;
        //}
    }

    public virtual void Fire()
    {
        if (magazine == null || magazine.GetRounds() <= 0)
        {
            return;
        }
        magazine.RemoveRound();
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
                MyEvents.shotFired.Invoke(true);
                hit.transform.GetComponent<ZombieController>().TakeDamage(1);
            } else
            {
                MyEvents.shotFired.Invoke(false);
            }
        } else
        {
            MyEvents.shotFired.Invoke(false);
        }
    }

    public virtual int CheckAmmo()
    {
        if (magazine == null)
        {
            return 0;
        }

        return magazine.GetRounds();
    }
}

