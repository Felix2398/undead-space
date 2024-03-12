using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisappear : MonoBehaviour
{
    public float lifetime; // Die Zeit in Sekunden, nach der das Bild verschwinden soll

    void Start()
    {
        StartCoroutine(DisappearAfterTime(lifetime));
    }

    private IEnumerator DisappearAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject); // Verwende diese Zeile statt der oberen, um das GameObject zu zerstören
    }
}
