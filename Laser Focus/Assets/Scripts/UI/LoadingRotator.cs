using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotator : MonoBehaviour
{

    public RectTransform icon;
    public float timeStep;
    public float oneStepAngle;

    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= timeStep)
        {
            Vector3 iconAngle = icon.localEulerAngles;
            iconAngle.z += oneStepAngle;

            icon.localEulerAngles = iconAngle;
            startTime = Time.time;
        }   
    }
}
