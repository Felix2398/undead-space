using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    public AudioSource audioSource;

    public float lifetime = 15f; // Lebensdauer des Power-Ups in Sekunden

    GameObject player;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnDestroy() {
        WaveSpawner.GetInstance().OnPowerUp();
    }

    private void OnTriggerEnter(Collider collider) {

        if(collider.gameObject.tag == "Player") {
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
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
