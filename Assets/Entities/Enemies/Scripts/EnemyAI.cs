using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyController myController;
    private Rigidbody2D myRB; 
    public List<GameObject> targetList; //Dynamic List of Targets
    private GameObject myTarget; //Destination for this baddy
    private float enemyMoveSpeed;
    private GameObject theHub;

    private void Start() {
        myController = this.GetComponent<EnemyController>(); //Quick Access to the controller
        theHub = GameObject.Find("Player");
        myTarget = theHub;
        myRB = this.GetComponent<Rigidbody2D>(); 
        enemyMoveSpeed = myController.myEnemyData.enemyMoveSpeed;
    }

    private void FixedUpdate() {
        //TODO: MAKE A THING THAT MAKES IT SO YOUR LIL DUDE DOESN"T WALK ALL UP IN IT'S LIL FRIENDS
        //Debug.Log(this.gameObject.name+" is attacking " + myTarget.name + " and it's "+ Distance() + " units away");
        //This Makes the Baddy Run Up To The Target
        if (Distance() > myController.myEnemyData.attackRange - 0.2f){
            Vector3 targetWithOffset = ((myTarget.transform.position - this.transform.position).normalized + myTarget.transform.position);
            myRB.MovePosition(Vector3.Lerp(this.transform.position, targetWithOffset , Time.deltaTime * enemyMoveSpeed * 0.25f));
        }
        
    }
    private void OnTriggerEnter2D(Collider2D entity) {
        if (entity.tag == "Plant" ||
            entity.tag == "Player" ||
            entity.tag == "Structure") {
            targetList.Add(entity.gameObject); //Adds That Object From Its Attack List
            CheckTarget();
        }

        
        
    }
    private void OnTriggerExit2D(Collider2D entity) {
        if (entity.tag == "Plant" ||
            entity.tag == "Player" ||
            entity.tag == "Structure") {
            targetList.Remove(entity.gameObject); //Remove That Object From Its Attack List
            CheckTarget();
            if (targetList.Count == 0) { myTarget = theHub;} //Clears the target if there are not more options
        }
    }

    private void CheckTarget(){ //If the target doesn't exist, or it's out of range, or it's daytime;
        if( (myTarget == null || Distance() > myController.myEnemyData.attackRange)){
            myTarget = theHub;
            FindTarget();
        }
    }

    private void FindTarget(){
        float tDist = 1000; //Starts with an absurd distance
        GameObject potentialTarget = null; //Sets a place holder
        foreach (GameObject target in targetList){ //checks all it's targets for a new option
            float distance = (Vector3.Distance(target.transform.position, gameObject.transform.position));
            if (distance < tDist){ //If this distance is better than any other 
                tDist = distance;
                potentialTarget = target; // Sets the potential target
            } 
        }
        SetTarget(potentialTarget);
    }

    //Returns The Distance Between the baddy and its target;
    private float Distance(){ return Vector3.Distance(this.transform.position, myTarget.transform.position); }
    private void SetTarget(GameObject myNewTarget){myController.attackTarget = myNewTarget; myTarget = myNewTarget;}
}
