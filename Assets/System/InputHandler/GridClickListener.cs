using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridClickListener : MonoBehaviour
{
    [SerializeField]
    private GridLayout gridHoldingTilemaps; 

    public Camera cam;
    private Vector2 mousePosition;

    void Start(){
        
    }

    void Update(){
        if (Input.GetButtonDown("Fire1")){
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = gridHoldingTilemaps.WorldToCell(mousePosition);
        }
    }
}
