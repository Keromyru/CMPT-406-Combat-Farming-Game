using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//TDK443
//This is a base class that other controllers for each plant should inharit
// it should hold the base stats they all should impliment, and if all goes well their actions should be controllable by modular
// SO action objects **Shrugs** 
// Hopefully this doesn't become too convoluted
public class PlantController : MonoBehaviour, IPlantControl
{
    //Interface exists in Assets/Entities/Plants/Scripts/IPlantControl.cs
    [SerializeField]private float health;
    [SerializeField]private float maxHealth;
    [SerializeField]private float energy;
    [SerializeField]private float maxEnergy;
    [SerializeField] private int growAge;
    private Vector2 location;
    private type plantType;
    
    private PlantOnHitSO onHitBehavior;
    private PlantOnAttackSO OnAttackBehavior;



    //onhit action

    //OnAttack Action

    //on new day

    ////////////////////////////////////////////////
    //Triggers
    ////////////////////////////////////////////////

    public void newDay(){
    growAge++;    

    }

    public void onHit(float damage, GameObject source){
        onHitBehavior.onHit( damage, source);    
    }

    public void onDeath(){

    }

    public void onDamage(){

    }

    private void FixedUpdate() {
        
    }

    ////////////////////////////////////////////////

    //Types
    public enum type
    {
        defense,
        produce,
        utility
    }


    //SETS 'n GETS
    public void setOnHit(PlantOnHitSO newOnHit){onHitBehavior = newOnHit; }
    public void setOnDeath(){ }
    public void setOnAttack(PlantOnAttackSO newOnAttack){ OnAttackBehavior = newOnAttack; }

    public void setLocation(Vector2 newLocation){ location = newLocation; }
    public Vector2 getLocation(){ return location; }

    public void setGrowAge(int newGrowAge){ growAge = newGrowAge; }
    public int getGrowAge(){ return growAge; }

    public void setHealth(float newHealth){ health = newHealth;}
    public float getHealth(){return health;}

    public void setMaxHealth(float newHealth){ maxHealth = newHealth;}
    public float getMaxHealth(){return maxHealth;}

    public void setEnergy(float newEnergy){ energy = newEnergy;}
    public float getEnergy(){ return energy;}

    public void setMaxEnergy(float newEnergy){ maxEnergy = newEnergy;}
    public float getMaxEnergy(){ return maxEnergy;}

    public void setType(int newType){ plantType = (type)Enum.ToObject(typeof(type), newType); }
    public int getType(){ return (int)plantType; }

}
