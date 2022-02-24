using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMenu", menuName = "Scene Data/Menus")]
public class MenuSO : GameSceneSO
{
 [Header("Menu specific")]
public Type type;

}

public enum Type
{
    Main_Menu,
    Pause_Menu,
    Settings_Menu,
}
