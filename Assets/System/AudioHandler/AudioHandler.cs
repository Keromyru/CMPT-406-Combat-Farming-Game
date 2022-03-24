using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//TDK443
public class AudioHandler : MonoBehaviour
{
    public MusicControllerSO controller;
    private AudioSource musicPlayer;
    private bool isPaused = true;
    private void Awake(){ musicPlayer = this.gameObject.GetComponent<AudioSource>();} //Sets the Audio source for easy recall
    private void FixedUpdate(){ 
        //Starts next file when song is over
        if (!musicPlayer.isPlaying && !isPaused ){ PlayNext();}
    }
    void OnApplicationFocus(bool hasFocus){
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus){
        isPaused = pauseStatus;
    }

    public AudioSource PlaySong(string name) {
        StopMusic();
        //Plays the file using it's perameters, and returns the audiosource
        return controller.PlaySong(name, musicPlayer);      
    }

    //PlayNext Song In The Queue
    public AudioSource PlayNext() {
        isPaused = false;
        return controller.PlayNext(musicPlayer);    
    }

    //Given a Location in string form this will contact the SO to clear everything and start the playlist
    public void StartPlayList(string location){ 
        location lName = (location)System.Enum.Parse( typeof(location), location );
        StopMusic();
        isPaused = false;
        controller.StartPlayList((int)lName, musicPlayer);
    }

    //Night Time
    public void onNight(){ this.StartPlayList("NightCycle");}

    //Day Time
    public void OnDay(){this.StartPlayList("DayCycle");}

    //Stops the Player
    public void StopMusic(){
        isPaused = true;
        musicPlayer.Stop();
    }

    public void PauseMusic(){
        isPaused = true;
        musicPlayer.Pause();
    }

    public void ResumeMusic(){
        isPaused = false;
        musicPlayer.Play();
    }

    public GameObject[] GetAllAudioObjects(){ return GameObject.FindGameObjectsWithTag("AudioClip");} 
    public void ClearAudio(){ Array.ForEach(GetAllAudioObjects(), s => Destroy(s));}

    public enum location {
        Menus,
        DayCycle,
        NightCycle,
        Other,
    }
}
public static class MusicPlayer{
    public static void ResumeMusic(){ GameObject.Find("AudioHandler").GetComponent<AudioHandler>().ResumeMusic();}
    public static void PauseMusic(){ GameObject.Find("AudioHandler").GetComponent<AudioHandler>().PauseMusic();}
    public static void StartPlayList(string location){GameObject.Find("AudioHandler").GetComponent<AudioHandler>().StartPlayList(location);}
}

public static class SoundPlayer{
    public static GameObject[] GetAllAudioObjects(){return GameObject.Find("AudioHandler").GetComponent<AudioHandler>().GetAllAudioObjects();} 
    public static void ClearAudio(){ GameObject.Find("AudioHandler").GetComponent<AudioHandler>().ClearAudio();}
}
