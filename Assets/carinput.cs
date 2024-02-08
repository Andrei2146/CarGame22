using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carinput : MonoBehaviour
{
    carsscripts carsscripts;
    private void Awake()
    {
        carsscripts = GetComponent<carsscripts>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carsscripts.SetInputVector(inputVector);
    }
}
