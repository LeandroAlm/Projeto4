using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBuild : MonoBehaviour {


    public float stepDuration;

    private float timer;

    private int currentBuildStep;

    private int stepCount;


    void Start()
    {
        timer = stepDuration;
        stepCount = transform.childCount;
        currentBuildStep = 0;
    }

    void Update()
    {
        if (timer <= 0 && currentBuildStep < stepCount)
        {
            timer = stepDuration;
            NextBuildStep();
        }

        timer -= Time.deltaTime;
    }

    void NextBuildStep()
    {
        transform.GetChild(currentBuildStep).gameObject.SetActive(true);
        currentBuildStep += 1;
    }
}
