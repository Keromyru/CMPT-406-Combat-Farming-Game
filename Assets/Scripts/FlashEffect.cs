using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class FlashEffect : MonoBehaviour
{
    [Header("Flash Effect Options"), Space] 
    [SerializeField] float flashDuration = 0.2f;
    [SerializeField] private Material flashMaterial;
    private Coroutine flashRoutine;

    private FlashObject[] myFlashObjects;

    private void OnEnable() {
        flashMaterial = new Material(flashMaterial);
        //Collect All Parts Into An Array
        myFlashObjects = this.gameObject.GetComponentsInChildren<SpriteRenderer>().Select(SP => new FlashObject(SP)).ToArray();
    }

    public void flash()
    {
         // If the flashRoutine is not null, then it is currently running.
            if (flashRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(flashRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            flashRoutine = StartCoroutine(FlashRoutine());

    }

    private IEnumerator FlashRoutine()
        {
            // Swap to the flashMaterial.
            Array.ForEach(myFlashObjects, s => s.SetMaterial(flashMaterial));

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(flashDuration);

            // After the pause, swap back to the original material
            Array.ForEach(myFlashObjects, s => s.RestoreMaterial());

            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
        }
}
