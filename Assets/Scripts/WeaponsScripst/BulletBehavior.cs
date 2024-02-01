using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //[SerializeField] private PlayerHealth _playerHealth;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamagingEnvironment"))
        {
            Destroy(gameObject);
        }
        
        else if (collision.gameObject.CompareTag("PlayerCar"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(6f);
        }
    }
}
