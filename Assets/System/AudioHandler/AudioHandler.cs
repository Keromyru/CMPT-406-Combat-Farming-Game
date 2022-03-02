using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public MusicControllerSO controller;

    private AudioSource musicPlayer;
    private bool isPaused = true;
    private void Awake(){   
        musicPlayer = this.gameObject.AddComponent<AudioSource>(); //Sets the Audio source for easy recall
    }

    private void FixedUpdate(){ 
        //Starts next file when song is over
        if (!musicPlayer.isPlaying && !isPaused){
            PlayNext();
        }
    }
    public AudioSource PlaySong(string name) {
        StopAll();
        //Plays the file using it's perameters, and returns the audiosource
        return controller.PlaySong(name, musicPlayer);      
    }


    public AudioSource PlayNext() {
        isPaused = false;
        return controller.PlayNext(musicPlayer);    
    }

    //Given a Location in string form this will contact the SO to clear everything and start the playlist
    public void SetPlayList(string location){ 
        location lName = (location)System.Enum.Parse( typeof(location), location );
        StopAll();
        controller.SetPlayList((int)lName);
    }

    //Stops the Player
    public void StopAll(){
        isPaused = true;
        musicPlayer.Stop();
    }



    public enum location
    {
        Menus,
        DayCycle,
        NightCycle,
        Other,
    }

}
