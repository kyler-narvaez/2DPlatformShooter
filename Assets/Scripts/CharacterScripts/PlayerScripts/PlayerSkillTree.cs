using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillTree : MonoBehaviour
{
    [SerializeField] private int jumpCeiling;
    [SerializeField] private int distanceCeiling;
    [SerializeField] private TextIndicators text;

    private int jumpCounter;

    private Player player;
    private PlayerMovement playerMovement;
    private CharacterAttributes playerAttributes;

    public enum SkillType
    {
        healthPerKill_1,
        healthPerKill_2,
        regeneration_1,
        regeneration_2,
        shield,
        shieldDamage,
        //reflectBullets
        slowMo_1,
    }

    private List<SkillType> unlockedSkillTypes;

    private void Start()
    {
        player = GetComponent<Player>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttributes = player.GetComponent<CharacterAttributes>();
        unlockedSkillTypes = new List<SkillType>();
    }

    // Jump logic

    private void CheckJumpUpgrade()
    {
        if (playerMovement.GetJumpsUsed() == jumpCeiling)
        {
            playerMovement.IncreaseJumpAbility(1);
            jumpCeiling *= 3;
        }
    }

    //Speed logic
    private void CheckSpeedUpgrade()
    {
        if (playerMovement.GetDistanceTravelled() > distanceCeiling)
        {
            playerMovement.IncreaseSpeedAbility(2);
            distanceCeiling *= 2;
        }
    }

    //UI
    public void SetValues()
    {
        text.SetSkillTreeValues("Max Health", playerAttributes.GetHealth(), "EXP", player.GetExp(), "Speed", playerMovement.GetSpeed(), "Jump Count", playerMovement.GetJumpCount());
    }

    //Skill Tree
    public void UnlockHealthPerKill_1()
    {
        if (!IsSkillUnlocked(SkillType.healthPerKill_1) && player.GetExp() > 100)
        {
            unlockedSkillTypes.Add(SkillType.healthPerKill_1);
            player.healthPerKill -= 1;
            player.SubtractExp(100);
            SetValues();
        }
    }

    public void UnlockSlowMo_1()
    {
        if (!IsSkillUnlocked(SkillType.slowMo_1) && player.GetExp() > 250)
        {
            unlockedSkillTypes.Add(SkillType.slowMo_1);
            player.SubtractExp(250);
            SetValues();
            player.slowMoRecharge = 5f;
        }
    }

    public bool IsSkillUnlocked(SkillType skill)
    {
        return unlockedSkillTypes.Contains(skill);
    }

    //OTHER
    private void Update()
    {
        //Passive Upgrade Checks
        CheckJumpUpgrade(); // FIXME - Make there be a limit
        CheckSpeedUpgrade();
    }
}
