using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<carlapcounter> carlapcounters = new List<carlapcounter>();
    // Start is called before the first frame update
    void Start()
    {
        //få alla carlapcounter i scenen
        carlapcounter[] carLapCounterArray = FindObjectsOfType<carlapcounter>();
        //Store alla carlapcounters i en list
        carlapcounters = carLapCounterArray.ToList<carlapcounter>();

        foreach (carlapcounter lapCounters in carlapcounters)
        
            lapCounters.OnPassCheckpoint += OnPassCheckPoint;
    }

    void OnPassCheckPoint (carlapcounter carlapcounter)
    {
        //Sortera bilens position baserad på hur många checkpoints dom har passerat, mer är bättre. efter sortera on time där kortare tid är bättre
        carlapcounters = carlapcounters.OrderByDescending(s => s.GetNumberOfCheckpointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();
        //Få bilens position
        int carPosition = carlapcounters.IndexOf(carlapcounter) + 1;
        //Säger till lap counter vart bilen är
        carlapcounter.SetCarPosition(carPosition);
    }

}
