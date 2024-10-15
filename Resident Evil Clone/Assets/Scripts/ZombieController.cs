using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Zombie took damage: " + damage);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            MyEvents.zombieKilled.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.instance.DamagePlayer(1);
        }
    }
}
