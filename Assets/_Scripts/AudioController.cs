using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public List<AudioClip> musicList;
    public List<AudioClip> sfxList;

    public List<AudioSource> Channels = new List<AudioSource>();

    public string[] GeneralSFX;
    public string[] Footsteps;

    public AudioSource currentlyPlaying;

    public AudioSource musicSource;

    private static AudioController instance = null;
    public static AudioController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(string sfxName)
    {
        AudioClip clip = GetSFX(sfxName);

        if (clip != null)
        {
            StartCoroutine(PlayTempChannel(clip));
        }
    }

    public void PlayLoop(string sfxName)
    {
        foreach (AudioSource source in Channels)
        {
            if (source.clip.name == sfxName)
            {
                return;
            }
        }

        AudioClip clip = GetSFX(sfxName);

        if (clip != null)
        {
            StartCoroutine(PlayLoopChannel(clip));
        }
    }

    public void StopSFX(string sfxName)
    {
        foreach (AudioSource source in Channels)
        {
            if (source.clip.name == sfxName)
            {
                source.Stop();
                break;
            }
        }
    }

    AudioClip GetMusic(string musicName)
    {
        foreach (AudioClip clip in musicList)
        {
            if (clip.name == musicName)
            {
                return clip;
            }
        }
        return null;
    }

    AudioClip GetSFX(string sfxName)
    {
        foreach (AudioClip clip in sfxList)
        {
            if (clip.name == sfxName)
            {
                return clip;
            }
        }

        return null;
    }

    public void StopAllSFX()
    {
        foreach (AudioSource channel in Channels)
        {
            if (sfxList.Contains(channel.clip))
            {
                channel.Stop();
            }
        }
    }

    int lastStep = -1;
    public void Footstep()
    {
        int index = UnityEngine.Random.Range(0, Footsteps.Length);

        if (index == lastStep)
        {
            Footstep();
            return;
        }

        PlaySFX(Footsteps[index]);

        lastStep = index;
    }

    IEnumerator PlayTempChannel(AudioClip clip)
    {
        AudioSource tempChannel = null;
        float remainingTime = clip.length;
        tempChannel = gameObject.AddComponent<AudioSource>();
        Channels.Add(tempChannel);
        tempChannel.clip = clip;
        tempChannel.Play();

        yield return new WaitForSeconds(remainingTime);

        if (tempChannel != null)
        {
            Channels.Remove(tempChannel);
            Destroy(tempChannel);
        }
    }

    IEnumerator PlayLoopChannel(AudioClip clip)
    {
        AudioSource tempChannel = null;
        tempChannel = gameObject.AddComponent<AudioSource>();
        Channels.Add(tempChannel);
        tempChannel.clip = clip;
        tempChannel.loop = true;
        tempChannel.Play();

        while (tempChannel.isPlaying)
        {
            yield return null;
        }

        if (tempChannel != null)
        {
            Channels.Remove(tempChannel);
            Destroy(tempChannel);
        }
    }
}