using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this listens for clicks on the tilemap and places plants!
public class GridClickListener : MonoBehaviour{
    // The world tilemap with basic tiles
    [SerializeField] private Tilemap worldTilemap;

    // Need to ensure the player isn't going to plant in the same tile as an obstacle
    [SerializeField] private Tilemap obstacleMap;

    public Camera cam;

    /*
        For now have an array of plantable things
        this can be a different data structure later where the player can have a choosen
        seed in hand (or "equipped") and we find the associated prefab of that plant type
        and plant it.
        this will have lots of interactions with several component; like inventory, day night, enemies, etc.
    */
    [SerializeField] private GameObject[] plantables;

    void Update(){
        if (Input.GetButtonDown("Fire1")){
            // Grab the mouse position
            Vector3 mousePos = Input.mousePosition;
            // create the ray for casting
            Ray ray = cam.ScreenPointToRay(mousePos);
            // essentially mimics the planes of the world and the obstacles (they are within the same plane space)
            Plane plane = new Plane(Vector3.back, Vector3.zero);
            float hitdist;
            plane.Raycast(ray, out hitdist);
            // get the point the ray intersected
            var point = ray.GetPoint(hitdist);
            // find the cell point
            var cellPosition = worldTilemap.WorldToCell(point);
            Vector3 centerPos = worldTilemap.GetCellCenterLocal(cellPosition);

            // Creates the plant prefabs in the cell chosen by the player
            Instantiate(plantables[0], centerPos, Quaternion.identity);
        }
    }
}
