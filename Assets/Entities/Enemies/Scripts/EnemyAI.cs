using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class EnemyAI : MonoBehaviour
{
    // contoller 
    private EnemyController myController;
    // for the moving
    private Rigidbody2D myRB; 
    public List<GameObject> targetList; //Dynamic List of Targets
    private GameObject myTarget; //Destination for this baddy
    private float enemyMoveSpeed;
    private GameObject theHub;
    public List<GameObject> friendsList; //Dynamic List of Friendly Enemies
    private string[] avoidList = {"Enemy","Obstacle","Structure"};
    private string[] findList = {"Plant", "Player","Structure"};
    [SerializeField] float socialDistance = 1; //sets the distance of the friendly enmies that is acceptable  **which should obviously be 6m b/c yea
    private Vector2 force;
    private float forceTime = 0.5f;
    private Vector2 myTargetPosition;
    private Vector2 myPostion; 
    private float leftOrRight; 
    private Animator myAnimation;
    //SlowStuffs
    
    

    private void Start() {
        myController = this.GetComponent<EnemyController>(); //Quick Access to the controller
        theHub = GameObject.Find("HUB");
        myTarget = theHub;
        myRB = this.GetComponent<Rigidbody2D>(); 
        enemyMoveSpeed = myController.myEnemyData.enemyMoveSpeed;
        this.myAnimation = this.GetComponent<Animator>();

    }

    private void FixedUpdate() {
        CheckTarget(); //Updated target 
        //This Makes the Baddy Run Up To The Target
        //Finding the location of my target
        if (myTarget.tag == "Structure") {  // Some magic to find the closest point of a structure
           myTargetPosition = myTarget.GetComponents<Collider2D>().OrderBy(col => Vector2.Distance(myRB.position,col.ClosestPoint(myRB.position))).First().ClosestPoint(myRB.position);
        } else {myTargetPosition = myTarget.transform.position;} //otherwise business as usual

        // SOCIAL DISTANCING
        foreach(GameObject friend in friendsList){ //CHECKS FOR SOCIAL DISTANCING
            if(Vector2.Distance(myRB.position,friend.transform.position) < socialDistance){ //If A FRAN IS TOO CLOSE
                knockback(friend.transform.position, 0.1f); //PANIK
            }
            else if (friend.tag == "Structure") { // this just keeps the baddies out of the hub
                if (Vector2.Distance(myRB.position,friend.GetComponent<PolygonCollider2D>().ClosestPoint(myRB.position)) < 0.1f){
                knockback(friend.GetComponent<PolygonCollider2D>().ClosestPoint(myRB.position), 0.15f);
                } // This checks between the different kind of colliders
                if (Vector2.Distance(myRB.position,friend.GetComponent<BoxCollider2D>().ClosestPoint(myRB.position)) < 0.1f){
                knockback(friend.GetComponent<BoxCollider2D>().ClosestPoint(myRB.position),  0.15f);
                }
            }
        }     

        //Updating movement
        if (myDistance() > myController.myEnemyData.attackRange){ //Moves to attack range
            Vector3 targetWithOffset = (
                (myTargetPosition - myRB.position).normalized // Direction
                * (myController.myEnemyData.AdditiveLerpRange - myDistance())  //Displacement of 10 units
                + myTargetPosition);
            myRB.MovePosition(force + Vector2.Lerp( myRB.position, targetWithOffset , Time.deltaTime * enemyMoveSpeed*myController.slowMulti * 0.1f)); //Actual move update
        }
        if (force.magnitude > 0){ force = force - (force*Time.deltaTime)/forceTime;} //this reduced the bounce time   
        // Facing the right direction
        leftOrRight = myRB.position.x - myTarget.transform.position.x;       
        // checks the potion of the target, changes the direction of the enemy based on what
        // direction their main target is. 
        if(leftOrRight > 0)
        {
            Quaternion aQuaternion = Quaternion.Euler(0,0,0);
            this.transform.rotation = aQuaternion;

       }
        else if(leftOrRight < 0 )
        {
            Quaternion aQuaternion = Quaternion.Euler(0,180,0);
            this.transform.rotation = aQuaternion;
            
        }
    }
    //Triggers When Something enters its range
    private void OnTriggerEnter2D(Collider2D entity) {
        if (findList.Contains(entity.tag)) {
            targetList.Add(entity.gameObject); //Adds That Object From Its Attack List
            CheckTarget();
        }
        if (avoidList.Contains(entity.tag)) {
            friendsList.Add(entity.gameObject); // Adds the friends to its list of friends 
        }
        
    
    }
        //Triggers When Something leaves its range
    private void OnTriggerExit2D(Collider2D entity) {
        if (findList.Contains(entity.tag)) {
            targetList.Remove(entity.gameObject); //Remove That Object From Its Attack List
            CheckTarget();
            if (targetList.Count == 0) { myTarget = theHub;} //Clears the target if there are not more options
        }
        if (avoidList.Contains(entity.tag)) {
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
    public void knockback(Vector2 origin, float scale){   //Bounces the entity away from whatever you put into it
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
