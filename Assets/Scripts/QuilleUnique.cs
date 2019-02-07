using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuilleUnique : MonoBehaviour {

    public bool quilleTombe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "GroupQuilles")
        {
            Quille.nbQuilles--;
            Quille.nbQuillesTombe++;
            quilleTombe = true;
            Debug.Log("Quille en moins");
        }
    }
}
