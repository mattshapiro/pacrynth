using UnityEngine;
using System.Collections;
// awful coding
public class VRControler : MonoBehaviour {

    GameObject zboard = null, xboard = null;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(zboard != null)
        {
            Vector3 originRot = zboard.transform.rotation.eulerAngles;
            zboard.transform.rotation = Quaternion.Euler(new Vector3(
                originRot.x,
                originRot.y, 
                this.transform.rotation.eulerAngles.z));
        }
        if (xboard != null)
        {
            Vector3 originRot = xboard.transform.rotation.eulerAngles;
            xboard.transform.rotation = Quaternion.Euler(new Vector3(
                this.transform.rotation.eulerAngles.z, 
                originRot.y,
                originRot.z));
        }
        if(controller != null)
        {
            if(controller.GetPressDown(triggerButton))
            {
                Exit();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Handle1")
        {
            if (xboard) return; // already initialized
            // z on z axis
            //Debug.Log("tag = " + collider.tag);
            if(zboard == null)
            {
                //Debug.Log("zboard is null");
                zboard = GameObject.FindGameObjectWithTag("Board");
            }
        }
        else if(collider.tag == "Handle2")
        {
            if (zboard) return; // already intiialized
            // x on z axis
            //Debug.Log("tag = " + collider.tag);
            if (xboard == null)
            {
                //Debug.Log("xboard is null");
                xboard = GameObject.FindGameObjectWithTag("Board");
            }
        }
    }

    void Exit()
    {
        if (zboard)
        {
            Debug.Log("Exit Handle 1");
            Vector3 originRot = zboard.transform.rotation.eulerAngles;
            zboard.transform.rotation = Quaternion.Euler(new Vector3(originRot.x, originRot.y, 0));
            zboard = null;
        }
        if (xboard)
        {
            Debug.Log("Exit Handle 2");
            Vector3 originRot = xboard.transform.rotation.eulerAngles;
            xboard.transform.rotation = Quaternion.Euler(new Vector3(0, originRot.y, originRot.z));
            xboard = null;
        }
    }
}
