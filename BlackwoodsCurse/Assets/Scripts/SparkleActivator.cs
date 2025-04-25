using UnityEngine;

public class SparkleActivator : MonoBehaviour
{
    public ParticleSystem sparkleEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sparkleEffect != null)
            sparkleEffect.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && sparkleEffect != null)
            sparkleEffect.Stop();
    }
}
