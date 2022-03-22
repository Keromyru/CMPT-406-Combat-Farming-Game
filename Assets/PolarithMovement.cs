using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// melissa Is attempt to make the ai not shitty 

using Polarith.AI.Move;

public class PolarithMovement : MonoBehaviour
{
   	
	private Transform target; 
	private EnemyController controller; // it has the movement speed in it. 



	private AIMContext aimContext;

    // Start is called before the first frame update
    
    void Start()
    {
        this.aimContext = this.GetComponent<AIMContext>();
        this.controller = this.GetComponent<EnemyController>();
        
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
        {
            var dist = Vector3.Distance(this.target.position, transform.position);
        }
    }

    private void pathfind()
    {
        Vector3 moveDirection = Vector3.MoveTowards(transform.position, this.transform.position + this.aimContext.DecidedDirection, this.controller.enemyMoveSpeed*Time.fixedDeltaTime);
    }
}
