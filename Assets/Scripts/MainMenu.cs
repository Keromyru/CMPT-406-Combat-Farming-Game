using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	[ Header( "Menu Buttons" ) ]
	public Button exitButton;
	
    // Start is called before the first frame update
    void Start() {
		
		myCursor.setDefault();
		exitButton.onClick.AddListener( QuitGame );
        
    }
	
	private void QuitGame() {
		
		Debug.Log( "Closing Game" );
		
		Application.Quit();
		
	}

}
