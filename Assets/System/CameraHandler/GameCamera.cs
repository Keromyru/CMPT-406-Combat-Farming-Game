using UnityEngine;
public static class GameCamera 
{
    public static void SetLocation(Vector3 location){
     GameObject.Find("Main Camera").GetComponent<CameraController>().SetLocation(location);
    }
    public static GameObject GetCamera(){
     return GameObject.Find("Main Camera");
    }

    public static void Zoom(float zoom){
        GameObject.Find("Main Camera").GetComponent<CameraController>().Zoom(zoom);
    }

    public static void SetFreelook(){
         GameObject.Find("Main Camera").GetComponent<CameraController>().SetFreelook();
    }

    public static void SetTarget(GameObject newTarget){
        GameObject.Find("Main Camera").GetComponent<CameraController>().SetTarget(newTarget);
    }

    public static void SetRotation(float newRotation){
        GameObject.Find("Main Camera").GetComponent<CameraController>().SetRotation(newRotation);
    }

    public static void SetBoundingBox(int HorizontalMin, int HorizontalMax, int VerticalMin, int VerticalMax){
        GameObject.Find("Main Camera").GetComponent<CameraController>().SetBoundingBox(HorizontalMin, HorizontalMax, VerticalMin, VerticalMax);
    }

        public static Vector3 GetPosition(){
        return GameObject.Find("Main Camera").GetComponent<CameraController>().GetPosition();
    }
}
