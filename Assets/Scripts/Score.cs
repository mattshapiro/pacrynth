using UnityEngine.UI;
using System.Collections;

public class Score : Text {

	int score, dot_max, dot_count;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		score = 0;
		Display ();
	}
	
	public void AddScore(int add) {
		score += add;
		Display ();
	}

	public void SetScore(int score) {
		this.score = score;
		Display ();
	}

	public int GetScore() {
		return score;
	}

	void Display () {
		text = "" + score;
	}
}
