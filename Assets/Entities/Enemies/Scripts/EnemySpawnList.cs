using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A static class that holds the spawnpoint list
public static class EnemySpawnList
{
    // Copy the spawnPoints list and this one will be used to find which spawnpoint will be used for the next night
    public static List<Transform> spawnPoints;

    // Gets the entire list of spawnpoint transforms
    public static List<Transform> getList()
    {
        return spawnPoints;
    }

    // Sets the entire list of spawnpoint transforms
    public static void setList(List<Transform> transformList)
    {
        spawnPoints = new List<Transform>(transformList);
    }

    // Gets the first spawnpoint for the night
    public static Transform getFirstSpawn()
    {
        return spawnPoints[0];
    }

    // Remove first variable in list
    public static void removeFirstSpawn()
    {
        spawnPoints.RemoveAt(0);
    }
}
