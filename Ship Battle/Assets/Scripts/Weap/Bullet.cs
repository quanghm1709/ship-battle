using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        collision.gameObject.GetComponent<BoatHealth>().UpdateHealth(-1);
        Destroy(gameObject);
    }
}
