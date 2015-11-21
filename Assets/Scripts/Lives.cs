using UnityEngine.UI;
using System.Collections;

public class Lives : Text {

	int lives;

	// Use this for initialization
	void Start () {
		lives = 3;
		Display ();
	}
	
	public void Die () {
		lives--;
		Display ();
	}

	public int GetScore() {
		return lives;
	}

	void Display () {
		text = "" + lives;
	}
}
