using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatBalloon: MonoBehaviour {

    private Vector3 balloonPosition;        // holds balloon position
    public float sineScalar;                // used to multiply the wavy movement by a random number
    private float destroyHeight = 50f;      // height at which balloon destroys
    public bool moveSine;                  // if true balloon moves at sine wave, if false moves at cosine wave
    public float uniqueBalloonRiseRate;

    private void Start()
    {
        uniqueBalloonRiseRate = BalloonGame.balloonRiseRate;
        sineScalar = Random.Range(1, 4);
        moveSine = (Random.value > 0.5);
    }
    
    void Update()
    {
        moveBalloon();      // moves balloon every frame
        autoDestroy();      // checks if the balloon needs to be destroyed
       
    }

    // Moves balloon in a wavy pattern at a certain rate
    private void moveBalloon()
    {
        transform.Translate(Vector3.up * Time.deltaTime * BalloonGame.balloonRiseRate); // moves balloon upwards at a rate

        // wave movement pattern
        if (moveSine) { 
            // moves balloon in a wavy pattern
            balloonPosition = transform.position;
            balloonPosition.z += Mathf.Sin(Time.time) * Time.deltaTime * sineScalar;
            transform.position = balloonPosition;
        }
        else
        {
            // moves balloon in a wavy pattern
            balloonPosition = transform.position;
            balloonPosition.z += Mathf.Cos(Time.time) * Time.deltaTime * sineScalar;
            transform.position = balloonPosition;
        }
    }

    // Destroys balloon if its height > destroyHeight (defined above)
    private void autoDestroy()
    {
        if (transform.position.y > destroyHeight)
        {
            Destroy(this.gameObject);
        }
    }

}
