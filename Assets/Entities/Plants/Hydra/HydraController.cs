using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraController : PlantController
{
    public override void onAttack(){
        base.onAttack();
        this.GetComponent<Animator>().SetTrigger("Attack");
    }

}
