using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAutoBuild : MonoBehaviour
{
    public float stepDuration;

    private float timer;

    private int currentBuildStep;

    private int stepCount;

    public GameObject wallPrefab;

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
        Instantiate(wallPrefab, transform.GetChild(currentBuildStep).position, transform.GetChild(currentBuildStep).rotation);
        transform.GetChild(currentBuildStep).gameObject.SetActive(false);
        currentBuildStep += 1;
    }
}
