using UnityEngine;

//there is no reason we need this script. i just wanted to do it
public class ExcessiveMenuVisual : MonoBehaviour
{
    //animation constants
    float rotationLimit = 15; //degrees in either direction
    int rotationPeriod = 48; //frames per cycle (50fps with FixedUpdate)

    float scaleMax = 1.2f;
    float scaleMin = 0.8f;
    int scalePeriod = 25; //for best results, have it not line up with rotationPeriod

    int tick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tick = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //count
        tick++;
        //calculate
        float currentRotation = Mathf.Sin(tick * 2 * Mathf.PI / rotationPeriod) * rotationLimit;
        float currentScale = (Mathf.Sin(tick * 2 * Mathf.PI / scalePeriod) / 2 + 0.5f) * (scaleMax - scaleMin) + scaleMin;
        //display
        transform.localRotation = Quaternion.Euler(new Vector3(0, currentRotation, 0));
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}
