using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public class EffectsController : MonoBehaviour
{
    [SerializeField] float delay = 0f;
    [SerializeField] AudioClipSO SoundEffects;
    //Deletes The Effect When It's Done
     void Start () {
         if (SoundEffects != null) { SoundEffects.Play();}
         Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
     }
}
