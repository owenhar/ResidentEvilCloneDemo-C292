using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy : MonoBehaviour
{

    float shotsFired = 0;
    float shotsHit = 0;
    float accuracy = 0;
    // Start is called before the first frame update
    void Start()
    {
        MyEvents.shotFired.AddListener(UpdateAccuray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateAccuray(bool hit)
    {
        shotsFired++;
        if (hit)
        {
            shotsHit++;
        }
        accuracy = shotsHit / shotsFired;
        Debug.Log("Accuracy: " + accuracy);
    }

    public float getAccuracy()
    {
        return accuracy;
    }
}
