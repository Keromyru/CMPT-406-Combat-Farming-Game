using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SFX_Clip", menuName = "Sound Data/Audio Clip")]
//TDK443
public class AudioClipSO : ScriptableObject
{
     #region config
     [Header("Audioclip Pack")]
    public string Name;
    public AudioClip[] clips;
    public Type type;
    [Header("Settings")]
    [Range(0f,1.0f)] 
    public float volume = 1;
    [Range(.1f,1.0f)] 
    public float pitch = 1;

    [HideInInspector]
    public AudioSource source;

    #endregion

    public AudioSource Play(AudioSource audioSourceParam = null)
    {
        if (clips.Length == 0)
            {
                Debug.LogWarning("Missing sound clips for "+ name);
                return null;
            }

        var source = audioSourceParam;
        //If there is no source file passed for this audio file, it'll just make a new one
        if (source == null){
            var _obj = new GameObject("Audio Clip", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }

        //Sets the config values so the new source
        source.clip = GetAudioClip();
        source.volume = volume;
        source.pitch = pitch;

        source.Play();

        //Destroys object when file is done playing
        Destroy(source.gameObject, source.clip.length/source.pitch);

        //Returning source in case there needs to be further tweaking or stopped early
        return source; 
    }

    private AudioClip GetAudioClip(){
        //Returns Random Clip here, may decide on something more elegant later
        return clips[Random.Range(0,clips.Length)];
    }

    public enum Type
{
    GUI,
    NPC,
    Environment,
    Player,
    Music
}

}
