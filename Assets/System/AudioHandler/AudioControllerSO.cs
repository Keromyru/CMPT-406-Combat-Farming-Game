using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
[CreateAssetMenu(fileName = "AudioController", menuName = "Sound Data/Audio Controller")]
//TDK443
public class AudioControllerSO : ScriptableObject
{
    public Type type;
    public AudioClipSO[] instances;
    
    private void OnEnable() {
        //Checks all the AudioClipSO's and adds the one with the correct type to it's list
        instances = instances.Union(Resources.FindObjectsOfTypeAll<AudioClipSO>()).ToArray();
    }

    public AudioSource Play(string name, AudioSource audioSourceParam = null) {
        var source = audioSourceParam;
        //Looks for the element that shares it's name with the input
        AudioClipSO AD = instances.First(m => m.Name.Contains(name));
        //Plays the file using it's perameters, and returns the audiosource
        return AD.Play(audioSourceParam);      
    }
    public enum Type
    {
        GUI,
        NPC,
        Environment,
        Player
    }
}


