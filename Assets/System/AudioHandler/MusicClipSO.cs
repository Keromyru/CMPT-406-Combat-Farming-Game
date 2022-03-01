using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
[CreateAssetMenu(fileName = "Music_Clip", menuName = "Sound Data/Music Clip")]
public class MusicClipSO : AudioClipSO
{
    [Header("Music Specific Settings")]
    public location musicLocation;


    public enum location
    {
        Menus,
        DayCycle,
        NightCycle,
        Other,
    }
}
