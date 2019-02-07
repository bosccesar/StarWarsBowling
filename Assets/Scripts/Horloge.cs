using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horloge : MonoBehaviour {

    public static float tempsOrigin = 10f;
    public static int tempsInt;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        tempsInt = Mathf.RoundToInt(tempsOrigin);
		if(Lancer.bouleLancer)
        {
            tempsOrigin -= Time.deltaTime;
        }
        Debug.Log(tempsOrigin);
	}

    public static void resetTime()
    {
        tempsOrigin = 10f;
    }
}
