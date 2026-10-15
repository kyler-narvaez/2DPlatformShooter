using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private RaycastHit2D hit;

    [SerializeField] private Transform laserFirePoint;

    [SerializeField] private float laserMaxDistance;
    [SerializeField] private int laserDamage;

    [SerializeField] private ParticleSystem chargeParticle;

    //[SerializeField] private Transform target;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DisableLaser();
        StartCoroutine("StartFiring");
    }

    private void Update()
    {
        UpdateLaser();
    }

    IEnumerator StartFiring()
    {

        
        ParticleSystem tempEffect = Instantiate(chargeParticle, laserFirePoint.position, laserFirePoint.rotation, laserFirePoint);
        ParticleSystem.MainModule main = tempEffect.main;
        main.startColor = GetComponent<SpriteRenderer>().color;
        FindObjectOfType<AudioManager>().Play("ChargeSound");

        yield return new WaitUntil(() => tempEffect.isStopped);

        EnableLaser();

        Destroy(tempEffect);
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = lineRenderer.startColor;

        FindObjectOfType<AudioManager>().Play("LaserSound");
        InvokeRepeating("CalculateHits", 0f, 0.2f);
        yield return new WaitForSeconds(5f);

        CancelInvoke();
        DisableLaser();
        
        yield return new WaitForSeconds(3f);
        StartCoroutine("StartFiring");
    }

    private void CalculateHits()
    {
        if (hit && hit.transform.name == "Player")
        {
            CharacterAttributes player = hit.transform.GetComponent<CharacterAttributes>();
            player.SetDamageTaken(laserDamage);
            StartCoroutine(player.DamageEffect(transform.GetComponent<SpriteRenderer>().color));
        }
    }

    public void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    private void UpdateLaser()
    {
        hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
        lineRenderer.SetPosition(0, laserFirePoint.position);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.right * laserMaxDistance);
        }
    }

    public int GetLaserDamage()
    {
        return laserDamage;
    }
}
