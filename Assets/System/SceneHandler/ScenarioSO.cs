using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "Scene Data/Scenarios")]
public class ScenarioSO : GameSceneSO
{
    [Header("Scenario specific")]
    public string  ScenarioTitle;
    public challange challange;
}

public enum challange
{
    Normal,
    Challanging,
}
