using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject zombiePrefab;

    void Start()
    {
        MyEvents.zombieKilled.AddListener(OnZombieKill);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnZombieKill()
    {
        Debug.Log("Spawning Zombie");
        Instantiate(zombiePrefab, transform.position, Quaternion.identity);
    }
}
