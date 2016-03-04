using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : Text {

	static int lives = 3;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		CalcText();
	}

	void CalcText () {
		text = "";
		for (int i = 0; i < lives; i++) {
			text += "●";
		}
	}
	
	public void Die () {
		lives--;
		if (lives >= 0) {
			CalcText();
		} else {
			text = "Game over.  Tap to continue...";
		}
	}

	public int GetLives() {
		return lives;
	}

	public void ResetLives () {
		lives = 3;
	}
}
