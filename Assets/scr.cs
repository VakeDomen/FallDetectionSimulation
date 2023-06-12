using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color initialColor;
    public int i;
    public int j;

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
    }



    // This function gets called when the object stops colliding with something
    void OnCollisionExit(Collision collision)
    {
        // revert back to the initial color
        objectRenderer.material.color = initialColor;
    }

    public void SetIndices(int i, int j)
    {
        this.i = i;
        this.j = j;
    }
}
