using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this is exprimental and not fully implemmented
public class GridClickListener : MonoBehaviour
{
    [SerializeField] private GridLayout gridHoldingTilemaps; 

    [SerializeField] private Tilemap tilemap;
    

    public Camera cam;
    private Vector2 mousePosition;

    public PlantDatabaseSO plantDatabase;
    void Start(){
        
    }

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
            var cellPosition = tilemap.WorldToCell(point);
            Vector3 centerPos = tilemap.GetCellCenterLocal(cellPosition);

            // Creates the plant prefabs in the cell chosen by the player
            GameObject plant = plantDatabase.spawnPlant("Hydra", new Vector2(centerPos.x, centerPos.y + (float) .18));
        }
    }
}
