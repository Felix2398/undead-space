using UnityEngine;
using TMPro;

public class TimeCalculator : MonoBehaviour
{
    public TextMeshProUGUI zeitAnzeige;
    private float vergangeneZeit = 0f;

    void Update()
    {
        vergangeneZeit += Time.deltaTime;
        ZeitAktualisieren();
    }

    void ZeitAktualisieren()
    {
        int stunden = (int)(vergangeneZeit / 3600f);
        int minuten = (int)((vergangeneZeit % 3600f) / 60f);
        int sekunden = (int)(vergangeneZeit % 60f);

        zeitAnzeige.text = string.Format(" {0:D2}:{1:D2}:{2:D2}", stunden, minuten, sekunden);
    }
}