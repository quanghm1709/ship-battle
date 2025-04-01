using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float acceleration = 5f;      
    public float maxSpeed = 10f;         
    public float turnSpeed = 50f;        
    public float waterDrag = 0.98f;      
    public float angularDrag = 0.95f;    

    [SerializeField] private Rigidbody rb;

    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = waterDrag;             
        rb.angularDamping = angularDrag; 
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Vertical");   
        turnInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
        }

        float turnSpeedAdjusted = (moveInput != 0) ? turnSpeed * (rb.linearVelocity.magnitude / maxSpeed) : turnSpeed * 0.5f;
        float turn = turnInput * turnSpeedAdjusted * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    public void SetSpeed(float speed)
    {
        moveInput = speed;
    }

    public void SetTurnSpeed(float turnSpeed)
    {
        turnInput = turnSpeed;
    }
}
