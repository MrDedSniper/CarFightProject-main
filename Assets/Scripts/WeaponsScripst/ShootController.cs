using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform gunTip;
    public float fireRate = 0.05f;
    private float nextFireTime = 0f;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] internal int _ammoCount = 100;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime && _ammoCount > 0)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
            _ammoCount--;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Прекращаем стрельбу
            // Дополнительные действия, если необходимо
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit))
        {
            // Здесь можно добавить логику для обработки попадания в объект
        }

        if (_muzzleFlash != null)
        {
            _muzzleFlash.Play(); // Воспроизводим партикл систему выстрела
        }
    }
}