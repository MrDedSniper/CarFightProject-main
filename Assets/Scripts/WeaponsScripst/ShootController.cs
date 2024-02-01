using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform gunTip; // точка выстрела
    public GameObject bulletPrefab; // префаб пули
    public float bulletSpeed = 10f; // скорость пули
    public float fireRate = 0.5f; // скорострельность
    private float nextFireTime = 0f; // время следующего выстрела
    private Animator animator; // аниматор

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot"); // запускаем анимацию выстрела

        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation); // создаем пулю
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * bulletSpeed; // задаем скорость пули

        Destroy(bullet, 3f); // удаляем пулю через 3 секунды
    }
}