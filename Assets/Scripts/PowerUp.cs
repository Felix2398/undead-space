using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    public AudioSource audioSource;

    public MeshRenderer meshRenderer;

    private float blinkStart = 3f;

    private float lifetime = 15f;

    void Start()
    {
        Destroy(gameObject, lifetime);
        
        StartCoroutine(LifetimeCountdown());
    }

    IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(lifetime - blinkStart);
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        float endTime = Time.time + blinkStart;
        while (Time.time < endTime)
        {
            // Toggle die Sichtbarkeit des MeshRenderers
            meshRenderer.enabled = !meshRenderer.enabled;

            // Warte einen kurzen Moment vor dem nächsten Blinken
            yield return new WaitForSeconds(0.2f);
        }

        // Stelle sicher, dass das Power-Up am Ende sichtbar ist
        meshRenderer.enabled = true;
    }

    void OnDestroy() {
        WaveSpawner.GetInstance().OnPowerUp();
    }

    private void OnTriggerEnter(Collider collider) {

        if(collider.gameObject.tag == "Player") {
            
            meshRenderer.enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            powerupEffect.Apply(collider.gameObject);
            audioSource.Play();
            StartCoroutine(DestroyAfterSound());
        }
    }

    IEnumerator DestroyAfterSound() {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(gameObject);
    }
}
