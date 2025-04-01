using UnityEngine;

public class BoatFoam : MonoBehaviour
{
    public ParticleSystem foamEffect;  
    public Rigidbody boatRb;           
    public float minSpeedToSpawn = 1f; 

    void Update()
    {
        if (boatRb.linearVelocity.magnitude > minSpeedToSpawn)
        {
            if (!foamEffect.isPlaying)
                foamEffect.Play();
        }
        else
        {
            if (foamEffect.isPlaying)
                foamEffect.Stop();
        }
    }
}
