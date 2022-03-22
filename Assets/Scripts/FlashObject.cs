using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443 
[System.Serializable]
public class FlashObject
{
    Material originalMaterial;
    SpriteRenderer spriteRenderer;
    public FlashObject(SpriteRenderer mySpriteRenderer){
        spriteRenderer = mySpriteRenderer;
        originalMaterial = mySpriteRenderer.material;
    }

    public void SetMaterial(Material material){
        spriteRenderer.material = material;
    }

    public void RestoreMaterial(){
        spriteRenderer.material = originalMaterial;
    }
}
