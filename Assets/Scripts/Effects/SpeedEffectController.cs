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

    [SerializeField] private float[] speedTable = new float[8];
    private int speedIndex = 0;
    public int SpeedIndex
    {
        get { return speedIndex; }
        private set {; }
    }
    private float currentSpeed = 0f;

    private void Start()
    {
        ConfirmSpeed();
    }
    private void Update()
    {
        var leftMain = leftEffect.main;
        var rightMain = rightEffect.main;
        leftMain.startSpeed = currentSpeed;
        rightMain.startSpeed = currentSpeed;

        var leftEmission = leftEffect.emission;
        var rightEmission = rightEffect.emission;
        leftEmission.rateOverTime = currentSpeed;
        rightEmission.rateOverTime = currentSpeed;

    }

    public void AddSpeed()
    {
        speedIndex++;
        if (speedIndex >= speedTable.Length) speedIndex = speedTable.Length - 1;
        ConfirmSpeed();
    }
    public void SubSpeed()
    {
        speedIndex--;
        if (speedIndex < 0) speedIndex = 0;
        ConfirmSpeed();
    }
    private void ConfirmSpeed()
    {
        st.SpeedPer = speedTable[speedIndex];
        currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedTable[speedIndex]);
        Locator<ScoreManagerTest>.Instance.AddMoveDistance(currentSpeed);
    }
}
