using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;
//TDK443
public class PlayerController : MonoBehaviour, IPlayerControl, ITakeDamage
{
    private float health = 100;

    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject wateringCan;
    [SerializeField] GameObject ExoMan;
    [SerializeField] GameObject Raygun;
    [SerializeField] GameObject EnemyArrow;
    [SerializeField] GameObject HubArrow;
    [SerializeField] float firePointLength = 1;
    [SerializeField] float enemyArrowLength = 1;
    [SerializeField] float hubArrowLength = 1;


    //Behaviors
    [SerializeField] PlayerBevahviorSO myPlayerData;
    private PlayerOnAttackSO onAttackBehavior;
    private PlayerOnHitSO onHitBehavior;
    private PlayerOnDeathSO onDeathBehavior;
    private AudioControllerSO audioController;

    // Data
    private GameObject myCamera;
    private PlayerInput playerInput;  // The inputs from the InputSystem
    private Rigidbody2D playerRB;     
    UnityEvent event_PlayerHealthChange = new UnityEvent();
    public GameObject[] myFarm;
    private GameObject theHub;
    private UnityEngine.Experimental.Rendering.Universal.Light2D myLight; 
    private SpriteRenderer gunRenderer;
    private Animator wateringAni;
    private Animator myAnimator;

    // Attack Data  
    private Coroutine actionRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again 
    private Vector2 force;
    private float forceTime = 0.2f;
    bool IsDay; //Just for reference if it's day
    
    void Start(){GameCamera.SetTarget(this.gameObject);}//Sets the player as the camera focus

    //PLAYER LOGIC
    private void Awake() { 
        myCamera = GameObject.Find("Main Camera"); //Find and set camera
        playerRB = this.GetComponent<Rigidbody2D>(); //Set Rigid Body Shortcut for Blakes Code      
        theHub =  GameObject.Find("HUB"); //Find and set hub reference
        myLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        gunRenderer = Raygun.GetComponentInChildren<SpriteRenderer>(); //Set gun reference
        wateringAni = wateringCan.GetComponentInChildren<Animator>();
        myAnimator = ExoMan.GetComponentInChildren<Animator>();

        onAttackBehavior = myPlayerData.onAttackBehavior; //Set onAttackBehavior
        onHitBehavior = myPlayerData.onHitBehavior; //Set onHitBehavior
        onDeathBehavior = myPlayerData.onDeathBehavior; //Set onDeathBehavior
        audioController = myPlayerData.audioController; //Set audioController   
    }

    private void Update() {
        //Checks if the mouse click is down, and if the reset timer isn't set
        //The behavior of this will be shots so long as  the left-click is held
        if ((playerInput.actions["PrimaryAction"].ReadValue<float>() > 0) && !isWaiting){
            if(IsDay){
                if (myFarm.Length != 0) {
                    GameObject myPlant = (myFarm.OrderBy(plants => fromPointer(plants)).First());
                    Debug.Log("Is Day:"+IsDay+"    myPlants"+myPlant);
                    if (myPlant != null && fromPlayer(myPlant) < myPlayerData.interactionRange){
                        if(myPlant.GetComponent<IPlantControl>().HarvestReady()){
                             Debug.Log("Harvesting "+myPlant.name);
                            onHarvest(myPlant);
                        } else {
                            Debug.Log("Watering "+myPlant.name);
                            onWater(myPlant);
                        }
                    }
                }    
            } 
            else {
                onAttack();
            }
        }        
    }

