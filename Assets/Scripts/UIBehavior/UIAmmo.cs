using TMPro;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private ShootController _shootController;
    
    private void Start()
    {
        _shootController = FindObjectOfType<ShootController>();
    }

    internal void UpdateAmmo()
    {
        _ammoText.text = _shootController._ammoCount.ToString();
    }
}
