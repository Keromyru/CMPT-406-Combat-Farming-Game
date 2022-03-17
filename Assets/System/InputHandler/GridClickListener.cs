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

    [SerializeField] private GameObject hotbar;
    private Seed seedToPlant;
    void Start(){
        plantLocationCollection = new List<Vector3>();
        seedToPlant = null;
    }

    void Update(){
        if (Input.GetButtonDown("Fire1") && seedToPlant != null){
            Inventory.instance.getItemAmount(seedToPlant);
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
            
            int plantLocationCollectionLength = plantLocationCollection.Count;
            int obstacleLength = gridHoldingTilemaps.transform.childCount;
            Debug.Log(obstacleLength);
            for (int i = 0; i < plantLocationCollectionLength; i++ ){
                if (i < plantLocationCollectionLength && centerPos == plantLocationCollection[i]){
                    Debug.Log("we collided");
                    return;
                }
                // Vector3 vectorpos = (obstacleTilemap.WorldToCell(
                //     obstacleTilemap.GetCellCenterLocal(cellPosition)));
                // Vector3 alteredPos = vectorpos - new Vector3( (float) .03, (float) .11, 0);
                // Debug.Log(alteredPos);
                // Debug.Log(gridHoldingTilemaps.transform.GetChild(i).position);
                // if (alteredPos == gridHoldingTilemaps.transform.GetChild(i).position){
                //     Debug.Log("we hit an obstacle");
                //     return;
                // }
            }
            GameObject planted = plantDatabase.spawnPlant(seedToPlant.getSpawnName(), centerPos);
            seedToPlant.remove();
            Debug.Log(tilemap.GetColor(cellPosition));
            plantLocationCollection.Add(centerPos);

            if (Inventory.instance.getItemAmount(seedToPlant) <= 0){
                seedToPlant = null;
                
            }
        }     
    }

    public void setItemToPlant(Seed seed){
       seedToPlant = seed;
    }
}
