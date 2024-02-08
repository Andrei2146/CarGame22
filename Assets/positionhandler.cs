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
        //f� alla carlapcounter i scenen
        carlapcounter[] carLapCounterArray = FindObjectsOfType<carlapcounter>();
        //Store alla carlapcounters i en list
        carlapcounters = carLapCounterArray.ToList<carlapcounter>();

        foreach (carlapcounter lapCounters in carlapcounters)
        
            lapCounters.OnPassCheckpoint += OnPassCheckPoint;
    }

    void OnPassCheckPoint (carlapcounter carlapcounter)
    {
        //Sortera bilens position baserad p� hur m�nga checkpoints dom har passerat, mer �r b�ttre. efter sortera on time d�r kortare tid �r b�ttre
        carlapcounters = carlapcounters.OrderByDescending(s => s.GetNumberOfCheckpointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();
        //F� bilens position
        int carPosition = carlapcounters.IndexOf(carlapcounter) + 1;
        //S�ger till lap counter vart bilen �r
        carlapcounter.SetCarPosition(carPosition);
    }

}
