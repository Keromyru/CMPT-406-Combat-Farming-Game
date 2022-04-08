using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
	
	public void EndTheGame() {
		
		// Reset the cursor back to default
		myCursor.setDefault();
		
		playTransition();
		
		// Load the end screen
        SceneManager.LoadScene( "EndScreen", LoadSceneMode.Single );
		
	}
	
	IEnumerator playTransition() {
		
		// Play animation and wait 1 second
		this.GetComponent<Animator>().Play( "anim_TransitionEnter" );
		
		yield return new WaitForSeconds( 1f );
		
	}	
	
    
}
