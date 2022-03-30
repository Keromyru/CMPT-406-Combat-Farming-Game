using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//TDK
[CreateAssetMenu(fileName = "CursorController", menuName = "System/CursorController")]
public class CursorsSO : ScriptableObject
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D combatCursor;
    [SerializeField] Texture2D gigaCursor;
    [SerializeField] Texture2D hiveCursor;
    [SerializeField] Texture2D potatCursor;
    [SerializeField] Texture2D hydraCursor;

    public void setCursorDefault(){ Cursor.SetCursor(defaultCursor, Vector2.zero,CursorMode.Auto);}
    public void setCursorCombat(){ Cursor.SetCursor(combatCursor, new Vector2(50,50),CursorMode.Auto);}
    public void setCursorGiga(){ Cursor.SetCursor(gigaCursor, Vector2.zero,CursorMode.Auto);}
    public void setCursorHive(){ Cursor.SetCursor(hiveCursor, Vector2.zero,CursorMode.Auto);}
    public void setCursorPotato(){ Cursor.SetCursor(potatCursor, Vector2.zero,CursorMode.Auto);}
    public void setCursorHydra(){ Cursor.SetCursor(hydraCursor, Vector2.zero,CursorMode.Auto);}
}

public static class myCursor{
    public static void setDefault(){ Resources.FindObjectsOfTypeAll<CursorsSO>().First().setCursorDefault(); }
    public static void setCombat(){  Resources.FindObjectsOfTypeAll<CursorsSO>().First().setCursorCombat();}
    public static void setGiga(){  Resources.FindObjectsOfTypeAll<CursorsSO>().First().setCursorGiga();}
    public static void setHive(){  Resources.FindObjectsOfTypeAll<CursorsSO>().First().setCursorHive();}
    public static void setPotato(){  Resources.FindObjectsOfTypeAll<CursorsSO>().First().setCursorPotato();}
    public static void setHydra(){  Resources.FindObjectsOfTypeAll<CursorsSO>().First().setCursorHydra();}
}
