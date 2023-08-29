using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffectController : MonoBehaviour
{
    [SerializeField] private float minSpeed = 20f;
    [SerializeField] private float maxSpeed = 80f;
    [SerializeField] private ParticleSystem leftEffect = null;
    [SerializeField] private ParticleSystem rightEffect = null;
    [SerializeField] private ScrollTexture st;

    public enum SPEED_STEP
    {
        Slow = 0,
        Middle = 50,
        Fast = 100
    }
    private SPEED_STEP speedStep = SPEED_STEP.Slow;
    public SPEED_STEP SpeedStep 
    { 
        private get { return speedStep; } 
        set 
        { 
            speedStep = value;
            currentSpeedPer = (float)speedStep / 100;
        } 
    }
    private float currentSpeedPer = 0f;
    private float currentSpeed = 0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) SpeedStep = SPEED_STEP.Slow;
        else if (Input.GetKeyDown(KeyCode.Alpha1)) SpeedStep = SPEED_STEP.Middle;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SpeedStep = SPEED_STEP.Fast;
        currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, currentSpeedPer);
        var leftMain = leftEffect.main;
        var rightMain = rightEffect.main;
        leftMain.startSpeed = currentSpeed;
        rightMain.startSpeed = currentSpeed;

        var leftEmission = leftEffect.emission;
        var rightEmission = rightEffect.emission;
        leftEmission.rateOverTime = currentSpeed;
        rightEmission.rateOverTime = currentSpeed;

        st.SpeedPer = currentSpeedPer;
    }
}
