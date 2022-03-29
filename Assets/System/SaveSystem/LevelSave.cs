using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
[System.Serializable]
public class LevelSave 
{
    PlantData[] mySavedPlants;
    List<PlantData> myPlants;
    int savedDay;
    int savedCash;
    int savedScore;
    /*
    PARAM: A list of every Plant in the level,
    Current Day Count
    Current Cash
    Current Score
    */

    public LevelSave(GameObject[] plantList, int day, int cash, int score){
       Array.ForEach(plantList, p => {
           PlantController myData = p.GetComponent<PlantController>();
           myPlants.Add(new PlantData(myData.name, myData.getLocation(), myData.getHealth(), myData.getGrowAge()));
       });
       mySavedPlants = myPlants.ToArray(); 
       this.savedDay = day;
       this.savedCash = cash;
       this.savedScore = score;
    }

    public PlantData[] getMyPlants(){ return mySavedPlants; }
    public int getDay(){ return this.savedDay;}
    public int getCash(){ return this.savedCash; }
    public int getScore(){ return this.savedScore; }


    public class PlantData{
        public string plantName;
        public float locationX;
        public float locationY;
        public float planthealth;
        public int plantAge;

        public PlantData(string name, Vector2 location, float health, int age){
            this.plantName = name;
            this.locationX = location.x;
            this.locationY = location.y;
            this.planthealth = health;
            this.plantAge = age;
        }

        public Vector2 getLocation(){ return new Vector2(this.locationX,this.locationY); }
    }
}
