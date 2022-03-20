using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TDK443 
[System.Serializable]
class FadeObject  
{
    public Image image;
    public float defaultAlpha;
    public FadeObject(Image myImage){
        image = myImage;
        defaultAlpha = myImage.color.a; 
    }

    public void setAlpha(float newAlpha){ //Set Alpha while retaining original
        if (newAlpha <= defaultAlpha ){ image.color = new Color( image.color.r, image.color.g, image.color.b, newAlpha);}
    }
}