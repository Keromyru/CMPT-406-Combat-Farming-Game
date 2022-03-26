using UnityEngine;
using System.Linq; 

public static class RangeFromHub 
{
        public static bool forPlayer(float range){
        return GameObject.Find("HUB").GetComponents<Collider2D>().Any(s => 
        (Vector2.Distance (GameObject.FindGameObjectWithTag("Player").transform.position, s.ClosestPoint(GameObject.FindGameObjectWithTag("Player").transform.position)) < range ));
    }
}