using System.Collections;
using UnityEngine;

public class BoatAI : MonoBehaviour
{
    public float acceleration = 5f;      
    public float maxSpeed = 10f;         
    public float turnSpeed = 50f;        
    public float changeDirectionTime = 3f; 

    public GameObject bulletPrefab;      
    public Transform firePoint;          
    public float bulletSpeed = 20f;      
    public float shootingInterval = 2f;  
    public Transform playerBoat;         

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    private float directionTimer;
    private float shootingTimer;
    private bool isActive = true;

    void Start()
    {
        playerBoat = FindFirstObjectByType<BoatController>().transform;
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0.98f;         
        rb.angularDamping = 0.95f;  
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        if (!isActive) return;
        directionTimer -= Time.fixedDeltaTime;
        if (directionTimer <= 0)
        {
            ChooseNewDirection();
        }

        rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);

        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);

        shootingTimer -= Time.fixedDeltaTime;
        if (shootingTimer <= 0)
        {
            ShootAtPlayer();
            shootingTimer = shootingInterval;
        }
    }

    void ChooseNewDirection()
    {
        directionTimer = Random.Range(1f, changeDirectionTime); 
        moveInput = Random.Range(0.5f, 1f); 
        turnInput = Random.Range(-1f, 1f); 
    }

    void ShootAtPlayer()
    {
        if (playerBoat == null) return;

        Vector3 directionToPlayer = (playerBoat.position - firePoint.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, lookRotation);

        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.linearVelocity = directionToPlayer * bulletSpeed;

        Destroy(bullet, 5f);
    }

    public void DisableBoat()
    {
        StartCoroutine(IDisableBoat());
    }

    IEnumerator IDisableBoat()
    {
        isActive = false;
        rb.useGravity = true;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}