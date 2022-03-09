using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Original script given to Blake by TK
// Edited version by Blake

public class PlayerShoot : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    // Added A camera variable
    public GameObject mainCamera;

    public float bulletDamage; 
    public float fireRate = 1;

    // The inputs from the input system
    private PlayerInput playerInput;

    private float timer; 

    private bool canFire; 

    [SerializeField] float bulletForce;

    [SerializeField] float firePointDistance;
    [SerializeField] float deadZone = 0.2f;
    private Vector3 MouseDirection;

    private Vector3 rotation;


    // Next three functions are to get the Player Input from the other scene
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("InputHandler") != null)
        {
            playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();
        }
    }


    private void Update() {
        // Commented out this if statement might possibly be used for later
/*        if (gameObject.GetComponent<IPlayerInterface>().isPaused() == false)   
        { }*/

        // Finds where the mouse is
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = -1000;
        Vector3 MousePosition = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
        MousePosition.y += 527f;
        MousePosition.z = 0f;
        float MouseDistance = Vector3.Distance(MousePosition, gameObject.transform.position);

        //Aims the "turret"
        if (MouseDistance > deadZone)
        { //Just makes a deadzone so the cursor doesn't freak out
            MouseDirection = (MousePosition - gameObject.transform.position).normalized;
            MouseDirection.z = 0;
        }
        
        float angle = Mathf.Atan2(MouseDirection.y, MouseDirection.x)* Mathf.Rad2Deg - 90f;
            
        //Displacement for the "Turret"
        // firepoint.eulerAngles = new Vector3(0,0,angle);
        rotation = new Vector3(0,0,angle);
        firepoint.position = gameObject.transform.position + (MouseDirection.normalized)*firePointDistance;
        /// END OF AIM


        //// FIRE RATE CONTROL
        if(timer > 0  && !canFire)  
        {
        timer -= Time.deltaTime;    
        }
        else
        {
            canFire = true;
            // Made the firerate level 0.5f for now
            int level = 1;
            /*int level = gameObject.GetComponent<PlayerStats>().fireRate;*/
            timer = fireRateTime(level);
        }

        
        if ((playerInput.actions["PrimaryAction"].ReadValue<float>() > 0) && canFire)
        {
            Shoot();
            canFire=false;
        }
    }

    private void Shoot()
    {
        // Have not had time to add actual damage to enemies yet, will be added later
        /*bulletDamage = gameObject.GetComponent<PlayerStats>().bulletDamage;*/
        GameObject bullet =  Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.transform.eulerAngles = rotation;
        Rigidbody2D rb  = bullet.GetComponent<Rigidbody2D>();
        /*bullet.GetComponent<RefBullet>().bulletDamage = bulletDamage;*/

        rb.AddForce(new Vector3(-MouseDirection.x, -MouseDirection.y, 0) * bulletForce, ForceMode2D.Impulse);
        // Another way to make bullet move
        /*rb.velocity = new Vector3(-MouseDirection.x, -MouseDirection.y, 0) * bulletForce;*/
    }

    // Changes the bullet firerate based on the integer given
    private float fireRateTime(int level)
    {
        float rate;
        switch(level)
        {
        case 1:
            rate = 0.5f;
            break;
        case 2:
            rate = 0.4f;
            break;
        
        case 3:
            rate = 0.3f;
            break;

        case 4:
            rate = 0.2f;
            break;

        case 5:
            rate = 0.1f;
            break;
        default:
            rate = 1;
            break;

        }

        return rate;

    }
}
