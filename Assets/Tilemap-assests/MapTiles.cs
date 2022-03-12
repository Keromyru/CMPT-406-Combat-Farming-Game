using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

// experimental tiles that can change their sprites HOWEVER 
// this is unlikely the way we want to go about having plants in the tiles
// this may end up being discarded entirely
public class MapTiles : Tile{

    // Plantable tiles
    [SerializeField] private Sprite[] plantableTileSprites;
    [SerializeField] private Sprite preview;

    // For random adornments to make the tiles look less bland
    [SerializeField] private Sprite[] TileAdornments;

    // This is calleed whenever you place a new tile into the game, on the cell that you have placed it in the tilemap
    // Could be used to randomly assign or change adornments based on position or other tiles around it
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }
}
