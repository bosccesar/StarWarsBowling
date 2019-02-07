using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

    public Text textResultat;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        textResultat.text = "Félicitation vous avez marqué" + "\n" + "                 " + Quille.points + " points";
    }

    public void newGame()
    {
        SceneManager.LoadScene("Bowling");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Bowling");
        Lancer.bouleLancer = false;
        Lancer.checkSpace = false;
        Lancer.checkMouse = false;
        Lancer.startValid = false;
        Quille.points = 0;
        Quille.nbQuilles = 10;
        Quille.bouleRigole = false;
        Quille.reinitialisation = false;
        Quille.nbQuillesTombe = 0;
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
