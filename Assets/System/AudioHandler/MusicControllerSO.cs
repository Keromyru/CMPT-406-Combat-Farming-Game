using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MusicController", menuName = "Sound Data/Music Controller")]
public class MusicControllerSO : ScriptableObject
{
        public List<MusicClipSO> instances;

        private List<MusicClipSO>Menus;
        private List<MusicClipSO>DayCycle;
        private List<MusicClipSO>NightCycle;
        private int mIndex; //Index Pointer
        private int playlist = 0;

        

    private void OnEnable() {
        //Clears All The Arrays
        Menus = new List<MusicClipSO>(); 
        DayCycle = new List<MusicClipSO>(); 
        NightCycle = new List<MusicClipSO>(); 
        instances = new List<MusicClipSO>(); 


        //Searches and populates with every instance of MusicClipSO kickin' around
        foreach (MusicClipSO item in Resources.FindObjectsOfTypeAll<MusicClipSO>()){
            if(!instances.Contains(item)){instances.Add(item);}
        }

        //Checks all the AudioClipSO's and adds the one with the correct type to it's list
        foreach (MusicClipSO item in instances){
            //Music List with info
            //Debug.Log("Name: "+item.Name+ "      for: "+ item.musicLocation);
    
            switch ((int)item.musicLocation)
            {
                case 0:
                    Menus.Add(item);
                break;

                case 1:
                    DayCycle.Add(item);
                break;

                case 2:
                    NightCycle.Add(item);
                break;

                default:
                
                break;
            }
        }
         Debug.Log(DayCycle.Count);
    }

    //Disabled AutoPlay and Starts a music piece of the specified name;
    public AudioSource PlaySong(string name, AudioSource audioSourceParam = null) {
        var source = audioSourceParam;
        //Looks for the element that shares it's name with the input
        MusicClipSO AD = instances.Find(x => x.Name.Contains(name));
        //Plays the file using it's perameters, and returns the audiosource
        return AD.Play(audioSourceParam);      
    }

    //preps and sets what list to play
    public void SetPlayList(int locationName) {
        mIndex = 0; 
        playlist = locationName;
    }


    public AudioSource PlayNext(AudioSource audioSourceParam) {
        var source = audioSourceParam;
        MusicClipSO selectedMusic;      
        switch (playlist)
            { //Takes the chosen playlist and plays the next song from
                case 0:
                    selectedMusic = Menus[mIndex];
                    if (mIndex+1 == Menus.Count){ mIndex = 0;}
                    selectedMusic.Play(audioSourceParam);
                break;

                case 1:
                   selectedMusic = DayCycle[mIndex];
                   if (mIndex+1 == DayCycle.Count){ mIndex = 0;}
                   selectedMusic.Play(audioSourceParam);
                break;

                case 2:
                   selectedMusic = NightCycle[mIndex];
                   if (mIndex+1 == NightCycle.Count){ mIndex = 0;}
                   selectedMusic.Play(audioSourceParam);
                break;

                default:
                    selectedMusic = instances[0];
                    selectedMusic.Play(audioSourceParam);
                break;
            }
        //Increment the counter
        mIndex ++;

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
