using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PocketWatch : MonoBehaviour
{
    public int value = 1;
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1f;
    public float rotateSpeed = 50f;
    public AudioClip collectSound;

    private Vector3 startPos;
    private AudioSource audioSource;
    private bool collected = false;

    void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (collected) return;

        // Float up and down
        float newY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, newY, 0f);

        // Rotate
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player")) return;

        collected = true;

        // Add score
        CollectibleManager.Instance.AddScore(value);

        // Play pickup sound
        if (collectSound != null && audioSource != null)
            audioSource.PlayOneShot(collectSound);

        // Hide visual and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Destroy after sound plays
        Destroy(gameObject, collectSound != null ? collectSound.length : 0.1f);
    }
}
