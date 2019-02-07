using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {

    public Camera cameraMain;
    public Camera cameraArrivee;

    // Use this for initialization
    void Start () {
        cameraArrivee.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Quille.reinitialisation)
        {
            cameraMain.enabled = true;
            cameraArrivee.enabled = false;
            Quille.reinitialisation = false;
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Boule")
        {
            cameraMain.enabled = false;
            cameraArrivee.enabled = true;
        }
    }
}
