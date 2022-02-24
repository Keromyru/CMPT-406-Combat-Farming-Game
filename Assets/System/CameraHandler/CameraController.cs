using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//TDK443
public class CameraController : MonoBehaviour 
{
    #region Camera Configs
        
    [Header("General Options")]
    [SerializeField] float panSpeed = 1.0f;
    
    [Header("Zoom Options")]
    [SerializeField] float MaxZoom = 200f;
    [SerializeField] float MinZoom = 10f;
    [SerializeField] float movementTime = 1f ;

    [Header("Screen Shake")]
    private bool screenShake;
    private float shakeDuration;
    [SerializeField] float shakeMagnitude = 0.2f;
    [SerializeField] float dampingSpeed = 1.0f;
    private Vector3 initialPosition;
    private float timer;
    private float defaultH; 
    private Coroutine shakeRoutine;

    //********************************//
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    private Vector3 targetZoom;
    private GameObject followTarget;
    private Vector3 locationTarget;
    //********************************//
    private GameObject CameraRig;
    private Camera Mcamera;
    //********************************//
    private enum State {
        Target,
        Location,
        Freelook,
    }
    private State state;
    #endregion
    private void Awake() {
        //Sets GameObject Shortcuts
        CameraRig = this.gameObject;
        Mcamera = CameraRig.GetComponentInChildren<Camera>();
        //Sets Camera's starting location as it's current location so that it doesn't fly off into the boonies.
        this.state = State.Freelook;
        transform.LookAt(CameraRig.transform.position);
        targetPosition = CameraRig.transform.position;
        targetRotation = CameraRig.transform.rotation;
        targetZoom = Mcamera.transform.localPosition;
    } 
               
    private void FixedUpdate() {
        CameraUpdate();
        //Updates the Cameras Target if the State is Set to Target
        if (state == State.Target){  
            targetPosition = new Vector3(
                followTarget.transform.position.x, 
                CameraRig.transform.position.y, 
                followTarget.transform.position.z);
            } 
        if (screenShake){Shake();} //Shakes the screen so long as the Coroutine be trippin'
    }

    private void CameraUpdate(){
        //Movement Pathing
        CameraRig.transform.position = Vector3.Lerp(CameraRig.transform.position,targetPosition, Time.deltaTime * movementTime); //
    
        //Actual Camera Movement, mostly used to calculate a "zoom" path
        //Interpolates a coordinate between where it is and a location nearer to the local origin
        Mcamera.transform.localPosition = Vector3.Lerp(Mcamera.transform.localPosition,targetZoom,Time.deltaTime * movementTime * 5);
    }


    /////////// GETS ///////////
    public GameObject GetCamera(){
        return Mcamera.gameObject;
    }

    //Passes the cameras tranfrom
    public Transform GetTransform(){
        return this.gameObject.transform;
    }

    public Vector3 GetPosition(){
        return transform.position;
    }

    /////////// SETS ///////////
    // Sets the camera to focus to a coordinate
    public void SetLocation(Vector3 newLocation){
        this.state = State.Location;
        targetPosition = newLocation;
    }
    // Sets the camera to focus to a gameObject
    public void SetTarget(GameObject newTarget){
        this.state = State.Target;
        followTarget = newTarget;
    }

    // Sets the angle of view of the camera on the origin
    public void SetRotation(float newRotation){
        CameraRig.transform.rotation = Quaternion.Euler(new Vector3(0f,newRotation,0f));
    }

    public void Zoom(float zoom){
        //Zoom is a float value between -1 and 1 dictating intensity and direction.

        //Gets the inverse of the local orientation of the camera, as it will always be pointing at its local 
        //0,0,0 coord. This then applies a zoom in and out based on an applied value between -1 and +1 as is expected with the
        // a mouse wheel, but easily applicable to other inputs
        Vector3 toCenter = -Mcamera.transform.localPosition.normalized;
        //Zoom In
        if(zoom > 0  && targetZoom.y > MinZoom) { 
            targetZoom +=  (toCenter * panSpeed);
        }
        //Zoom Out
        else if (zoom < 0 && MaxZoom > targetZoom.y) { 
            targetZoom += -(toCenter * panSpeed);
        }  
    }
    
    private void Shake(){
        //Shake = Cameras local position + a random vector3 between 0-1 * a magnitude to indicate how much
        Vector3 shake = Mcamera.transform.localPosition + Random.insideUnitSphere * shakeMagnitude;
        //Removed the Y value to keep the camera from drifting too much
        shake.y = initialPosition.y;
        //Sets the current location with out any interpolation makeing the effect slightly jarring
        Mcamera.transform.localPosition = shake;
    }


     public void ShakeScreen(float shakeTime){
        //Alters the time based on a global modifier
        shakeDuration = shakeTime * dampingSpeed;
        if (shakeRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple ShakeRoutine the same time would cause bugs.
            StopCoroutine(shakeRoutine);
        }
        // Start the Coroutine, and store the reference for it.
        shakeRoutine = StartCoroutine(ShakeRoutine());            
     }

    private IEnumerator ShakeRoutine()
    {
        //Stores original local position
        initialPosition = Mcamera.transform.localPosition;
        //toggles on the screen shake function
        screenShake = true;
        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(shakeDuration);
        // After the pause, swap back to the original material.
        screenShake = false;
        //returns default position
        Mcamera.transform.localPosition = initialPosition;
        // Set the routine to null, signaling that it's finished.
        shakeRoutine = null;
    }
}
