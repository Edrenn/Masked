using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();

        float angle = Mathf.Atan2(-1, 0) * Mathf.Rad2Deg;
        proj.GetComponent<Rigidbody2D>().rotation = angle;
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(this.transform.right * proj.Speed, ForceMode2D.Impulse);
    }
}
