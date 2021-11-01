using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class ShootScript : MonoBehaviour
{
    private InputDevice leftDevice;     // Used to get button presses from L hand
    private InputDevice rightDevice;    // Used to get button presses from L hand
    public GameObject LeftHand;         // Tracks position of L hand
    public GameObject RightHand;        // Tracks position of R hand
    public GameObject smoke;            // smoke effect whenever balloon is destroyed

    public TextMeshProUGUI blueText;
    public TextMeshProUGUI yellowText;
    public TextMeshProUGUI redText;

    public static int[] balloonCount = new int[(int)Color.numColors];  // number of balloons of each color that have been destroyed

    private float reloadTime;
    private bool canShootRight;
    private bool canShootLeft;

    
    enum Color { Red, Blue, Yellow, numColors = 3 };    // array entries in balloonCount are referred to by color (enum)

    [SerializeField]
    private XRNode leftNode = XRNode.LeftHand;
    [SerializeField]
    private XRNode rightNode = XRNode.LeftHand;

    private void Start()
    {
        reloadTime = .2f;
        canShootRight = true;
        canShootLeft = true;

        blueText.text = "x 0";
        yellowText.text = "x 0";
        redText.text = "x 0";
    }

    void GetDevice()
    {
        leftDevice = InputDevices.GetDeviceAtXRNode(leftNode);
        rightDevice = InputDevices.GetDeviceAtXRNode(rightNode);
    }

    public void OnEnable()
    {
        if (!leftDevice.isValid || !rightDevice.isValid)
            GetDevice();
    }

    // if L/R trigger pressed, shoot from L/R hand
    public void Update()
    {
        if (!leftDevice.isValid || !rightDevice.isValid)
            GetDevice();


        // If L/R trigger is pressed, shoots from L/R hand. If balloon was destroyed, initiates reload time.
        bool triggerPressed = false;
        if (leftDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed && canShootLeft)
        {
            if (Shoot(LeftHand))
            {
                canShootLeft = false;
                StartCoroutine(LeftReload());
            }
        }
        if (rightDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed && canShootRight)
        {
            if (Shoot(RightHand))
            {
                canShootRight = false;
                StartCoroutine(RightReload());
            }
        }
    }

    // Shoots a raycast from a controller. If it contacts a balloon, update balloonCount[] and destroy balloon
    public bool Shoot(GameObject shootHand)
    {
        
        RaycastHit hit;

        // if the Raycast hits a balloon, will update balloon count and destroy balloon
        if (Physics.Raycast(shootHand.transform.position, shootHand.transform.forward, out hit))
        {
            if (hit.transform.name == "blue(Clone)" || hit.transform.name == "red(Clone)" || hit.transform.name == "yellow(Clone)")
            {
                UpdateBalloonCount(hit);
                DestroyBalloon(hit);

                if (!hit.transform.tag.Contains(BalloonGame.targetBalloon))
                {
                    BalloonGame.SetGameLost(true);
                    Debug.Log("Game ended due to red or yellow balloon pop");
                }
                return true;
            }
        }

        return false;
    }

    // Turn canshoot to true after waiting to reload
    IEnumerator LeftReload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShootLeft = true;
    }

    IEnumerator RightReload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShootRight = true;
    }

    // updates balloonCount[] based on which balloon was hit
    public void UpdateBalloonCount(RaycastHit balloonHit)
    {
        if (balloonHit.transform.name.Contains("red"))
        {
            balloonCount[(int)Color.Red]++;
        }
        if (balloonHit.transform.name.Contains("blue"))
        {
            balloonCount[(int)Color.Blue]++;
        }
        if (balloonHit.transform.name.Contains("yellow"))
        {
            balloonCount[(int)Color.Yellow]++;
        }

        SetScore(GetBlueCount(), GetYellowCount(), GetRedCount());
    }

    // destroys balloon
    public void DestroyBalloon(RaycastHit balloon)
    {
        Destroy(balloon.transform.gameObject);
        Destroy(Instantiate(smoke, balloon.point, Quaternion.LookRotation(balloon.normal)),0.5f);
    }

    // returns balloonCount[] in a string
    public string BalloonCount
    {
        get
        {
            string s = "Balloon count: ";

            s += "Red " + balloonCount[(int)Color.Red] + " / ";
            s += "Blue " + balloonCount[(int)Color.Blue] + " / ";
            s += "Yellow " + balloonCount[(int)Color.Yellow];

            return s;
        }
    }

    public static int GetRedCount()
    {
        return balloonCount[(int)Color.Red];
    }

    public static int GetBlueCount()
    {
        return balloonCount[(int)Color.Blue];
    }

    public static int GetYellowCount()
    {
        return balloonCount[(int)Color.Yellow];
    }

    public void SetScore(int blue, int yellow, int red)
    {
        blueText.text = " x " + blue;
        yellowText.text = " x " + yellow;
        redText.text = " x " + red;
    }

    public static void ResetCount()
    {
        for (int i = 0; i < balloonCount.Length; i++)
        {
            balloonCount[i] = 0;
        }
    }
}
