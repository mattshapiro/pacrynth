using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : Text {

	static int lives = 3;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		text = "" + lives;
	}
	
	public void Die () {
		lives--;
		if (lives > 0) {
			text = "" + lives;
		} else {
			text = "Tap the screen to restart.";
		}
	}

	public int GetScore() {
		return lives;
	}
}
