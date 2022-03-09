using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawnTest : MonoBehaviour
{
    public PlantDatabaseSO plants;
    private void Awake() {
        plants.spawnPlant("Hydra", new Vector2(0.94f,0.82f));
    }
}
