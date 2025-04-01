using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float acceleration = 5f;      
    public float maxSpeed = 10f;         
    public float turnSpeed = 50f;        
    public float waterDrag = 0.98f;      
    public float angularDrag = 0.95f;    

    [SerializeField] private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = waterDrag;             
        rb.angularDamping = angularDrag; 
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");   
        float turnInput = Input.GetAxis("Horizontal");

        if (moveInput != 0)
        {
            rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
        }

        float turnSpeedAdjusted = (moveInput != 0) ? turnSpeed * (rb.linearVelocity.magnitude / maxSpeed) : turnSpeed * 0.5f;
        float turn = turnInput * turnSpeedAdjusted * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }
}
