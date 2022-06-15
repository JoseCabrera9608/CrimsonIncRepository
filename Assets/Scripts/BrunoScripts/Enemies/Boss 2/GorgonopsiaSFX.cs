using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GorgonopsiaSFX : MonoBehaviour
{
    public SoundsList[] list;
    public static GorgonopsiaSFX Instance;
    private void Awake()
    {
        foreach(SoundsList sfx in list)
        {
            sfx.source = gameObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.source.volume = sfx.volume;
            sfx.source.loop = sfx.loop;
            sfx.source.pitch = sfx.pitch;

            //3D settings
            sfx.source.spatialBlend = 1;
            sfx.source.playOnAwake = false;
            sfx.source.maxDistance = 50;
            sfx.source.minDistance = 1;
            sfx.source.rolloffMode=AudioRolloffMode.Linear;
        }
        Instance = this;
    }

    public void Play(string _clipName)
    {
        SoundsList s = Array.Find(list, sound => sound.clipName == _clipName);
        s.source.Stop();
        s.source.Play();
    }
    public void Stop(string _clipName)
    {
        SoundsList s = Array.Find(list, sound => sound.clipName == _clipName);
         s.source.Stop();
    }
    [System.Serializable]
    public class SoundsList
    {
        public string clipName;
        public AudioClip clip;
        [Range(0, 1)] public float volume;
        [Range(0.1f, 3f)] public float pitch;
        public bool loop;
        [HideInInspector]public AudioSource source;
    }
}
