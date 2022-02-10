using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//TDK443
public class CameraController : MonoBehaviour 
{
    [Header("General Options")]
    [SerializeField] bool canRotate = false;
    [SerializeField] bool canUseDirectional = false;
    [SerializeField] bool lockToStates = false;


    [Header("Panning Options")]
    [SerializeField] float movementTime = 1f ;
    [SerializeField] float panSpeed = 1f ;
    [Header("Rotation Options")]
    [SerializeField] bool hasRotationLock = true;
    [SerializeField] float rotationAmount;
    [SerializeField] float MaxRotationAngle;
    
    [Header("Zoom Options")]
    [SerializeField] float MaxZoom = 200f;
    [SerializeField] float MinZoom = 10f;
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    private Vector3 targetZoom;
    private GameObject followTarget;
    private Vector3 locationTarget;
    [Header("Default Bounding Box")]
    [SerializeField] private int defaultHorzMin; 
    [SerializeField] private int defaultVertMin, defaultHorzMax, defaultVertMax;
    private int horzMin, vertMin, horzMax, vertMax;
      
    //TODO:
    //- Add pitch set

    private enum State {
        Target,
        Location,
        Freelook,
    }
    private State state;
 
 

    private void Awake() {
        this.state = State.Freelook;
        transform.LookAt(this.transform.parent.transform.position);
        targetPosition = this.transform.parent.transform.position;
        targetRotation = this.transform.parent.transform.rotation;
        targetZoom = this.transform.localPosition;

        if (horzMax == 0){ horzMax = defaultHorzMax;}    
        if (horzMin == 0){ horzMin = defaultHorzMin;} 
        if (vertMax == 0){ vertMax = defaultVertMax;} 
        if (vertMin == 0){ vertMin = defaultVertMin;} 
               
    }

    private void Update() {
            CameraUpdate();
            
            if (state == State.Target){ 
                targetPosition = new Vector3(
                    followTarget.transform.position.x, 
                    this.transform.parent.transform.position.y, 
                    followTarget.transform.position.z);
                }
            
    }
    private void CameraUpdate(){
        //Movement Pathing
        targetPosition.x = Mathf.Clamp(targetPosition.x, horzMin, horzMax);
        targetPosition.z = Mathf.Clamp(targetPosition.z, vertMin, vertMax);
        this.transform.parent.transform.position = Vector3.Lerp(this.transform.parent.transform.position,targetPosition, Time.deltaTime * movementTime); //
    
    
        //Rotation Pathing
        if(hasRotationLock) {this.rotationLock();}
        this.transform.parent.transform.rotation = Quaternion.Lerp(this.transform.parent.transform.rotation, targetRotation, Time.deltaTime * movementTime);
        
        //Actual Camera Movement, mostly used to calculate a "zoom" path
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,targetZoom,Time.deltaTime * movementTime * 5);
    }

    private void rotationLock(){
        if (targetRotation.eulerAngles.y > MaxRotationAngle &&  targetRotation.eulerAngles.y < 180){ 
            targetRotation =  Quaternion.Euler(targetRotation.eulerAngles.x, MaxRotationAngle, targetRotation.eulerAngles.z);
        }
        else if (targetRotation.eulerAngles.y < 360 - MaxRotationAngle &&  targetRotation.eulerAngles.y > 180){ 
            targetRotation =  Quaternion.Euler(targetRotation.eulerAngles.x, 360 - MaxRotationAngle, targetRotation.eulerAngles.z);
        }
    }
    
    //Directional Input Calls
    public void InputUp(){
        if(canUseDirectional && state == State.Freelook){targetPosition += this.transform.parent.transform.forward * panSpeed;}
    }
    public void InputDown(){
        if(canUseDirectional && state == State.Freelook){targetPosition -= this.transform.parent.transform.forward * panSpeed;}
    }
    public void InputLeft(){
        if( canUseDirectional && state == State.Freelook){targetPosition -= this.transform.parent.transform.right * panSpeed;}
    }
    public void InputRight(){
        if(canUseDirectional && state == State.Freelook ){targetPosition += this.transform.parent.transform.right * panSpeed;}
    }

    //Free Rotate Input Calls
    public void InputClockwise(){
        if(canRotate && state == State.Freelook ){ targetRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);}
    }
    public void InputCounterClockwise(){
        if(canRotate && state == State.Freelook){targetRotation *= Quaternion.Euler(Vector3.up * rotationAmount);}
    }

    /////////// GETS ///////////
    public GameObject GetCamera(){
        return this.gameObject;
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
        if (lockToStates){this.state = State.Location;}
        
        targetPosition = newLocation;
    }


    public void SetTarget(GameObject newTarget){
        if (lockToStates){this.state = State.Target;}
        followTarget = newTarget;
    }

    public void SetRotation(float newRotation){
        this.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0f,newRotation,0f));
    }

    //SET STATE TO FREE LOOK
    public void SetFreelook(){
        this.state = State.Freelook;
    }

    //SET FREELOOK BOUNDING BOX
    public void SetBoundingBox(int HorizontalMin, int HorizontalMax, int VerticalMin, int VerticalMax){
        horzMin = HorizontalMin;
        horzMax = HorizontalMax;
        vertMin = VerticalMin;
        vertMax = VerticalMax;
    }

    // INTERFACE COMPONENTS

    public void OnClick(InputAction.CallbackContext context){

    }
    public void OnDoubleClick(InputAction.CallbackContext context){

    }
    public void OnAltClick(InputAction.CallbackContext context){

    }
    public void OnDrag(Vector2 delta){
        if (state == State.Freelook){
            float distance = ((Vector3.Distance(this.transform.position,  this.transform.parent.transform.position))*0.02f);
            targetPosition.x += -delta.x * distance;
            targetPosition.z += -delta.y * distance;
        }
    }

    public void OnUnClick(InputAction.CallbackContext context){

    }

     public void OnLongClick(InputAction.CallbackContext context){

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


    public void OnAltDrag(Vector2 deltaCursor)
    {
        throw new System.NotImplementedException();
    }

    public void OnAltDoubleClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAltUnClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAltLongClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
