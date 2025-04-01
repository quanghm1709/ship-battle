using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
