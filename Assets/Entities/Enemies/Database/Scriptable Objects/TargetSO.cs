using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetData", menuName = "Enemy Data/Enemy TargetData")]
public class TargetSO : ScriptableObject
{
    [Header("Target Data")]
    public string targetName;

}
