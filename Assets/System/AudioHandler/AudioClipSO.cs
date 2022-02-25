using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SFX_Clip", menuName = "Sound Data/Audio Clip")]
public class AudioClipSO : ScriptableObject
{
    #region config
    public string audioName;
    [Tooltip("This will let you play random audio from a tagged group")] 
    public string[] tags;
    public AudioClip clip;

    [Range(0f,1.0f)] 
    public float volume = 1;
    [Range(.1f,3.0f)] 
    public float pitch = 1;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    #endregion
    
    public AudioSource Play(AudioSource audioSourceParam = null){


        return source;
    }


}
