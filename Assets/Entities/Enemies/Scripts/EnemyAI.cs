using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class EnemyAI : MonoBehaviour
{
    private EnemyController myController;
    private Rigidbody2D myRB; 
    public List<GameObject> targetList; //Dynamic List of Targets
    private GameObject myTarget; //Destination for this baddy
    private float enemyMoveSpeed;
    private GameObject theHub;
    public List<GameObject> friendsList; //Dynamic List of Friendly Enemies
    [SerializeField] float socialDistance = 1; //sets the distance of the friendly enmies that is acceptable  **which should obviously be 6m b/c yea
    private Vector2 force;
    private float forceTime = 0.5f;

    private void Start() {
        myController = this.GetComponent<EnemyController>(); //Quick Access to the controller
        theHub = GameObject.Find("HUB");
        myTarget = theHub;
        myRB = this.GetComponent<Rigidbody2D>(); 
        enemyMoveSpeed = myController.myEnemyData.enemyMoveSpeed;
    }

    private void FixedUpdate() {
        //This Makes the Baddy Run Up To The Target
        if (myDistance() > myController.myEnemyData.attackRange - 0.2f){
            Vector3 targetWithOffset = ((
                myTarget.transform.position - this.transform.position).normalized
                * (10 - myDistance())  
                + myTarget.transform.position);

            myRB.MovePosition(force + Vector2.Lerp(this.transform.position, targetWithOffset , Time.deltaTime * enemyMoveSpeed * 0.1f));
        }
        if (force.magnitude > 0){ force = force - (force*Time.deltaTime)/forceTime;} //this reduced the bounce time

        foreach(GameObject friend in friendsList){ //CHECKS FOR SOCIAL DISTANCING
            if(Vector2.Distance(this.transform.position,friend.transform.position) < socialDistance){
                knockback(friend.transform.position, 0.1f);
            } 
        }     
        CheckTarget(); //Updated target 
    }
    private void OnTriggerEnter2D(Collider2D entity) {
        if (entity.tag == "Plant" ||
            entity.tag == "Player" ||
            entity.tag == "Structure") {
            targetList.Add(entity.gameObject); //Adds That Object From Its Attack List
            CheckTarget();
        }
        else if (entity.tag == "Enemy" || entity.tag == "Obstacle") {
            friendsList.Add(entity.gameObject); // Adds the friends to its list of friends 
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
        else if (entity.tag == "Enemy" || entity.tag == "Obstacle") {
            friendsList.Remove(entity.gameObject); // Adds the friends to its list of friends 
        }
    }

    private void CheckTarget(){ //If the target doesn't exist, or it's out of range, or it's daytime;
        if( (myTarget == null || myDistance() > myController.myEnemyData.attackRange)){
            SetTarget(FindTarget());
        }
    }

    private GameObject FindTarget(){   /// NEEDS TO RETURN A SINGLE TARGET GAMEOBJECT
        if(targetList.Count == 0){ return theHub; } // Sanity Check, not point doin' stuff if there's nothing to look at.

        foreach(TargetPriority priority in myController.myEnemyData.priorityList){
            float tDist = 1000; //Starts with an absurd distance
            GameObject potentialTarget = null; //Sets a place holder
            foreach (GameObject target in targetList){ //checks all it's targets for a new option
                if(target.tag == priority.tag){
                    float distance = (Vector3.Distance(target.transform.position, gameObject.transform.position));
                    if (distance < tDist && distance < priority.distance){ //If this distance is better than any other 
                        tDist = distance;
                        potentialTarget = target; // Sets the potential target
                    }
                }
            }
            if (potentialTarget != null){ return potentialTarget; }
        // SetTarget(potentialTarget);
        }
        return theHub;
    }
    public void knockback(Vector2 origin, float scale){   //Shoves the player away
        Vector2 knockback = (myRB.position - origin).normalized*scale;
        force = knockback*scale;
    }

    //Returns The Distance Between the baddy and its target;
    private float myDistance(){ 
        if(targetList.Count == 0){ myTarget = theHub;}
        else if (myTarget == null){ CheckTarget(); }
        return Vector3.Distance(this.transform.position, myTarget.transform.position); }
    private void SetTarget(GameObject myNewTarget){myController.attackTarget = myNewTarget; myTarget = myNewTarget;}
}
