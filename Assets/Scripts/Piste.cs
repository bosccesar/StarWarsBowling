using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piste : MonoBehaviour {

    public Rigidbody rigidBoule;

    // Use this for initialization
    void Start () {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Boule")
        {
            rigidBoule.isKinematic = true;
            rigidBoule.isKinematic = false;
            rigidBoule.AddForce(new Vector3(0f, 0f, 30f));
            if (Quille.nbQuilles == 10)
            {
                Quille.bouleRigole = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
