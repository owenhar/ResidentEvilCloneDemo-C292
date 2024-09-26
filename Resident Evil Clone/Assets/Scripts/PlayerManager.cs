using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float maxHealth = 10f;
    [SerializeField] TextMeshProUGUI ammoText;

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
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateGUI()
    {
        healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    public void UpdateAmmo(int ammo)
    {
        ammoText.text = "Ammo: " + ammo.ToString();
    }
}
