using UnityEngine;

public class BoatShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform firePoint;      
    public float bulletSpeed = 20f;
    public Camera mainCamera;
    public LayerMask aimLayerMask;
    [Range(0f, 100f)]
    public float spreadPercentage = 0f;

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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); 
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        }
    }
}
