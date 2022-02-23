using UnityEngine;
public static class GameCamera 
{
    public static void SetLocation(Vector3 location){
     GameObject.Find("Camera Rig").GetComponent<CameraController>().SetLocation(location);
    }
    public static GameObject GetCamera(){
     return GameObject.Find("Camera Rig");
    }

    public static void Zoom(float zoom){
        GameObject.Find("Camera Rig").GetComponent<CameraController>().Zoom(zoom);
    }

    public static void SetTarget(GameObject newTarget){
        GameObject.Find("Camera Rig").GetComponent<CameraController>().SetTarget(newTarget);
    }
    public static void SetRotation(float newRotation){
        GameObject.Find("Camera Rig").GetComponent<CameraController>().SetRotation(newRotation);
    }

        public static Vector3 GetPosition(){
        return GameObject.Find("Camera Rig").GetComponent<CameraController>().GetPosition();
    }

    public static void ShakeScreen(float shakeTime){
        GameObject.Find("Camera Rig").GetComponent<CameraController>().ShakeScreen(shakeTime);
    }
}
