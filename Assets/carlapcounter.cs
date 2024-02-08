using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class carlapcounter : MonoBehaviour
{
    //S�ger vilken bil �r f�rst, mera checkpoints i lite tid = f�rst
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
            //Ser om man gick checkpoint i r�tt ordning, r�tta checkpoint m�ste ha en nummer som �r 1 h�gre �n f�rra
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
