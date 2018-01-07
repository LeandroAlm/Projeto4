using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBuild : MonoBehaviour
{
    public float stepDuration;

    private float timer;

    private int currentBuildStep;

    private int stepCount;

    Vector3 prefabSize;

    int prefabSize2;

    Grid grid;

    public GameObject pathFindingObj;

    void Start()
    {
        timer = stepDuration;
        stepCount = transform.childCount;
        currentBuildStep = 0;

        pathFindingObj = GameObject.FindGameObjectWithTag("A");

        grid = pathFindingObj.gameObject.GetComponent<Grid>();
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
        Node node = grid.NodeFromWorldPoint(this.transform.GetChild(currentBuildStep).transform.position);

        node.walkable = false;
        currentBuildStep += 1;
    }

}
