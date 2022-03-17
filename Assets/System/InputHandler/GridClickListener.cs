using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this is exprimental and not fully implemmented
public class GridClickListener : MonoBehaviour
{
    [SerializeField] private GridLayout gridHoldingTilemaps; 

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap obstacleTilemap;
    

    public Camera cam;
    private Vector2 mousePosition;

    public PlantDatabaseSO plantDatabase;
    private List<Vector3> plantLocationCollection;
    void Start(){
        plantLocationCollection = new List<Vector3>();
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
            
            int length = plantLocationCollection.Count;
            for (int i = 0; i < length; i++ ){
                if (centerPos == plantLocationCollection[i]){
                    Debug.Log("we collided");
                    Debug.Log(centerPos);
                    Debug.Log(plantLocationCollection[i]);
                    return;
                }
            }
            // assuming there hasn't been a match
            // save for offset
            // GameObject plant = plantDatabase.spawnPlant("Hydra", new Vector2(centerPos.x, centerPos.y + (float) .18));
            // save for offset
            // GameObject plant = plantDatabase.spawnPlant("GigaGourd", new Vector2(centerPos.x, centerPos.y + (float) .18));
            // save for offset
            // GameObject plant = plantDatabase.spawnPlant("Eggroot", new Vector2(centerPos.x - (float) .05, centerPos.y + (float) .05));
            // save for offset
            // GameObject plant = plantDatabase.spawnPlant("HiveFlower", new Vector2(centerPos.x, centerPos.y + (float) .30));
            plantLocationCollection.Add(centerPos);
        
        }
    }
}
