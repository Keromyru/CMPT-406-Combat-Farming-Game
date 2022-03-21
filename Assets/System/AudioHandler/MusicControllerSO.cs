using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
[CreateAssetMenu(fileName = "MusicController", menuName = "Sound Data/Music Controller")]
public class MusicControllerSO : ScriptableObject
{
        public MusicClipSO[] instances;
        private MusicClipSO[] Menus;
        private MusicClipSO[] DayCycle;
        private MusicClipSO[]NightCycle;
        private int mIndex; //Index Pointer
         
        private MusicClipSO[] playlist;

    private void OnEnable() {
        //Searches and populates with every instance of MusicClipSO kickin' around
        instances = instances.Union(Resources.FindObjectsOfTypeAll<MusicClipSO>()).OrderBy(m => m.priority).ToArray();
        
        Menus = instances.Where(m => m.musicLocation == 0 ).ToArray();
        DayCycle = instances.Where(m => (int)m.musicLocation == 1 ).ToArray();
        NightCycle = instances.Where(m => (int)m.musicLocation == 2 ).ToArray();
        //Array.ForEach(instances, m => Debug.Log(m.Name)); //Lists The Entire Music Library
    }

    //Disabled AutoPlay and Starts a music piece of the specified name;
    public AudioSource PlaySong(string name, AudioSource audioSourceParam = null) {
        var source = audioSourceParam;
        //Looks for the element that shares it's name with the input
        
        MusicClipSO AD = instances.First(m => m.Name.Contains(name));
        //Plays the file using it's perameters, and returns the audiosource
        return AD.Play(audioSourceParam);      
    }

    //preps and sets what list to play
    public AudioSource StartPlayList(int locationName, AudioSource audioSourceParam = null) {
        mIndex = 0;
        switch (locationName){
            case 0: 
                playlist = Menus;
            break;

            case 1:
                playlist = DayCycle;
            break;

            case 2:
                playlist = NightCycle;
            break;

            default:
                playlist = instances;
            break;
        }
        playlist[mIndex].Play(audioSourceParam);
        return audioSourceParam;     
    }


    public AudioSource PlayNext(AudioSource audioSourceParam) {
        mIndex ++;
        if(mIndex == playlist.Length){mIndex = 0;}     
        playlist[mIndex].Play(audioSourceParam);
        //Plays the file using it's perameters, and returns the audiosource
        return audioSourceParam;      
    }

    public enum location
    {
        Menus,
        DayCycle,
        NightCycle,
        Other,
    }
}
