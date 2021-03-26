using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float slowShootingSpeed;
    [SerializeField] private float heavyShootingSpeed;
    private float currentShootingSpeed;

    private void Start()
    {
        currentShootingSpeed = slowShootingSpeed;
    }

    public void StartFiring()
    {
        StartCoroutine(FireCycle());
    }

    public void StartHeavyFire()
    {
        currentShootingSpeed = heavyShootingSpeed;
    }

    public void StopHeavyFire()
    {
        currentShootingSpeed = slowShootingSpeed;
    }

    private IEnumerator FireCycle()
    {
        Fire();
        yield return new WaitForSeconds(Random.Range(0.5f, currentShootingSpeed));
        StartCoroutine(FireCycle());
    }

    // Update is called once per frame
    public void Fire()
    {
        // projectile instanciation
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        
        // turn the object to right direction
        float angle = Mathf.Atan2(this.transform.up.y, this.transform.up.x) * Mathf.Rad2Deg;
        proj.GetComponent<Rigidbody2D>().rotation = angle;

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(this.transform.up * proj.Speed, ForceMode2D.Impulse);

    }
}
