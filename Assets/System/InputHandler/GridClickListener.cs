using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this listens for clicks on the tilemap and places plants!
public class GridClickListener : MonoBehaviour{
    // The world tilemap with basic tiles
    [SerializeField] private Tilemap worldTilemap;

    // Need to ensure the player isn't going to plant in the same tile as an obstacle
    [SerializeField] private Tilemap obstacleTilemap;

    public Camera cam;

    /*
        For now have an array of plantable things
        this can be a different data structure later where the player can have a choosen
        seed in hand (or "equipped") and we find the associated prefab of that plant type
        and plant it.
        this will have lots of interactions with several component; like inventory, day night, enemies, etc.
    */
    [SerializeField] private GameObject[] plantables;

    [SerializeField] private PlantCollection plantCollection;

    void Start(){
        plantCollection = new PlantCollection();
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
            var cellPosition = worldTilemap.WorldToCell(point);
            Vector3 centerPos = worldTilemap.GetCellCenterLocal(cellPosition);

            Transform obstacleTransform = obstacleTilemap.transform;
            int obstacleCount = obstacleTransform.childCount;
<<<<<<< Updated upstream
            int plantsize =  plantCollection.getSize();
            int maxIterationCount = obstacleCount > plantsize ? obstacleCount : plantsize;
            for (int i = 0; i < maxIterationCount; i++){
                if (i < obstacleCount){
                    Transform child = obstacleTransform.GetChild(i);
                    string tag = child.gameObject.tag;
                    if (child.position == obstacleTilemap.LocalToWorld(
                        worldTilemap.CellToLocalInterpolated(cellPosition))){
                            Debug.Log("hello "+ child);
                            return;
                        }
                }
                if (i < plantsize){
                    // get coords for new plant

                    //logic to see if a plant is already in that slot

                    // if there is a plant in the collection that is already in the slot, don't plant
                    Debug.Log("plants!!");
                }
           
            }

=======
            for (int i = 0; i < obstacleCount; i++){
                Transform child = obstacleTransform.GetChild(i);
                string tag = child.gameObject.tag;
                if (child.position == obstacleTilemap.LocalToWorld(
                    worldTilemap.CellToLocalInterpolated(cellPosition))){
                        Debug.Log("hello "+ child);
                        return;
                }
            }
>>>>>>> Stashed changes
            // Creates the plant prefabs in the cell chosen by the player
            // TODO limit the range? 
            GameObject plantedPlant = Instantiate(plantables[0], centerPos, Quaternion.identity);
            plantCollection.addPlant(plantedPlant);
        }
    }
}
