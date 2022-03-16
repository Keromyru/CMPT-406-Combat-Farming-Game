using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

///////////////////////////////////////////////////////////////
///    Session Handler               TDK443
///////////////////////////////////////////////////////////////
[CreateAssetMenu(fileName = "SessionHandler", menuName = "System/Session Handler")]
public class SessionHandlerSO : ScriptableObject
{
    [SerializeField] private string SelectedScenario;
    [SerializeField] private int SelectedSaveSlot;

    UnityEvent event_CurrancyChange;
    public int playerCurrency;
    public float gameTime;
    public bool sessionTimerOn;

    private void Start() {
        if (event_CurrancyChange == null) { event_CurrancyChange = new UnityEvent();} //Creates the event

    }
    private void FixedUpdate() {
        if (sessionTimerOn) {gameTime += Time.deltaTime;} //Time Acclamation
    }


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

    public int getCurrency() { return playerCurrency;}
    public void setCurrency(int newCurrency){ 
        playerCurrency = newCurrency;
        event_CurrancyChange.Invoke(); 
        }

    public void addCurrency(int addedCurrency){ 
        playerCurrency += addedCurrency;
        event_CurrancyChange.Invoke();   
        }

    public bool subCurrency(int subractedCurrency){ //Subracts if money exists, also returns a false
        if (subractedCurrency > playerCurrency){return false;}
        else {
            playerCurrency -= subractedCurrency;
            event_CurrancyChange.Invoke(); 
            return true;
        }
         
    }
}
