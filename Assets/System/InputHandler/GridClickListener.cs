using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this is exprimental and not fully implemmented
public class GridClickListener : MonoBehaviour
{
    [SerializeField]
    private GridLayout gridHoldingTilemaps; 

    public Camera cam;
    private Vector2 mousePosition;

    public PlantDatabaseSO plantDatabase;
    void Start(){
        
    }

    void Update(){
        if (Input.GetButtonDown("Fire1")){
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = gridHoldingTilemaps.WorldToCell(mousePosition);
            plantDatabase.spawnPlant("Hydra", new Vector2(cellPosition.x, cellPosition.y));
        }
    }
}
