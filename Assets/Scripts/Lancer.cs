using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lancer : MonoBehaviour {

    public Camera cam;
    public AudioSource audioStart;
    public AudioSource audioLancer;
    private Rigidbody rb;
    private Ray ray;
    private RaycastHit hit;
    private float force = 120f;
    private float ratioSpeed;
    private float ratioAngle;
    private float angle;
    public static bool checkSpace;
    public static bool checkMouse;
    public static bool startValid;
    private Vector3 mousePositionStart;
    private Vector3 mousePositionEnd;
    public static bool bouleLancer;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !checkSpace)
        //{
        //    rb.AddForce(new Vector3(0f, 0f, force));
        //    checkSpace = false; //mettre true
        //    bouleLancer = true;
        //    audioStart.Stop();
        //    audioLancer.Play();
        //}
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left mouse apply
        {
            mousePositionStart = Input.mousePosition;
            ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(mousePositionStart));
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name.Equals("BB8:BB8_Body"))
            {
                Debug.Log(hit.collider.gameObject.name);
                startValid = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && !checkMouse && startValid) // Left mouse release
        {
            mousePositionEnd = Input.mousePosition;
            if (mousePositionEnd.y > mousePositionStart.y)
            {
                ratioSpeed = (mousePositionEnd.y / mousePositionStart.y);
                ratioAngle = (mousePositionEnd.x / mousePositionStart.x);
                if (ratioSpeed >= 10)
                {
                    ratioSpeed = 10;
                }
                force = force * ratioSpeed;
                angle = rb.position.x + ratioAngle;
                if (ratioAngle > 1)
                {
                    angle = angle + 5;
                    if(ratioAngle < 1.1f)
                    {
                        rb.AddForce(new Vector3(0f, 0f, force));
                    }else
                    {
                        rb.AddForce(new Vector3(angle, 0f, force));
                    }
                }
                else if(ratioAngle < 1)
                {
                    angle = angle + 6;
                    if (ratioAngle > 0.9f)
                    {
                        rb.AddForce(new Vector3(0f, 0f, force));
                    }
                    else
                    {
                        rb.AddForce(new Vector3(-angle, 0f, force));
                    }
                }
                else
                {
                    rb.AddForce(new Vector3(0f, 0f, force));
                }
                checkMouse = true;
                bouleLancer = true;
                force = 120f;
                audioStart.Stop();
                audioLancer.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Bowling");
            bouleLancer = false;
            checkMouse = false;
            startValid = false;
            checkSpace = false;
            Quille.points = 0;
            Quille.nbQuilles = 10;
            Quille.bouleRigole = false;
            Quille.reinitialisation = false;
            Quille.nbQuillesTombe = 0;
            Horloge.resetTime();
        }
    }
}