    private void FixedUpdate() {
        // Written by Blake Williams
        // Gets the movement input and applies a constant velocity to the player
        Vector2 inputVector = playerInput.actions["PlayerMovement"].ReadValue<Vector2>();
        playerRB.MovePosition(playerRB.position + force + new Vector2(inputVector.x, inputVector.y) * myPlayerData.moveRate);
        if (force.magnitude > 0){ force = force - (force*Time.deltaTime)/forceTime;} // Reduces the force added
        if (inputVector.x == -1){
            // flip rotational y to 0
            // Needs to be a Quaternion as rotation is only described as one
            Quaternion aQuaternion = Quaternion.Euler(0, 0, 0);
            playerTransform.rotation = aQuaternion; 
            if(wateringCan.activeSelf == true){ wateringCan.transform.rotation = aQuaternion;}
        } else if (inputVector.x == 1){
            // flip rotational y to 180
            // Needs to be a Quaternion as rotation is only described as one
            Quaternion aQuaternion = Quaternion.Euler(0, 180, 0);
            playerTransform.rotation = aQuaternion;
            if(wateringCan.activeSelf == true){ wateringCan.transform.rotation = aQuaternion;}
        } 

        if (inputVector.x != 0){
            myAnimator.SetTrigger("Run");
        } else {
             myAnimator.SetTrigger("Idle");
        }

        //RAYGUN ANGLE AND DIRECTION
        if(Raygun.activeSelf == true){
               
            // a Normalized Vector * the distance from the focal point desired + from the source of the 
            Vector3 sLocation = playerTransform.position; //Source of bullets location
            Vector3 tLocation = pointerLocation();
            Vector3 targetDirection =  (tLocation - sLocation).normalized; //Direction

            Vector3 trackedLocation = (targetDirection * firePointLength)  + sLocation;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg ; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
            Quaternion trackedRotation =   Quaternion.Euler(0,0,angle);
            if(trackedLocation.x < sLocation.x){ Raygun.GetComponentInChildren<SpriteRenderer>().flipY = true ;} else {Raygun.GetComponentInChildren<SpriteRenderer>().flipY = false;}
            Raygun.transform.position = trackedLocation;
            Raygun.transform.rotation = trackedRotation;
        }
        //ENEMY TRACKER
        if(IsDay == false){
            if (GameObject.FindGameObjectWithTag("Enemy") != null){ //If there is a baddy
                GameObject myBaddy = (GameObject.FindGameObjectsWithTag("Enemy").OrderBy(baddy => fromPlayer(baddy)).First()); //Check all baddies, and return the closest
                if (fromPlayer(myBaddy) > 6) {
                    EnemyArrow.SetActive(true);
                    // a Normalized Vector * the distance from the focal point desired + from the source of the 
                    Vector3 sLocation = playerTransform.position; //Source of bullets location
                    Vector3 tLocation = myBaddy.transform.position;
                    Vector3 targetDirection =  (tLocation - sLocation).normalized; //Direction

                    Vector3 trackedLocation = (targetDirection * enemyArrowLength)  + sLocation;
                    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 90; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
                    Quaternion trackedRotation =   Quaternion.Euler(0,0,angle);
                    EnemyArrow.transform.position = trackedLocation;
                    EnemyArrow.transform.rotation = trackedRotation;
                } else {
                    EnemyArrow.SetActive(false);
                }
                
            } else {
                EnemyArrow.SetActive(false);
            }
        } else {
            EnemyArrow.SetActive(false);
        }
        //HUB TRACKER
         if (fromPlayer(theHub) > 10) {
                    HubArrow.SetActive(true);
                    // a Normalized Vector * the distance from the focal point desired + from the source of the 
                    Vector3 sLocation = playerTransform.position; //Source of bullets location
                    Vector3 tLocation = theHub.transform.position;
                    Vector3 targetDirection =  (tLocation - sLocation).normalized; //Direction

                    Vector3 trackedLocation = (targetDirection * hubArrowLength)  + sLocation;
                    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 90; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
                    Quaternion trackedRotation =   Quaternion.Euler(0,0,angle);
                    
                    HubArrow.GetComponent<RectTransform>().position = trackedLocation;
                    HubArrow.transform.rotation = trackedRotation;
                } else {
                    HubArrow.SetActive(false);
                }

    }

