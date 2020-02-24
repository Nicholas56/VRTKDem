using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MakePancakes : VRTK_InteractableObject
{
    public Transform spawnPoint;
    public int numOfPancakes;
    float spawnDelay = 0.5f;
    float timer;

    bool makePancakes;
    public GameObject pancake;

    protected VRTK_ControllerEvents controllerEvents;


    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        controllerEvents = currentUsingObject.GetComponent<VRTK_ControllerEvents>();
    }
    public override void StopUsing(VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);
        controllerEvents = null;
    }

    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (controllerEvents)
        {
            float power = controllerEvents.GetTriggerAxis();
            if (Time.time > timer)
            {
                TurnOnMachine();
                timer = Time.time + spawnDelay;
            }
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerEvents.gameObject), power * 0.25f, 0.1f, 0.01f);
        }
        else
        {
        }
    }

    void TurnOnMachine()
    {
        Instantiate(pancake, spawnPoint.position, Quaternion.identity);
    }
}
