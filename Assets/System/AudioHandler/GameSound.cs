using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class GameSound 
{
    public static class Interface {   
        // public static void Play(GameObject source, string name){
        //     GameObject.Find("Interface Audio").GetComponent<InterfaceAudioController>().Play(source, name);
            
        // }
        // public static void PlayTagged(GameObject source,string tag){
        //     GameObject.Find("Interface Audio").GetComponent<InterfaceAudioController>().PlayTagged(source, tag);
        // }
        // public static void PlayTagged(GameObject source,string tag1, string tag2){
        //     GameObject.Find("Interface Audio").GetComponent<InterfaceAudioController>().PlayTagged(source, tag1, tag2);
        // }
        // public static float getVolume(){  //TODO
        // return 0.1f;
        // }
    }
    public static class Music{ 
        public static void Play(string name){
            GameObject.Find("Music Audio").GetComponent<MusicController>().Play(name);
        }

        public static void StartTaggedPlaylist(string tag){
            GameObject.Find("Music Audio").GetComponent<MusicController>().StartTaggedPlaylist(tag);
        }

        private static void Pause(){
        GameObject.Find("Music Audio").GetComponent<MusicController>().Pause();
        }

        private static void UnPause(){
            GameObject.Find("Music Audio").GetComponent<MusicController>().UnPause(); 
        }

        private static void StopAll(){
            GameObject.Find("Music Audio").GetComponent<MusicController>().StopAll(); 
        }
        

    }   
}

