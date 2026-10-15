using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private int maxHealth;

    private int health;
    //private Player player;

    private SpriteRenderer rend;
    private Color origColor;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        rend = GetComponent<SpriteRenderer>();
        origColor = rend.color;
    }

    public void IncrementHealth(int addedHealth)
    {
        if (health + addedHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += addedHealth;
        }
    }

    public void SetMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
        health = maxHealth;
    }

    public void SetDamageTaken(int damage)
    {
        health -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("DeathSound");

            deathParticles = Instantiate(deathParticles, transform.position, transform.rotation);
            ParticleSystem.MainModule main = deathParticles.main;
            main.startColor = origColor;

            Destroy(gameObject);
        }
    }

    //FIXME add this to the generic Enemy script
    private void OnDestroy()
    {
        
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<Player>().OnKill(maxHealth);
        }
    }

    public IEnumerator DamageEffect(Color damageColor)
    {
        //Change color to red
        rend.color = damageColor;

        //waits for # seconds
        yield return new WaitForSeconds(0.2f);

        //Change color back to original color after # seconds
        rend.color = origColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(transform.tag);
        // Checks if collided with "bullet" and if the "Bullet" is of this object's or not
        if (collision.gameObject.GetComponent<Bullet>() != null && !collision.gameObject.GetComponent<Bullet>().CompareTag(transform.tag))
        {
            SetDamageTaken(collision.GetComponent<Bullet>().GetBulletDamage());
            StartCoroutine(DamageEffect(collision.gameObject.GetComponent<SpriteRenderer>().color)); //FIXME
        }

    }


    //Getters
    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
