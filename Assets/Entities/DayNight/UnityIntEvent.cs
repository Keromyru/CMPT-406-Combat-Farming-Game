using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

//Used to support serializing for generic attributes
//Allows events to show up in the Unity Editor
//Inheritance from unity event using the variable argument and making it serializable
namespace Tooling
{
    [Serializable] 
    public class UnityIntEvent : UnityEvent<int>
    {

    }
}