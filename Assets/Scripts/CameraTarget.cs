using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

    public GameObject bouleTarget;
    private float offset;

    // Use this for initialization
    void Start () {
        offset = transform.position.z - bouleTarget.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vector = new Vector3(transform.position.x, transform.position.y, bouleTarget.transform.position.z + offset);
        transform.position = vector;
	}
}
