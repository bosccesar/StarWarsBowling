using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quille : MonoBehaviour {

    public Text timerText;
    public Text timerPoints;
    public GameObject groupQuilles;
    public GameObject particleExplosion;
    public GameObject particlePlasma;
    public GameObject boule;
    public GameObject panel;
    public AudioSource audioExplosion;
    public AudioSource audioLancer;
    public AudioSource audioStart;
    public AudioSource audioEnd;
    private float timeLeft;
    private Rigidbody rbBoule;
    public static int nbQuilles;
    public static int points;
    private int essais;
    public static int nbQuillesTombe;
    public static bool bouleRigole;
    public static bool reinitialisation;
    private bool resultatTrue;
    private bool essaisIncremente;
    private bool strike;
    private bool spare;
    private GameObject[] tabQuille;
    private Vector3 positionOriginQuilles;
    private Quaternion rotationOriginQuilles;
    private Vector3 positionOriginBoule;
    private Quaternion rotationOriginBoule;

    // Use this for initialization
    void Start () {
        panel.SetActive(false);
        nbQuilles = 10;
        essais = 0;
        timerText.text = "Start : Drag & Drop entre la boule et l'endroit souhaité pour lancer la boule" + "\n" + "Vous avez 2 essais pour faire tomber toutes les quilles";
        timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + essais;
        positionOriginQuilles = groupQuilles.transform.position;
        rotationOriginQuilles = groupQuilles.transform.rotation;
        positionOriginBoule = boule.transform.position;
        rotationOriginBoule = boule.transform.rotation;
        rbBoule = boule.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeLeft = Horloge.tempsInt;
        if (Lancer.bouleLancer && !essaisIncremente)
        {
            timerText.text = "";
            timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
            essaisIncremente = true;
        }
        else if (!Lancer.bouleLancer && essaisIncremente)
        {
            timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + essais;
        }
        if (Lancer.bouleLancer)
        {
            Debug.Log("Nombre de quilles : " + nbQuilles);
            Resultat();
        }
        if (Lancer.bouleLancer && timeLeft == -3f)
        {
            if(!strike || !spare)
            {
                tabQuille = GameObject.FindGameObjectsWithTag("Quilles");
                foreach (GameObject quille in tabQuille)
                {
                    QuilleUnique quilleUnique = quille.GetComponent<QuilleUnique>();
                    if (quilleUnique.quilleTombe && timeLeft == -3f)
                    {
                        Destroy(quille);
                    }
                }
            }
            Reinitialisation();
            if (essais == 2 || strike)
            {
                essais = 0;
                strike = false;
                panel.SetActive(true);
                audioStart.Stop();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Boule" && nbQuilles == 10)
        {
            bouleRigole = true;
        }
    }
    
    void Resultat()
    {
        if (!resultatTrue)
        {
            if (nbQuilles == 0)
            {
                if (essais == 0)
                {
                    points = points + 30;
                    timerText.text = ("STRIKE !!!!! " + "\n" + "En attente du tableau des scores champion ! :)");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                    Instantiate(particleExplosion, new Vector3(groupQuilles.transform.position.x, groupQuilles.transform.position.y, groupQuilles.transform.position.z + 9.5f), Quaternion.identity);
                    strike = true;
                    audioExplosion.Play();
                }
                else if(essais == 1)
                {
                    points = points + (nbQuillesTombe * 2);
                    timerText.text = ("Spare !!!" + "\n" + "Bravo ! Vous avez fait tomber toutes les quilles." + "\n" + "En attente du tableau des scores, Retentez un strike pour faire un carton plein");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                    Instantiate(particlePlasma, new Vector3(groupQuilles.transform.position.x, groupQuilles.transform.position.y, groupQuilles.transform.position.z + 9.5f), Quaternion.identity);
                    spare = true;
                    audioExplosion.Play();
                }
                resultatTrue = true;
                essais++;
                tabQuille = GameObject.FindGameObjectsWithTag("Quilles");
                foreach (GameObject quille in tabQuille)
                {
                    QuilleUnique quilleUnique = quille.GetComponent<QuilleUnique>();
                    if (quilleUnique.quilleTombe)
                    {
                        Destroy(quille);
                    }
                }
                audioLancer.Stop();
                audioEnd.Play();
            }
            else if (timeLeft == 0 && nbQuilles < 10)
            {
                if (essais == 0)
                {
                    points = points + (10 - nbQuilles);
                    timerText.text = ("Bravo ! Vous avez fait tomber : " + (10 - nbQuilles) + " quilles." + "\n" + "Relancez la boule, vous pouvez faire un spare rien n'est perdu !");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                }
                else if (essais == 1)
                {
                    points = points + nbQuillesTombe;
                    timerText.text = ("Oh non vous avez raté le spare..." + "\n" + "Il vous restait " + nbQuilles + " quilles." + "\n" + "En attente du tableau des scores");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                }
                resultatTrue = true;
                essais++;
                audioLancer.Stop();
                audioEnd.Play();
            }
            else if (timeLeft == 0 && nbQuilles == 10)
            {
                if(essais == 0)
                {
                    timerText.text = ("Oh non vous avez mis trop de temps pour toucher les quilles." + "\n" + "Ne perdez pas espoir, relancez la boule, vous pouvez faire un spare rien n'est perdu !");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                }
                else if (essais == 1)
                {
                    timerText.text = ("Oh non vous avez mis trop de temps pour toucher les quilles." + "\n" + "En attente du tableau des scores");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                }
                resultatTrue = true;
                essais++;
                audioLancer.Stop();
                audioEnd.Play();
            }
            else if (bouleRigole && nbQuilles == 10)
            {
                if (essais == 0)
                {
                    timerText.text = ("Oh non vous avez raté les quilles." + "\n" + "Ne perdez pas espoir, relancez la boule, vous pouvez faire un spare rien n'est perdu !");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                }
                else if (essais == 1)
                {
                    timerText.text = ("Oh non vous avez raté les quilles." + "\n" + "En attente du tableau des scores");
                    timerPoints.text = "Points : " + points + "\n" + "Nb d'essais : " + (essais + 1);
                }
                bouleRigole = false;
                resultatTrue = true;
                essais++;
                audioLancer.Stop();
                audioEnd.Play();
            }
        }
    }

    void Reinitialisation()
    {
        reinitialisation = true;
        Horloge.resetTime();
        groupQuilles.transform.position = positionOriginQuilles;
        groupQuilles.transform.rotation = rotationOriginQuilles;
        boule.transform.position = positionOriginBoule;
        boule.transform.rotation = rotationOriginBoule;
        rbBoule.velocity = new Vector3(0f, 0f, 0f);
        rbBoule.angularVelocity = new Vector3(0f, 0f, 0f);

        timerText.text = "";
        bouleRigole = false;
        resultatTrue = false;
        essaisIncremente = false;
        Lancer.bouleLancer = false;
        Lancer.checkSpace = false;
        Lancer.checkMouse = false;
        Lancer.startValid = false;
        nbQuillesTombe = 0;
        audioEnd.Stop();
        audioStart.Play();
    }
}
