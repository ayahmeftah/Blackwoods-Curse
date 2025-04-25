using UnityEngine;

public class SparkleActivator : MonoBehaviour
{
    public ParticleSystem sparkleEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sparkleEffect != null)
        {
            if (DrawerLock.isDrawerUnlocked) // Only play if drawer is unlocked
            {
                sparkleEffect.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && sparkleEffect != null)
        {
            // Always stop when leaving (no need to check if unlocked)
            sparkleEffect.Stop();
        }
    }
}