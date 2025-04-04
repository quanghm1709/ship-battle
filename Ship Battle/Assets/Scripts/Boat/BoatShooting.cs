﻿using UnityEngine;

public class BoatShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform firePoint;      
    public float bulletSpeed = 20f;
    public Camera mainCamera;
    public LayerMask aimLayerMask;
    public bool isRotateWeap = false;

    [Range(0f, 100f)]
    public float spreadPercentage = 0f;

    [Header("Turret Rotation Limits")]
    public float minRotationY = -45f;
    public float maxRotationY = 45f;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    void Update()
    {
        RotateTurret();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        float spreadAmount = spreadPercentage / 100f * 10f;
        float randomYaw = Random.Range(-spreadAmount, spreadAmount);
        float randomPitch = Random.Range(-spreadAmount, spreadAmount);

        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = spreadRotation * firePoint.forward * bulletSpeed;
        Destroy(bullet, 5f);
    }

    void RotateTurret()
    {
        if (!isRotateWeap) return;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float targetY = lookRotation.eulerAngles.y;

            if (targetY < minRotationY || targetY > maxRotationY)
            {
                return;
            }

            float clampedY = Mathf.Clamp(targetY, minRotationY, maxRotationY);
            transform.rotation = Quaternion.Euler(0, clampedY, 0);
        }
    }

    public Color rangeColor;

    void OnDrawGizmos()
    {
        if (firePoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.forward * 5f);

        Gizmos.color = rangeColor;
        Vector3 position = transform.position;
        float radius = 5f;
        int segments = 30;
        float angleStep = (maxRotationY - minRotationY) / segments;
        Vector3 prevPoint = position + Quaternion.Euler(0, minRotationY, 0) * Vector3.forward * radius;
        for (int i = 1; i <= segments; i++)
        {
            float angle = minRotationY + angleStep * i;
            Vector3 nextPoint = position + Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}
