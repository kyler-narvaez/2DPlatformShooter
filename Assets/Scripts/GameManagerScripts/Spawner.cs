using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] ParticleSystem spawnParticle;
    [SerializeField] private GameObject skillTree;

    public int[] enemySpawnNums;

    private RoundManager roundManager;
    private Player player;

    public List<GameObject> spawnList = new();  //FIXME dont make public
    
    private bool isRoundOver;
    private int killsNeeded;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        roundManager = GetComponent<RoundManager>();
        SetListOfEnemies();
        BeginSpawning();
    }

    public void BeginSpawning()
    {
        isRoundOver = false;
        //spawnList = new();
        DetermineSpawnList();
        StartCoroutine(StartRound());
    }

    private void SetListOfEnemies()
    {
        enemySpawnNums = new int[enemyPrefabs.Count];
        for (int i = 0; i < enemySpawnNums.Length; ++i)
        {
            enemySpawnNums[i] = 0;
        }

        //enemyPrefabs = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "Enemy").ToList();
        
    }

    private void DetermineSpawnList()
    {
        bool isIntroNewChar = false;
        if (roundManager.GetRound() != 1 && enemySpawnNums[0] == 0)
        {
            enemySpawnNums[0] = roundManager.GetRound() / 2 - 1;
        }

        for (int i = enemyPrefabs.Count - 1; i >= 0; --i)
        {
            enemySpawnNums[i] += enemyPrefabs[i].GetComponent<EnemySpawnBehaviour>().GetSpawnCount(roundManager.GetRound());
            
            //if enemy is being introduced : Make it the only thing in list (reset list to 0)
            
            if (enemySpawnNums[i] < 0)  //Negative means original round
            {
                ResetSpawns(i);
                enemySpawnNums[i] *= -1; //Changes to positive number
                isIntroNewChar = true;
            }

            for (int j = 0; j < enemySpawnNums[i]; ++j)
            {
                spawnList.Add(enemyPrefabs[i]);
            }
            if (isIntroNewChar)
            {
                break;
            }
        }

        killsNeeded = spawnList.Count;

        //Randomize list
        int count = spawnList.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int newPosition = Random.Range(i, count);
            GameObject tmp = spawnList[i];
            spawnList[i] = spawnList[newPosition];
            spawnList[newPosition] = tmp;
        }
    }
    //Resets the number of spawns to 0 (other than the new enemy), after a new enemy is introduced
    private void ResetSpawns(int specialIndex)
    {
        for (int i = 0; i < enemySpawnNums.Length; ++i)
        {
            if (i != specialIndex)
            {
                enemySpawnNums[i] = 0;
            }
        }
    }

    IEnumerator StartRound()
    {
        for (int i = 0; i < spawnList.Count; ++i)
        {
            Vector3 position = new Vector3(Random.Range(-16f, 16f), Random.Range(-8f, 5f), 0f);
            GameObject enemy = spawnList[i];

            ParticleSystem tempEffect = Instantiate(spawnParticle, position, new Quaternion(0f, 0f, 0f, 0f));
            ParticleSystem.MainModule main = tempEffect.main;
            main.startColor = enemy.GetComponent<SpriteRenderer>().color;

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));    
            Destroy(tempEffect);
            Instantiate(enemy, position, enemy.transform.rotation);
        }

        yield return new WaitUntil(() => isRoundOver);
        spawnList = new();
        player.ResetKillsSinceRoundStarted();

        player.GetComponent<PlayerSkillTree>().SetValues();
        skillTree.SetActive(true);
        yield return new WaitUntil(() => skillTree.activeSelf == false);
        roundManager.NextRound();


        BeginSpawning();
    }


    private void Update()
    {
        if (killsNeeded == player.GetKillsSinceRoundStarted())
        {
            isRoundOver = true;
        }
    }
}
