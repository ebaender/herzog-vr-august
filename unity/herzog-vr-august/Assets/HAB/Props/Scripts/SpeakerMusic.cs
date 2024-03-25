using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class SpeakerMusic : MonoBehaviour
{
    public List<AudioClip> tracks;
    private List<AudioClip> playedTracks;
    private int trackIndex = 0;
    private AudioSource audioSource;

    void Awake()
    {
        playedTracks = new List<AudioClip>();
        audioSource = GetComponent<AudioSource>();
        tracks = tracks.OrderBy(x => Random.value).ToList();
        audioSource.clip = tracks[trackIndex];
        audioSource.Play();
    }

    void NextClip()
    {
        if (trackIndex < tracks.Count - 1)
        {
            ++trackIndex;
        }
        else
        {
            trackIndex = 0;
        }
        audioSource.clip = tracks[trackIndex];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            else
            {
                NextClip();
                audioSource.Play();
            }
        }
    }

}
