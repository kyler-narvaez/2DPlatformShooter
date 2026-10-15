using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
    //[SerializeField] private RoundManager roundManager;

    [SerializeField] private int initialSpawnRound;
    [SerializeField] private int initialSpawnAmount;
    [SerializeField] private int spawnPerRound;
    [SerializeField] private int amountPerIteration;

    private bool isInitialSpawn = false;

    //[SerializeField] private static int totalSpawnCount = 0;

    private void Start()
    {
        //
    }


    public int GetSpawnCount(int round)
    {
        if (round == initialSpawnRound)
        {
            isInitialSpawn = true;
            return (-1)*initialSpawnAmount; //FIXME CHANGE TO -1
        }
        else if (round >= (initialSpawnRound + spawnPerRound) && (round - initialSpawnRound) % (spawnPerRound) == 0)
        {
            isInitialSpawn = false;
            return amountPerIteration;
        }

        return 0;
    }

    public bool GetIsInitialSpawn()
    {
        return isInitialSpawn;
    }


}
