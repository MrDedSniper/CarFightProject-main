using System;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public ColumnBreakScript _columnBreakScript;
    
    public Transform gunTip;
    public float fireRate = 0.1f; // Задержка между выстрелами
    private float nextFireTime = 0f;

    private bool _isShooting;
    
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Light _lightFromShot;
    [SerializeField] internal int _ammoCount = 100;

    private UIAmmo _uiAmmo;

    private void Start()
    {
        _uiAmmo = FindObjectOfType<UIAmmo>();
        _columnBreakScript = FindObjectOfType<ColumnBreakScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isShooting = true;
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            _muzzleFlash.Stop();
            _isShooting = false;
            _lightFromShot.enabled = false;
        }

        if (_isShooting && Time.time > nextFireTime && _ammoCount > 0)
        {
            Shoot();
            _muzzleFlash.Play();
            nextFireTime = Time.time + fireRate;

            if ((Time.time * 10) % 2 < 1)
            {
                _lightFromShot.enabled = !_lightFromShot.enabled;
            }
        }
    }

    private void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hitInfo))
        {
            ColumnBreakScript column = hitInfo.collider.GetComponent<ColumnBreakScript>();
            if (column != null)
            {
                column.TakeDamage(1);
            }

            RaycastShootHitPointTracker.SetLastHitPoint(hitInfo.point);

            _ammoCount--;
            _uiAmmo.UpdateAmmo();
        }
    }
}