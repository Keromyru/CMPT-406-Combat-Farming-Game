using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor{

    // lets you create a new brush in the create menu, it will then create a scriptable object for you to
    // add in prefabs you could like to randomly be selected from and placed; this also stops multiple
    // prefabs from being placed in the same location, though this only works if the prefabs are 1 hex tile big
    [CreateAssetMenu]
    [CustomGridBrush(false, true, false, "Randomized Prefab Brush")]

    /*
        This class is intended to be used with single hex sized obstacles. Can easily be expanded to include
        different prefabs for say adornments on the tilemap or to place obstacles. NOTE if using obstacles please
        place it on the obstacles map, this way we have have one tilemap hold just obstacles and another the 
        actual world/game tiles 
    */
    public class RandomizedPrefabsBrush : GridBrushBase{
        // this size varies with how many you add to the scriptable object
        public GameObject[] randomPrefabs;

        // the paint method, this can be adjusted in many ways, this is just one way to randomly select and pain
        // prefabs
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position){
            // to ensure nothing crashes if the array is empty
            if (randomPrefabs.Length <= 0) return;

            // coord on the tilemap NOT the world, important distinction
            // can be used if we need to change the z coord to make it standard
            // or later adjust with offsets for things larger than one hex
            Vector3Int cellPosition = new Vector3Int(position.x, position.y, position.z);
            
            // check that the altered position is correct
            Vector3 vectorpos = (gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(cellPosition)));
            Vector3 alteredPos = vectorpos - new Vector3( (float) .03, (float) .11, 0);

            // checks if there is a obstacle in the cell already
            if (GetObjectInCell(gridLayout, brushTarget.transform, alteredPos) != null){
                return;
            }

            // randomly choose one of the obstacles (or prefabs) to paint into the scene
            GameObject chosenPrefab = Instantiate(randomPrefabs[Random.Range(0, randomPrefabs.Length)]);
            chosenPrefab.transform.SetParent(brushTarget.transform);
            chosenPrefab.transform.position = alteredPos;      

            // base.Paint(gridLayout, brushTarget, position);
        }

        //Checks if there is an prefab with the tag obstacle in the cell already
        private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3 offsetLocation){
            // First list all the children of the grid
            int childCount = parent.childCount;

            //Tranverse the children, aka obstacles, in the tilemap
            for (int i = 0; i < childCount; i++){
                Transform child = parent.GetChild(i);
                string tag = child.gameObject.tag;
                /* 
                    check if the current child is within the cell AND if it already contains an obstacle
                    Note that this works with smaller obstacles because each obstacle is only one hex in size
                    thus they will have the exact same position
                    For anything more complex that goes over several hexes more complicated math needs to be 
                    used to know if something is within the bounds of something else.
                */
                if (child.position == offsetLocation && (tag == "randomObstacle")){
                    // if so return that child
                    return child;
                }
            }
            // otherwise nothing is found and the prefab can be painted
            return null;
        }
    }
}