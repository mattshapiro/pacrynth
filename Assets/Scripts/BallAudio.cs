using UnityEngine;
using System.Collections;

public class BallAudio : MonoBehaviour {
	public AudioClip loop;
    public AudioSource source;
    public Rigidbody ball;

    bool isRolling;

	// Use this for initialization
	void Start () {
        isRolling = false;
        source.clip = loop;
        source.loop = true;
    }
	
	// Update is called once per frame
	void Update () {
        float mag = Mathf.Min( Mathf.Abs(ball.velocity.x) + Mathf.Abs(ball.velocity.y), 5);
        float vol = mag / 5;
        if (isRolling)
        {
            source.volume = Mathf.Min(vol, 1);
            if(mag < 0.01)
            {
                isRolling = false;
                source.Stop();
            }
        }
        else
        {
            if(mag > 0.01)
            {
                isRolling = true;
                source.volume = Mathf.Min(vol, 1);
                source.Play();
            }
        }
	}
}
