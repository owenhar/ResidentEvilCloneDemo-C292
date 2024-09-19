using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float maxHealth = 10f;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        currentHealth = maxHealth;
        UpdateGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        UpdateGUI();
    }

    void UpdateGUI()
    {
        healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
