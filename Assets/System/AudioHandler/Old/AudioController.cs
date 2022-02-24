using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;
//TDK443
public abstract class AudioController : MonoBehaviour
{
    public Sound[] sounds;
     private void Awake() 
    {
        foreach (Sound s in sounds){   
            //Load All Of The Audio Into The Game Objects
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(GameObject newSource, string name) {
        Sound s = Array.Find(sounds, Clip =>  Clip.name == name);
        if( s == null) {
            Debug.LogWarning("Sounds : "+ name + " not found");
            return;
        }
        else { 
            AudioSource p = newSource.AddComponent<AudioSource>();
            p.volume = s.volume * getVolume();
            p.clip = s.clip;
            p.volume = s.volume;
            p.pitch = s.pitch;
            p.loop = s.loop;
            p.Play();
        }
    }

    public virtual float getVolume(){
        return 1f;  //TODO
    }

    public void PlayTagged(GameObject newSource, string tag){
        Sound[] s = Array.FindAll(sounds, clip => (clip.tags).Contains(tag));
        int randomPick = UnityEngine.Random.Range(0,s.Length);
        Play(newSource, s[randomPick].name);
    }


    public void PlayTagged(GameObject newSource,string tag1, string tag2){
        Sound[] s1 = Array.FindAll(sounds, clip => (clip.tags).Contains(tag1));
        Sound[] s2 = Array.FindAll(s1, clip => (clip.tags).Contains(tag2));
        int randomPick = UnityEngine.Random.Range(0,s2.Length);
        Play(newSource, s2[randomPick].name);
    }
}



