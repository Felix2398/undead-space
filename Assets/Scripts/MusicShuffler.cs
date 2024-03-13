using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicShuffler : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> playlist = new List<AudioClip>();
    private List<AudioClip> playHistory = new List<AudioClip>();
    private int historyLimit = 2; // Anpassen, um mehr oder weniger der letzten Lieder zu berücksichtigen
    static MusicShuffler instance;


    void Start()
    {
        StartCoroutine(PlayShuffledMusic());
    }

    public static MusicShuffler GetInstance() {
        return instance;
    }
    
    void Awake() {
        
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    IEnumerator PlayShuffledMusic()
    {
        while (true)
        {
            var playlistToShuffle = new List<AudioClip>(playlist);

            // Entferne die Historie, um direkte Wiederholungen zu vermeiden
            foreach (var track in playHistory)
            {
                playlistToShuffle.Remove(track);
            }

            // Mische die verbleibende Playlist
            var shuffledPlaylist = ShuffleList(playlistToShuffle);

            foreach (var track in shuffledPlaylist)
            {
                audioSource.clip = track;
                PlayAudioSource();
                yield return new WaitForSeconds(track.length);

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
            playHistory.RemoveAt(0); // Entferne das älteste Lied, um die Historie auf eine feste Größe zu beschränken
        }
    }

    public void PauseAudioSource() {
        audioSource.Pause();
    }

    public void PlayAudioSource() {
        audioSource.Play();
    }
}
