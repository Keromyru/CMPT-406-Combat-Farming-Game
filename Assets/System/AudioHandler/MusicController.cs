using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//TDK443
public class MusicController : MonoBehaviour
{
    public Sound[] sounds; //Music Elements
    public Playlist[] scenePlaylist;  //Listing of what tags are for what playlist

    
    private int playNum; //Tracks the Index of the playlist
    private  Sound[] currentPlaylist;
    private AudioSource musicPlayer;
    private bool isPaused = true;


    private void Awake(){   
        musicPlayer = this.gameObject.AddComponent<AudioSource>(); //Sets the Audio source for easy recall
        foreach (Sound s in sounds){   
            //Loads All Of The Audio Into The Game Objects
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void FixedUpdate(){ 
        //Starts next file when song is over
        if (!musicPlayer.isPlaying && !isPaused){
            PlayNext();
        }
    }

    public void Play(string name){
        musicPlayer.Stop(); //Stops existing sounds
        Sound s = Array.Find(sounds, Clip =>  Clip.name == name); //Loads "s" with first element named "name"
        if( s == null) { //if Null
            Debug.LogWarning("Sounds : "+ name + " not found");
            return;
        }
        else { //Loads up the music player source with the configs
            musicPlayer.clip = s.clip;
            musicPlayer.volume = s.volume * getVolume();
            musicPlayer.pitch = s.pitch;
            musicPlayer.Play();
        }
    }

    private void PlayNext(){
        playNum ++;
        if (playNum >= currentPlaylist.Length){
            playNum = 0;
        }
        this.Play(currentPlaylist[playNum].name);
    }

    public void Pause(){
        musicPlayer.Pause(); //Stops existing sounds
    }

    public void UnPause(){
        musicPlayer.UnPause(); //Stops existing sounds
    }

    public void StopAll(){
         musicPlayer.Stop();
         ClearPlaylist();
    }

    public void StartTaggedPlaylist(string tag){
        musicPlayer.Stop();  //Stops the music
        this.ClearPlaylist(); //Resets the playlist
        currentPlaylist = Array.FindAll(sounds, clip => (clip.tags).Contains(tag));
        this.Play(currentPlaylist[playNum].name);
        isPaused = false;
    }
    public void ClearPlaylist(){
        currentPlaylist = null;
    }


    private float getVolume(){
        return 1f; //TODO
    }


}
