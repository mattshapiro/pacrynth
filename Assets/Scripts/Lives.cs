using UnityEngine.UI;
using System.Collections;

public class Lives : Text {

	int lives;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		lives = 3;
		text = "" + lives;
	}
	
	public void Die () {
		lives--;
		if (lives > 0) {
			text = "" + lives;
		} else {
			text = "fuck.";
		}
	}

	public int GetScore() {
		return lives;
	}
}
