using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    [SerializeField] private GameObject[] stageList;
    [SerializeField] private int stageIncrement;

    private GameObject currStage;

    private void Start()
    {
        //Makes all stages inactive
        for (int i = 0; i < stageList.Length; ++i)
        {
            stageList[i].SetActive(false);
        }

        currStage = stageList[0];
        currStage.SetActive(true);
    }

    public void DetermineStageChange(int round)
    {
        if (round % stageIncrement == 0)
        {
            PickStageAtRandom();
        }
    }

    private void SetStage(GameObject newStage)
    {
        currStage.SetActive(false); // Disables stage

        currStage = newStage; //Sets new stage

        currStage.SetActive(true);  //Enables new stage

    }

    private void PickStageAtRandom()
    {
        int randomIndex = Random.Range(0, stageList.Length);
        SetStage(stageList[randomIndex]);
    }
}
