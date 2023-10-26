using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResult : MonoBehaviour
{
    private Vector3 respawnPos = Vector3.zero;
    private int ballThrows = 0;
    private GameObject[] allPins;

    private void Start()
    { 
        allPins = GameObject.FindGameObjectsWithTag("Pin");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballThrows++;
            other.gameObject.GetComponent<PinScript>().restartPin();
            if (ballThrows >= 2) {
                ballThrows = 0;
                for (int i = 0;i < allPins.Length;i++)
                {
                    allPins[i].GetComponent<PinScript>().restartPin();
                }
            }
        }
        else if (!other.CompareTag("Pin"))
        {
            other.transform.position = respawnPos;
        }
    }
}
