using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);

        BoatHealth boatHealth = collision.gameObject.GetComponent<BoatHealth>();
        if(boatHealth != null)
        {
            boatHealth.UpdateHealth(-1);
        }

        Destroy(gameObject);
    }
}