    //Pointer Location from mask layered raycast
    private Vector2 pointerLocation(){
        Vector3 PointerPosition = Vector3.zero;

        // Bit shift the index of the layer (16) to get a bit mask
        int layerMask = 1 << 16;
        // This would cast rays only against colliders in layer 16.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        // Does the ray intersect any objects on the raycast layer
        if (Physics.Raycast (ray, out hit, Mathf.Infinity,layerMask)) {
            PointerPosition = hit.point; //Vector2
        }
        else { 
            //Pointer Location Extracted from Blakes Code 
            //This is defaulted too if the raycast doesnt' work for whatever reason
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = -1000;
            PointerPosition = myCamera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            PointerPosition.y += 523f;
            PointerPosition.z = 0f;
        }
        return PointerPosition;
    }

    private IPlantControl pointerObject(){
        //Argument: Location of a pointer
        //Returns Object At the location of the Pointer
        IPlantControl clickedInterface = null;
        Ray ray = Camera.main.ScreenPointToRay(pointerLocation());
        if (Physics.Raycast(ray, out RaycastHit hit3D)) {
            GameObject clickedObject = hit3D.collider.gameObject;
            if (clickedObject != null){ clickedInterface = clickedObject.GetComponent<IPlantControl>(); }
        }
        return clickedInterface;
    }

    public void knockback(Vector2 origin, float scale){   //Shoves the player away
        Vector2 knockback = (playerRB.position - origin).normalized*scale;
        force = knockback*scale;
    }

    public void resetHealth(){ //Reset health
        health = myPlayerData.maxHealth;
        event_PlayerHealthChange.Invoke();
    }
    
