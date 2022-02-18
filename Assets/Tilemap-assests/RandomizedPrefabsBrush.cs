using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor{

    // lets you create a new brush in the create menu 
    [CreateAssetMenu]
    [CustomGridBrush(false, true, false, "Randomized Prefab Brush")]
    public class RandomizedPrefabsBrush : GridBrushBase{
        public GameObject[] randomPrefabs;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position){
            // to ensure nothing crashes if the array is empty
            if (randomPrefabs.Length <= 0) return;

            // coord on the tilemap NOT the world, important distinction
            // can be used if we need to change the z coord to make it standard
            Vector3Int cellPosition = new Vector3Int(position.x, position.y, position.z);

            if (GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, position.z)) != null){
                return;
            }

            // randomly choose one of the obstacles (or prefabs) to paint into the scene
            GameObject chosenPrefab = Instantiate(randomPrefabs[Random.Range(0, randomPrefabs.Length)]);
            chosenPrefab.transform.SetParent(brushTarget.transform);
            chosenPrefab.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(cellPosition));         

            // base.Paint(gridLayout, brushTarget, position);
        }

        private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position){
            // First list all the children of the grid
            int childCount = parent.childCount;

            // get the world position of the grid cell that has been clicked
            Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));
            Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));
            Bounds bounds = new Bounds((max + min), max - min);

            //Tranverse the children
            for (int i = 0; i < childCount; i++){
                Transform child = parent.GetChild(i);
                string tag = child.gameObject.tag;
                // check if the current child is within the cell AND if it contains that jump pad
                if (bounds.Contains(child.position) && (tag == "randomObstacle")){
                    // if so return that child
                    return child;
                }
            }
            return null;
        }

        // [CustomEditor(typeof(RandomizedPrefabsBrush))]
        // public class RandomizedPrefabsBrushEditor : GridBrushBase{
        //     // public override GameObject[] validTargets {
        //     //     get {
        //     //         return GameObject.FindObjectOfType<Tilemap>().Select(x => x.gameObject)
        //     //     }
        //     // }

        //     public override void OnPaintSceneGUI( GridLayout grid, GameObject brushTarget){

        //     }
        // }

    }
}