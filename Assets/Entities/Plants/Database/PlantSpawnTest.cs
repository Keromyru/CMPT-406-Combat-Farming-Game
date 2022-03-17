using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawnTest : MonoBehaviour
{
    public PlantDatabaseSO plants;
    private void Awake() {
<<<<<<< HEAD
        // plants.spawnPlant("Hydra", new Vector2(0.94f,0.82f));
        // plants.spawnPlant("Eggroot", new Vector2(-0.94f,0.82f));
        // plants.spawnPlant("GigaGourd", new Vector2(0.94f,-0.86f));
        // plants.spawnPlant("HiveFlower", new Vector2(-0.94f,-0.82f));
=======
        plants.spawnPlant("Hydra", new Vector3(0.9f,0.8f,0f));
        plants.spawnPlant("Eggroot", new Vector3(-0.9f,0.8f,0f));
        plants.spawnPlant("GigaGourd", new Vector3(0.9f,-0.8f,0f));
        plants.spawnPlant("HiveFlower", new Vector3(-0.9f,-0.8f,0f));
>>>>>>> 569d778de68a34c879665708b8987bf190636bc8
    }
}
