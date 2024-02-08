using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carsscripts : MonoBehaviour
{
    [Header("CarSettings")]
    public float driftFactor = 0.5f;
    public float accelerationfactor = 30.0f;
    public float turnfactor = 3.5f;
    public float maxspeed = 20;


    float accelerationinput = 0;
    float steeringinput = 0;

    float rotationangle = 0;

    float velocityVsup = 0;
    
    Rigidbody2D carRigidbody2D;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }
    void ApplyEngineForce()
    {
        velocityVsup = Vector2.Dot(transform.up, carRigidbody2D.velocity);
        if (velocityVsup > maxspeed && accelerationinput > 0)
            return;
        if (velocityVsup < -maxspeed * 0.5f && accelerationinput < 0) 
            return;
        if (carRigidbody2D.velocity.sqrMagnitude > maxspeed * maxspeed && accelerationinput > 0) 
            return;
        if (accelerationinput == 0)
      
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
            else carRigidbody2D.drag = 0;
        

        //Gör styrka for motoren
        Vector2 engineforceVector = transform.up * accelerationinput * accelerationfactor;

        //Sätt styrka och flytta bilen framåt
        carRigidbody2D.AddForce(engineforceVector, ForceMode2D.Force);
    }
    void ApplySteering ()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        //Byta rotation angle baserat på input
        rotationangle -= steeringinput * turnfactor * minSpeedBeforeAllowTurningFactor;

        //Gör steering för bilen med att man rotatear bil objekten
        carRigidbody2D.MoveRotation(rotationangle);
    }
    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetlateralVelocity()
    {
        //Säger hur snabbt bilen går åt sidan
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

 
    public void SetInputVector(Vector2 inputVector)
    {
        steeringinput = inputVector.x;
        accelerationinput = inputVector.y;

    }
   


}

