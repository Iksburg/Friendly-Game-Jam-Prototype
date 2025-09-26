using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float damage = 20f; // Урон пистолета
    [SerializeField] private float range = 100f; // Дальность выстрела
    [SerializeField] private float fireRate = 0.3f; // Задержка между выстрелами
    [SerializeField] private Camera fpsCam; // Камера игрока

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}