using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///////////////////////////////////////////////////////////////
///    Session Handler               TDK443
///////////////////////////////////////////////////////////////
[CreateAssetMenu(fileName = "SessionHandler", menuName = "System/Session Handler")]
public class SessionHandlerSO : ScriptableObject
{
    [SerializeField] private string SelectedScenario;
    [SerializeField] private int SelectedSaveSlot;

    



///////////////////////////////////////////////////////////////
///    Settings
///////////////////////////////////////////////////////////////
    [SerializeField] private float MusicVolume;
    [SerializeField] private float MasterVolume;
    [SerializeField] private float EffectsVolume;

///////////////////////////////////////////////////////////////
///    STATS
///////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////
public string GetSelectedScenario(){return SelectedScenario;}
public void SetSelectedScenario(string scenario){ SelectedScenario = scenario;}
public int GetSelectedSaveSlot(){return SelectedSaveSlot;}
public void SetSelectedSaveSlot(int slot){ SelectedSaveSlot = slot;}

}
