using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public ProjectileTypeEnum ProjectileType;
        public float Speed = 5f;
        [SerializeField] private float lifeTime = 0.5f;

        private void Update()
        {
            if (ProjectileType == ProjectileTypeEnum.Gel)
            {
                lifeTime -= Time.deltaTime;
                if (lifeTime <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (ProjectileType == ProjectileTypeEnum.BossProjectile && collision.tag == "Player")
            {
                collision.GetComponent<Player>().TakeDamage();
                Destroy(this.gameObject);
            }
        }
    }
}
