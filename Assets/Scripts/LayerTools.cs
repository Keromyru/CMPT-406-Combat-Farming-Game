using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//TDK443
public class LayerTools: MonoBehaviour
{
    //Return a list of every Object on a specific Layer
    public static GameObject[] FindGameObjectsWithLayer (int layer){
        return GameObject.FindObjectsOfType<GameObject>().Where(g => g.layer == layer).ToArray();
    }

    //Return a list of every Object on a specific Tag
    public static GameObject[] FindGameObjectsWithTag (string tag){
        return GameObject.FindGameObjectsWithTag(tag);
    }

    //Set A Layer To All Objects With Tag
    public static void SetLayerToTag (int layer, string tag){
        Array.ForEach(FindGameObjectsWithLayer(layer), g => {g.tag = tag;}); 
    }

    //Set A Tag To All Objects Within a layer
    public static void SetTagToLayer(string tag,int layer ){
        Array.ForEach(GameObject.FindGameObjectsWithTag(tag), g => {g.layer = layer;});
    }

}