    public void heal(float healValue){ //Restore health by value
        if (myPlayerData.soundHeal != null) {audioController.Play(myPlayerData.soundHeal);} //Play myPlayerData.soundHeal if the file has been declared
        health += healValue;
        if (health > myPlayerData.maxHealth){ health = myPlayerData.maxHealth;}
        event_PlayerHealthChange.Invoke();
    }

        
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DayNightCycle.isNowDay += newDay;
        DayNightCycle.isNowNight += newNight;
        } //Subscribe to on Scene Loaded Event

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        DayNightCycle.isNowDay -= newDay;
        DayNightCycle.isNowNight -= newNight;
        } //unsubscribe to on Scene Loaded Event

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { //If not null, add player input to "playerInput"
        if (GameObject.Find("InputHandler") != null) { playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();}
        }
    
    private float fromPlayer(GameObject other){
        return Vector2.Distance(other.transform.position, this.transform.position);
    }

    private float fromPointer(GameObject other){
        if (other != null) {return Vector2.Distance(pointerLocation(), other.transform.position);}
        else{ return 1000;}
    }



    //TRIGGERS
    public void onHit(float damage, GameObject source)
    {    //if it's a baddy, take the damage
        if (source.tag == "Enemy"){ 
            health -= onHitBehavior.onHit(damage,source, this.gameObject);
            if (myPlayerData.soundHurt != null) {audioController.Play(myPlayerData.soundHurt);} //Play soundHurt if the file has been declared
        }
        GetComponent<FlashEffect>().flash();
        knockback(source.transform.position, 0.25f);
        event_PlayerHealthChange.Invoke();
        //Death Check
        if(health <= 0){ onDeath();}
    }

    public void newDay(){ //On a new day
        resetHealth();
        myFarm = GameObject.FindGameObjectsWithTag("Plant");
        if(IsDay == false){myCursor.setDefault();}
        IsDay = true;
        Raygun.SetActive(false);
        wateringCan.SetActive(true);
        LampOff();
    }
   
    public void newNight(){
        IsDay = false;
        Raygun.SetActive(true);
        wateringCan.SetActive(false);
        myCursor.setCombat();
        gunRenderer.sprite = onAttackBehavior.GunSprite; //Set GunSprite
        LampOn();
    }

    public void onDeath(){
        if (myPlayerData.soundDeath != null) {audioController.Play(myPlayerData.soundDeath);} //Play soundDeath if the file has been declared
        onDeathBehavior.onDeath(this.gameObject);
    }
    
    //Should not be triggered 
    public void onAttack(){
        if (!isWaiting){
            onAttackBehavior.OnAttack(
                onAttackBehavior.damage,
                pointerLocation(),
                this.gameObject
            );
            ActionTimer(onAttackBehavior.fireRate);//START TIMER
        }
    }
    public void onHarvest(GameObject myPlant){
        myPlant.GetComponent<IPlantControl>().onHarvest();

         ActionTimer(1f);
    }
    public void onWater(GameObject myPlant){
        if( myPlant.GetComponent<IPlantControl>() != null && fromPointer(myPlant) < 1) {
            myPlant.GetComponent<IPlantControl>().waterPlant(myPlayerData.WaterQuantity);   //Water plant
            if (myPlayerData.soundWater != null) {audioController.Play(myPlayerData.soundWater);} //Play sound if there is one
            if (myPlayerData.WaterEffect != null) {
                Instantiate(myPlayerData.WaterEffect,
                new Vector3 (myPlant.transform.position.x, myPlant.transform.position.y-0.2f,0), 
                Quaternion.identity);}
                wateringAni.SetTrigger("PourWater");
        }
        ActionTimer(myPlayerData.WaterRate); 
    }

    ////////////////////////////////////////////////
    // Attack Interval Coroutine

    //Starts attack Timer
    public void ActionTimer(float timer){
        if (actionRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple AttackRoutine at the same time would cause bugs.
            StopCoroutine(actionRoutine);
        }
        // Start the Coroutine, and store the reference for it.
        actionRoutine = StartCoroutine(ActionRoutine(timer));            
     }

    private IEnumerator ActionRoutine(float timer){
        //toggles on the screen shake function
        isWaiting = true;
        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(timer);
        // After the pause, swap back to the original material.
        isWaiting = false;
        // Set the routine to null, signaling that it's finished.
        actionRoutine = null;
    }

    ////////////////////////////////////////////////////////////////
    // LAMP EFFECTS
    private void LampOn(){
        StartCoroutine(LightOnRoutine(2,myPlayerData.lightOnDelay));
    }
    private void LampOff(){
        StartCoroutine(LightOffRoutine(2,myPlayerData.lightOffDelay));
    }


    private IEnumerator LightOffRoutine( int fadeSpeed = 2, float timer = 0) { 
        yield return new WaitForSeconds(timer);
        float fadeKey = 1;
        while (fadeKey > 0) {
            fadeKey -= Time.fixedDeltaTime*(1f/fadeSpeed);
            myLight.intensity = fadeKey;       
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator LightOnRoutine( int fadeSpeed = 2, float timer  = 0) { 
        yield return new WaitForSeconds(timer);
        float fadeKey = 0;
        while (fadeKey < 1) {
            fadeKey += Time.fixedDeltaTime*(1f/fadeSpeed);
            myLight.intensity = fadeKey;       
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }





    ////////////////////////////////////////////////
    #region Sets and Gets
    ////////////////////////////////////////////////
    //SETS 'n GETS
    public void setHealth(float newHealth){ health = newHealth;}
    public float getHealth(){return health;}
    public float getMaxHealth(){return myPlayerData.maxHealth;}
    public Vector2 getLocation(){return this.gameObject.transform.position;}
    public void setLocation(Vector2 newLocation){ playerRB.position = newLocation;}
    public void setNewOnAttack(PlayerOnAttackSO newAttack){
        onAttackBehavior = newAttack;
        Raygun.GetComponentInChildren<SpriteRenderer>().sprite = onAttackBehavior.GunSprite;
    }
    #endregion Sets and Gets
}
