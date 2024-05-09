using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public int curBullet = 30;

    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject prefabBullet;

    public void OnShoot()
    {
        if (curBullet > 0)
        {
            curBullet--;
            GameObject projectile = Instantiate(prefabBullet, shootPos.position, Quaternion.identity);
            Vector3 direction = -transform.up; // Направление стрельбы вниз

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = direction * 10f; // Установите желаемую скорость пули
        }
    }
}
