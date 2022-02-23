using UnityEngine;
//TDK443  See Camera Controller 
public class SetCamera : MonoBehaviour
{
    void Start(){GameCamera.SetTarget(this.gameObject);}
}
