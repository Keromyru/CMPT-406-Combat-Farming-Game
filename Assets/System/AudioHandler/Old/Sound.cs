using UnityEngine;
[System.Serializable]
////TDK443
public class Sound 
{   
    public string name;
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
}

