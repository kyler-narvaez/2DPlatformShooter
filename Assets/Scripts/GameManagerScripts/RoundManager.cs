using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int round;

    //[SerializeField] private GameObject skillTree;
    private AudioManager audioManager;
    [SerializeField] private TextIndicators roundIndicator;
    [SerializeField] private StageManager stageManager;

    void Awake()
    {
        round = 1;
        roundIndicator.SetTextValue("Round: ", round);
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void NextRound()
    {
        audioManager.Play("RoundOverSound");

        round++;
        roundIndicator.SetTextValue("Round: ", round);
        
        stageManager.DetermineStageChange(round);
    }

    public int GetRound()
    {
        return round;
    }


}
