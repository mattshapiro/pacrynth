using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	public Text output;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		output.text = (Input.gyro.enabled) ? "GYRO" : "ACCEL";
		output.text += " X: " + rb.transform.rotation.x.ToString("F3") + " \n" +
			"Y: " + rb.transform.rotation.y.ToString("F3") + " \n" + 
				"Z: " + rb.transform.rotation.z.ToString("F3");
		output.fontSize = 20;
	}
}
