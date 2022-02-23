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
    private Coroutine flashRoutine;

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
        CameraRig = this.gameObject;
        Mcamera = CameraRig.GetComponentInChildren<Camera>();

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
    // Sets a look at Coordinate
    public void SetLocation(Vector3 newLocation){
        this.state = State.Location;
        targetPosition = newLocation;
    }

    public void SetTarget(GameObject newTarget){
        this.state = State.Target;
        followTarget = newTarget;
    }

    public void SetRotation(float newRotation){
        CameraRig.transform.rotation = Quaternion.Euler(new Vector3(0f,newRotation,0f));
    }

    public void Zoom(float zoom){
        //Vector3 toCenter = (transform.parent.transform.position - transform.position).normalized;
        Vector3 toCenter = -transform.localPosition.normalized;
        if(zoom > 0  && targetZoom.y > MinZoom) { //Zoom In
            targetZoom +=  (toCenter * panSpeed);
        }
        else if (zoom < 0 && MaxZoom > targetZoom.y) { //Zoom Out
            targetZoom += -(toCenter * panSpeed);
        }  
    }
    
    private void Shake(){
            Vector3 shake = Mcamera.transform.localPosition + Random.insideUnitSphere * shakeMagnitude;
            shake.y = initialPosition.y;
            transform.localPosition = shake;
    }


     public void ShakeScreen(float shakeTime){
            shakeDuration = shakeTime * dampingSpeed;
            if (flashRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(flashRoutine);
            }

             flashRoutine = StartCoroutine(ShakeRoutine());
     }

    private IEnumerator ShakeRoutine()
    {
        initialPosition = Mcamera.transform.localPosition;
        screenShake = true;
        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(shakeDuration);

        // After the pause, swap back to the original material.
        screenShake = false;
        Mcamera.transform.localPosition = initialPosition;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
