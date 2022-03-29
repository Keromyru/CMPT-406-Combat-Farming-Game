using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System;
//TDK443
public static class SaveSystem
{
    public static void SaveSession(){

   }

    public static void LoadSession(){

   }

    public static void SaveLevel(){
        //BUILD DATA OBJECT
        //////////////////////////////////////////////////////////////////
        GameObject[] myPlants = GameObject.FindGameObjectsWithTag("Plant");
        // Aruguments: plantList, int day, float cash, float score
        LevelSave myLevel = new LevelSave(myPlants,0,Currency.getMoney(),0);

        //Build Save Object
        //////////////////////////////////////////////////////////////////
        BinaryFormatter formatter = new BinaryFormatter();
        string mySavePath = Application.persistentDataPath + "/LevelSave.wtf";
        FileStream stream = new FileStream(mySavePath, FileMode.Create);
        formatter.Serialize(stream, myLevel);
        stream.Close();
    }

    public static void LoadLevel(){
        string mySavePath = Application.persistentDataPath + "/LevelSave.wtf";
        if (File.Exists(mySavePath)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(mySavePath, FileMode.Open);

            LevelSave myLevel = formatter.Deserialize(stream) as LevelSave;
            stream.Close();

            SetLevel(myLevel); // Loads the plant spawns
            Currency.setMoney(myLevel.getCash());

        } else {
            Debug.LogError ("Save file not found in " + mySavePath);
        }
    }

    public static void SetLevel(LevelSave Level){
        PlantDatabaseSO myDatabase = Resources.FindObjectsOfTypeAll<PlantDatabaseSO>()[0];
        Array.ForEach(Level.getMyPlants(), p => {
            //string name, Vector2 location, float health, float energy, int age{
            myDatabase.spawnPlant(p.plantName, p.getLocation(),p.planthealth, p.plantAge); 
        });
    }
}