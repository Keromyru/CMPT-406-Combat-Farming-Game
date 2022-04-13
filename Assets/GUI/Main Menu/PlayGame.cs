using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayGame : MonoBehaviour
{
    public void StartGame () {
        Currency.setMoney(500);
        GameStats.ResetAll();
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
        SceneManager.LoadScene("System", LoadSceneMode.Additive);
    }
}