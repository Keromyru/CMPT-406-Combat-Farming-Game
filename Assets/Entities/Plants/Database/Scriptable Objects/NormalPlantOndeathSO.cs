using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443

[CreateAssetMenu(fileName = "OnDeath Normal", menuName = "Plant Data/Plant Action/OnDeath Normal")]
public class NormalPlantOndeathSO : PlantOnDeathSO
{
    //Nothing Special, just kills the object;
    [SerializeField] GameObject deathEffect; 
    public override void onDeath(GameObject thisObject)
    {
        //Plays a death effect if there is one
        if(deathEffect != null) {Instantiate(this.deathEffect, thisObject.transform.position, thisObject.transform.rotation);}
        GameObject.Find("HexGrid").GetComponentInChildren<GridClickListener>().restoreTile(thisObject.GetComponent<IPlantControl>().getLocation());
        Destroy(thisObject);
    }

}
