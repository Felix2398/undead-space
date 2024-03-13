using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicShuffler : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> playlist = new List<AudioClip>();
    private List<AudioClip> playHistory = new List<AudioClip>();
    private int historyLimit = 2; // Anpassen, um mehr oder weniger der letzten Lieder zu berücksichtigen
    private bool stopCoroutine = false;
    static MusicShuffler instance;

    void Start()
    {
        StartCoroutine(PlayShuffledMusic());
    }


    public static MusicShuffler GetInstance() {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    IEnumerator PlayShuffledMusic()
    {
        while (true)
        {
            if (stopCoroutine)
            {
                yield break; // Beendet die Coroutine, wenn stopCoroutine true ist
            }

            var playlistToShuffle = new List<AudioClip>(playlist);
            foreach (var track in playHistory)
            {
                playlistToShuffle.Remove(track);
            }

            var shuffledPlaylist = ShuffleList(playlistToShuffle);

            foreach (var track in shuffledPlaylist)
            {
                audioSource.clip = track;
                audioSource.Play();
                yield return new WaitForSeconds(track.length);

                if (stopCoroutine)
                {
                    yield break; // Beendet die Coroutine, wenn stopCoroutine true ist
                }

                UpdatePlayHistory(track);
            }
        }
    }

    private List<AudioClip> ShuffleList(List<AudioClip> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            AudioClip temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    private void UpdatePlayHistory(AudioClip track)
    {
        playHistory.Add(track);
        if (playHistory.Count > historyLimit)
        {
            playHistory.RemoveAt(0);
        }
    }

    public void PauseMusic()
    {
        audioSource.Pause();
        stopCoroutine = true;
    }

    public void ResumeMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        stopCoroutine = false;
    }
}