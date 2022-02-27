using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioController", menuName = "Sound Data/Audio Controller")]
//TDK443
public class AudioControllerSO : ScriptableObject
{
    public Type type;
    public List<AudioClipSO> instances;
    
    private void OnEnable() {
        instances = new List<AudioClipSO>(); 
        //Checks all the AudioClipSO's and adds the one with the correct type to it's list
        foreach (AudioClipSO item in Resources.FindObjectsOfTypeAll<AudioClipSO>())
        {
            if(!instances.Contains(item) && (int)item.type == (int)this.type){
                instances.Add(item);
            }
        }
    }


    public AudioSource Play(string name, AudioSource audioSourceParam = null) {
        var source = audioSourceParam;
        //Looks for the element that shares it's name with the input
        AudioClipSO AD = instances.Find(x => x.Name.Contains(name));
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
