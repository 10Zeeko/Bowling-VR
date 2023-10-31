using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckResult : MonoBehaviour
{
    private Vector3 respawnPos = Vector3.zero;
    private int ballThrows = 0;
    private GameObject[] allPins;
    private int score = 0;
    private int strikeCount = 0;
    private int roundNumber = 1;

    [SerializeField]
    private TextMeshProUGUI round;
    [SerializeField]
    private TextMeshProUGUI scoreUIOne;
    [SerializeField]
    private TextMeshProUGUI scoreUITwo;
    [SerializeField]
    private TextMeshProUGUI scoreUITotal;

    private void Start()
    {
        allPins = GameObject.FindGameObjectsWithTag("Pin");
        round.text = "Round: " + roundNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballThrows++;
            checkScore();
            other.gameObject.GetComponent<PinScript>().restartPin();
            restartGameZone(false);
        }
        else if (!other.CompareTag("Pin"))
        {
            other.transform.position = respawnPos;
        }
    }

    private void checkScore()
    {
        int fallenPins = 0;
        for (int i = 0; i < allPins.Length; i++)
        {
            if (allPins[i].GetComponent<PinScript>().isGrounded())
            {
                allPins[i].SetActive(false);
                fallenPins++;
            }
        }

        if (fallenPins == 10) // Strike
        {
            strikeCount++;
            if (strikeCount == 1) // Single Strike
            {
                score += 10;
            }
            else if (strikeCount == 2) // Double
            {
                score += 20;
            }
            else // Turkey or more
            {
                score += 30;
            }
            restartGameZone(true);
        }
        else if (ballThrows == 2) // Spare or normal throw
        {
            if (fallenPins == 10) // Spare
            {
                score += 10 + fallenPins;
            }
            else // Normal throw
            {
                score += fallenPins;
            }
            strikeCount = 0; // Reset strike count after second throw
        }

        // Update UI
        scoreUIOne.text = ((ballThrows == 1) ? fallenPins.ToString() : "0");
        scoreUITwo.text = ((ballThrows == 2) ? fallenPins.ToString() : "0");
        scoreUITotal.text = score.ToString();
    }

    private void restartGameZone(bool strike)
    {
        
        if (ballThrows >= 2 || strike)
        {
            for (int i = 0; i < allPins.Length; i++)
            {
                allPins[i].GetComponent<PinScript>().restartPin();
            }
            ballThrows = 0;
            roundNumber++;
            round.text = "Round: " + roundNumber;
            if (roundNumber > 10)
            {
                Application.Quit();
            }
        }
    }
}