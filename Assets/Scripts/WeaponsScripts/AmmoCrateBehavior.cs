using UnityEngine;

public class AmmoCrateBehavior : MonoBehaviour
{
    private ShootController _shootController;
    private void Start()
    {
        _shootController = FindObjectOfType<ShootController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            Destroy(gameObject);
            _shootController.GiveMaxAmmo();
        }
    }
}
