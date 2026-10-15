using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private SliderBar healthBar;
    [SerializeField] private SliderBar killbar;
    public int healthPerKill;
    public float slowMoLength;
    public float slowMoStrength;
    public float slowMoRecharge;

    public int killCountReward;

    private CharacterAttributes player;
    private PlayerMovement playerMovement;
    private int killCount;
    private int killsSinceRoundStarted;
    private int exp;
    private PlayerSkillTree playerSkills;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterAttributes>();
        playerSkills = GetComponent<PlayerSkillTree>();
        playerMovement = GetComponent<PlayerMovement>();

        healthBar.SetMaxValue(player.GetHealth());
        healthBar.SetValue(player.GetHealth());
        healthBar.SetTextIndicator(player.GetHealth());

        killbar.SetMaxValue(killCountReward);
        killbar.SetValue(killCount);
        killbar.SetTextIndicator(killCount);

        killCount = 0;
        exp = 0;

        Invoke("Tick", 1f);
    }

    private void SetHealthBar()
    {
        healthBar.SetValue(player.GetHealth());
        healthBar.SetTextIndicator(player.GetHealth());
    }

    private void Update()
    {
        if (healthBar.GetBarValue() != player.GetHealth())
        {
            SetHealthBar();
        }
        SkillChecks();
    }

    private void SkillChecks()
    {
        if (playerSkills.IsSkillUnlocked(PlayerSkillTree.SkillType.healthPerKill_1) && Input.GetKeyDown(KeyCode.Q))
        {
            print("Click");
        }
        if (playerSkills.IsSkillUnlocked(PlayerSkillTree.SkillType.slowMo_1) && slowMoRecharge >= 15f && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(SlowTime());
        }

    }

    IEnumerator SlowTime()
    {
        print(Time.fixedTime + " Activate");
        Time.timeScale = slowMoStrength;
        playerMovement.sloMoEffect = 1 / slowMoStrength;
        yield return new WaitForSeconds(slowMoLength);
        playerMovement.sloMoEffect = 1f;
        Time.timeScale = 1f;
        slowMoRecharge = 0f;
        print("After: " + slowMoRecharge);
    }

    private void Tick()
    {
        print("Tick: " + slowMoRecharge);
        slowMoRecharge += 1f;
        Invoke("Tick", 1f);
    }

    private void OnDestroy()
    {
        SetHealthBar();
    }

    public void OnKill(int enemyHealth)
    {
        player.IncrementHealth((enemyHealth / healthPerKill));  //FIXME change somehow
        exp += enemyHealth; //FIXME change based on location in spawn list
        IncrementKillCount();
    }

    public void IncrementKillCount()
    {
        killCount++;
        killsSinceRoundStarted++;
        killbar.SetValue(killCount);
        killbar.SetTextIndicator(killCount);
        CheckForBonusHealth();
    }

    private void CheckForBonusHealth()
    {
        if (killCount == killCountReward)
        {
            FindObjectOfType<AudioManager>().Play("KillCountSound");
            killbar.SetMinValue(killCount);
            killCountReward *= 2;
            killbar.SetMaxValue(killCountReward);

            player.SetMaxHealth(player.GetMaxHealth() + 5);
            healthBar.SetMaxValue(player.GetMaxHealth());
            healthBar.SetTextIndicator(player.GetHealth());

        }
    }

    public void ResetKillsSinceRoundStarted()
    {
        killsSinceRoundStarted = 0;
    }

    public int GetKillsSinceRoundStarted()
    {
        return killsSinceRoundStarted;
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public void SubtractExp(int amount)
    {
        exp -= amount;
    }

    public int GetExp()
    {
        return exp;
    }
}
