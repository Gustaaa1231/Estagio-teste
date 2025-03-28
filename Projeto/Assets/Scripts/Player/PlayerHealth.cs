using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    private Animator animator;
    private HealthBar healthBar;
    private PlayerMovement playerMovement;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar = FindObjectOfType<HealthBar>();
        playerMovement = GetComponent<PlayerMovement>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        animator.SetTrigger("die");
        playerMovement.enabled = false;
        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        playerMovement.enabled = true;
        transform.position = Vector3.zero;
        animator.ResetTrigger("die");
        animator.Play("idle");
    }

    void Update()
    {

    }
}