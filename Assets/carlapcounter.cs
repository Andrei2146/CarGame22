using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class carlapcounter : MonoBehaviour
{
    //Säger vilken bil är först, mera checkpoints i lite tid = först
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckpoint = 0;
    int lapsCompleted = 0;
    const int lapsToComplete = 2;
    bool isRaceCompleted;
    int carPosition = 0;

    int NumberOfPassedCheckPoints = 0;
    public event Action<carlapcounter> OnPassCheckpoint;
    public void SetCarPosition(int position)
    {
        carPosition = position;

    }

    public int GetNumberOfCheckpointsPassed ()
    {
        return NumberOfPassedCheckPoints;
    }

    public float GetTimeAtLastCheckPoint ()
    {
        return timeAtLastPassedCheckpoint;
    }



    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("checkpoint"))
        {
            if (isRaceCompleted) 
                return;
            checkpoint checkpoint = collider2D.GetComponent<checkpoint>();
            //Ser om man gick checkpoint i rätt ordning, rätta checkpoint måste ha en nummer som är 1 högre än förra
            if (passedCheckPointNumber + 1 == checkpoint.checkPointNumber)
            {
                passedCheckPointNumber = checkpoint.checkPointNumber;

                NumberOfPassedCheckPoints++;

                //Storear tiden av checkpointen
                timeAtLastPassedCheckpoint = Time.deltaTime;

                if (checkpoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                        isRaceCompleted = true;
                }
                //Invokerar passerade checkpoint event
                OnPassCheckpoint?.Invoke(this); 
            }
        }
    }
}
