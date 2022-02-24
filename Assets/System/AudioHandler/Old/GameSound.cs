
using UnityEngine;
//TDK443
public static class GameSound 
{
    public static class GUI {   
        public static void Play(GameObject source, string name){
            GameObject.Find("Interface Audio").GetComponent<GUIAudioController>().Play(source, name);   
        }
        public static void PlayTagged(GameObject source,string tag){
            GameObject.Find("Interface Audio").GetComponent<GUIAudioController>().PlayTagged(source, tag);
        }
        public static void PlayTagged(GameObject source,string tag1, string tag2){
            GameObject.Find("Interface Audio").GetComponent<GUIAudioController>().PlayTagged(source, tag1, tag2);
        }
        public static float getVolume(){  //TODO
        return 0.5f;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class Player { 

        public static void Play(GameObject source, string name){
            GameObject.Find("Interface Audio").GetComponent<PlayerAudioController>().Play(source, name);
            
        }
        public static void PlayTagged(GameObject source,string tag){
            GameObject.Find("Interface Audio").GetComponent<PlayerAudioController>().PlayTagged(source, tag);
        }
        public static void PlayTagged(GameObject source,string tag1, string tag2){
            GameObject.Find("Interface Audio").GetComponent<PlayerAudioController>().PlayTagged(source, tag1, tag2);
        }
          
        public static float getVolume(){  //TODO
        return 0.5f;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class Environment {   
        public static void Play(GameObject source, string name){
            GameObject.Find("Interface Audio").GetComponent<EnvironmentAudioController>().Play(source, name);
        }
        public static void PlayTagged(GameObject source,string tag){
            GameObject.Find("Interface Audio").GetComponent<EnvironmentAudioController>().PlayTagged(source, tag);
        }
        public static void PlayTagged(GameObject source,string tag1, string tag2){
            GameObject.Find("Interface Audio").GetComponent<EnvironmentAudioController>().PlayTagged(source, tag1, tag2);
        }
        public static float getVolume(){  //TODO
        return 0.5f;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class Entity {   
                public static void Play(GameObject source, string name){
            GameObject.Find("Interface Audio").GetComponent<EntityAudioController>().Play(source, name);
        }
        public static void PlayTagged(GameObject source,string tag){
            GameObject.Find("Interface Audio").GetComponent<EntityAudioController>().PlayTagged(source, tag);
        }
        public static void PlayTagged(GameObject source,string tag1, string tag2){
            GameObject.Find("Interface Audio").GetComponent<EntityAudioController>().PlayTagged(source, tag1, tag2);
        }
        public static float getVolume(){  //TODO
        return 0.5f;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
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

