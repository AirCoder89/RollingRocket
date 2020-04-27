using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    private bool IsMusic;
    private bool IsMusicPlayed;
    void Awake () {

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    private void Start()
    {
        GetVolumeFromSettings();
        GetSoundFXFromSettings();
    }
    public void SetMusicVolume(float vol)
    {
        foreach (Sound s in sounds)
        {
            if(s.IsMusic)
            {
                s.source.volume = vol;
                s.volume = vol;
            }
        }
    }
    public void SetMusicPitch(float p)
    {
        foreach (Sound s in sounds)
        {
            if (s.IsMusic)
            {
                s.source.pitch = p;
                s.pitch = p;
            }
        }
    }
    public void SetSoundEffectStatus(bool status)
    {
        foreach (Sound s in sounds)
        {
            if (!s.IsMusic)
            {
                s.source.mute = !status;
            }
        }
    }

    private void GetSoundFXFromSettings()
    {
        foreach (Sound s in sounds)
        {
            if (!s.IsMusic)
            {
                s.source.mute = !SceneHandler.GetInstance().Settings.GetSoundEffectStatus();
            }
        }
    }
    private void GetVolumeFromSettings()
    {
        foreach (Sound s in sounds)
        {
            if (s.IsMusic)
            {
                s.source.volume = SceneHandler.GetInstance().Settings.GetMusicVolume();
                s.volume = SceneHandler.GetInstance().Settings.GetMusicVolume();
            }
        }
    }
    public void Play(string name,bool ismusic = false)
    {
        if(!ismusic)
        {
            Sound s = Array.Find(sounds, Sound => Sound.name == name);
            if (s == null)
            {
                Debug.Log("AudioManager > Sound [" + name + "] not found !");
                return;
            }
            s.source.Play();
        }
        else
        {
            if(!IsMusicPlayed)
            {
                IsMusicPlayed = true;
                Sound s = Array.Find(sounds, Sound => Sound.name == name);
                if (s == null)
                {
                    Debug.Log("AudioManager > Sound [" + name + "] not found !");
                    return;
                }
                s.source.Play();
            }
        }
       
    }
    
    public void Stop(string name,bool ismusic = false)
    {
        if(!ismusic)
        {
            Sound s = Array.Find(sounds, Sound => Sound.name == name);
            if (s == null)
            {
                Debug.Log("AudioManager > Sound [" + name + "] not found !");
                return;
            }
            s.source.Stop();
        }
        else
        {
            if(IsMusicPlayed)
            {
                IsMusicPlayed = false;
                Sound s = Array.Find(sounds, Sound => Sound.name == name);
                if (s == null)
                {
                    Debug.Log("AudioManager > Sound [" + name + "] not found !");
                    return;
                }
                s.source.Stop();
            }
        }
    }

    public void Pause(string name,bool ismusic = false)
    {
        if(!ismusic)
        {
            Sound s = Array.Find(sounds, Sound => Sound.name == name);
            if (s == null)
            {
                Debug.Log("AudioManager > Sound [" + name + "] not found !");
                return;
            }
            s.source.Pause();
        }
        else
        {
            if(IsMusicPlayed)
            {
                IsMusicPlayed = false;
                Sound s = Array.Find(sounds, Sound => Sound.name == name);
                if (s == null)
                {
                    Debug.Log("AudioManager > Sound [" + name + "] not found !");
                    return;
                }
                s.source.Pause();
            }
        }
        
    }

    public void Resume(string name,bool ismusic = false)
    {
        if(!ismusic)
        {
            Sound s = Array.Find(sounds, Sound => Sound.name == name);
            if (s == null)
            {
                Debug.Log("AudioManager > Sound [" + name + "] not found !");
                return;
            }
            s.source.UnPause();
        }
        else
        {
            if(!IsMusicPlayed)
            {
                IsMusicPlayed = true;
                Sound s = Array.Find(sounds, Sound => Sound.name == name);
                if (s == null)
                {
                    Debug.Log("AudioManager > Sound [" + name + "] not found !");
                    return;
                }
                s.source.UnPause();
            }
        }
        
    }
}
