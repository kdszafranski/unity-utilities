using System;
using UnityEngine;

/**
* Generic Timer method that will call a provided method on the provided target GameObject in the given interval of seconds
* @private timer and running are handled thru enabling and disabling this Component on a GameObject
*
* This component can be added dynamically and adjust via script (all fields are public)
*/
public class TSTimer : MonoBehaviour {

    float timer = 0f;    
    bool running = false;

    [Tooltip("A unique name. Useful to differential multiple timers on the same Game Object")]
    public string timerName = "";

    [Tooltip("Run the Trigger Method immediately, then start the timer")]
    public bool runOnEnable = false;

    [Tooltip("Check if this timer only runs one time.")]
    public bool runOneTime = false;

    [Tooltip("Interval in which to fire a method (seconds)")]
    public float timeLimit = 0f;

    [Tooltip("Target GameObject")]
    public GameObject targetGO = null;

    [Tooltip("Name of the Method to invoke on the TargetGO")]
    public string triggerMethod = "";

    [Tooltip("The value to send to the Trigger Method (optional)")]
    public int value = 0;

    void Update() {
        
        if (running && targetGO) {
            timer += Time.deltaTime;
            if (timer >= timeLimit) {
                // do a thing
                targetGO.SendMessage((string)triggerMethod, value);
                timer = 0f;

                if(runOneTime) {
                    // get rid of me
                    Destroy(this);
                }
            }
        }
    }

    private void OnEnable() {
        if(runOnEnable) {
            targetGO.SendMessage((string)triggerMethod, value);
        }

        // now, continue
        timer = 0f;    
        running = true;
    }

    private void OnDisable() {
        running = false;
    }


}