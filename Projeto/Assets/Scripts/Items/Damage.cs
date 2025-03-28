using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : MonoBehaviour
{
    public int damageAmount = 1;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Respawn();
            }
        }
    }
    void Respawn()
    {
        Vector3 newPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z));
        transform.position = newPosition;
    }
}