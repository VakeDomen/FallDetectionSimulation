using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color initialColor;
    public int i;
    public int j;
    public int id;
    private float maxIntensity = 0.0f;
    private int lastForceIntensity = 0;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        initialColor = objectRenderer.material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Calculate total impulse due to this collision
        Vector3 totalImpulse = collision.impulse;
    
        // Project this onto the 'down' direction (which is -transform.up in Unity's coordinate system)
        Vector3 downwardImpulse = Vector3.Project(totalImpulse, -transform.up);
    
        // To get force from impulse you need to divide by time. 
        // However, as Unity applies physics in fixed time steps, this division would give you average force during that time step.
        Vector3 downwardForce = downwardImpulse / Time.fixedDeltaTime;
    
        // Now print the downwardForce to the console
        if (downwardForce.y != 0.0f) {
            Debug.Log("Downward force: " + downwardForce);
        }


        // COLOR THE TILE

        // calculate the intensity of the collision
        float intensity = collision.relativeVelocity.magnitude;

        // normalize intensity to a range between 0 and 1 for use with Color.Lerp
        intensity = Mathf.Clamp01(intensity / 65f); // we assume the max intensity is 10
        
        float forceInKg = downwardForce.y / 9.81f; // Convert force to mass in kg
        lastForceIntensity = (int) Mathf.Lerp(0, 65537, forceInKg / 65); // Map mass range to 0-65537
        
        if (intensity > maxIntensity)
        {
            // lerp from white to red based on the intensity of the collision
            Color collisionColor = Color.Lerp(Color.white, Color.red, intensity);

            // change the color of the object
            objectRenderer.material.color = collisionColor;

            maxIntensity = intensity;
        }

    }



    // This function gets called when the object stops colliding with something
    void OnCollisionExit(Collision collision)
    {
        // revert back to the initial color
        objectRenderer.material.color = initialColor;
    }

    public int GetSensorData() {
        return lastForceIntensity;
    }

    public void SetIndices(int i, int j, int id)
    {
        this.i = i;
        this.j = j;
        this.id = id;
    }
}
