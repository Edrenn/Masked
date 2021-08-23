using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MonsterDestroyed();

public class Monster : MonoBehaviour
{
    public MonsterDestroyed MonsterDestroyedDelegate;

    [SerializeField] private int healthPoint;
    float speed = 1f;
    GameObject gameObjectToFlip;

    [SerializeField] private float detectionRange;

    [SerializeField] private bool canMove = false;
    [SerializeField] private bool isPlayerInRange = false;

    private Animator currentAnimator;
    private Rigidbody2D rigidbody;
    private Vector3 direction = new Vector3(1, 0, 0);
    private float countdownBeforeStop;
    private float countdownBeforeMovingAgain;
    private Player currentPlayer;
    private AudioSource audioSource;
    [SerializeField] AudioClip aggroSound;
    [SerializeField] AudioClip deathSound;
    private bool aggroSoundPlayed = false;
    private bool deathCoroutineLaunched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.layer == LayerMask.NameToLayer("AllyProjectile"))
            {
                Projectile projectile = collision.GetComponent<Projectile>();
                switch (projectile.ProjectileType)
                {
                    case ProjectileTypeEnum.Mask:
                    case ProjectileTypeEnum.Sringe:
                        break;
                    case ProjectileTypeEnum.Gel:
                    TakeDamage();
                    Destroy(collision.gameObject);
                        break;
                }
            }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAnimator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        currentPlayer = FindObjectOfType<Player>();
    }

    private void TakeDamage()
    {
        GetComponentInChildren<Animator>().SetTrigger("TakeDamage");
        healthPoint--;
        if (healthPoint <= 0)
        {
            if (!deathCoroutineLaunched)
                StartCoroutine("Death");
        }
    }

    IEnumerator Death(){
        deathCoroutineLaunched = true;
        audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(0.19f);
        Destroy(this.gameObject);
    }
    private void Update()
    {
        if (currentPlayer != null)
        {
            if (detectionRange >= Vector3.Distance(this.transform.position, currentPlayer.transform.position))
            {
                isPlayerInRange = true;
                if (aggroSoundPlayed == false){
                    audioSource.PlayOneShot(aggroSound);
                    aggroSoundPlayed = true;
                }

            }

            if (canMove)
            {
                if (isPlayerInRange)
                {
                    MoveToPlayer();
                }
            }
        }
    }

    public void SetPlayerInRange()
    {
        isPlayerInRange = true;
        canMove = true;
    }
    public void SetPlayerNotInRange()
    {
        isPlayerInRange = false;
    }

    public void SetCanMove(bool value)
    {
        Debug.Log("Can move = " + value);
        canMove = value;
    }

    void MoveToPlayer()
    {
        // find where player
        Vector2 directionToPlayer = currentPlayer.transform.position - transform.position;

        // set velocity
        rigidbody.velocity = new Vector2(Mathf.Clamp(directionToPlayer.x * speed ,-speed, speed), Mathf.Clamp(directionToPlayer.y * speed, -speed, speed));
    }

    void MovingBehaviour()
    {
        if (canMove)
        {
            currentAnimator.SetBool("isWalking", true);
            rigidbody.velocity = new Vector2(direction.x * speed, direction.y * speed);
            countdownBeforeStop -= Time.deltaTime;
            if (countdownBeforeStop <= 0)
            {
                countdownBeforeMovingAgain = UnityEngine.Random.Range(1, 5);
                canMove = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            currentAnimator.SetBool("isWalking", false);
            countdownBeforeMovingAgain -= Time.deltaTime;
            if (countdownBeforeMovingAgain <= 0)
            {
                RandomizeDirection();
                countdownBeforeStop = UnityEngine.Random.Range(1, 5);
                canMove = true;
            }
        }
    }

    private void RandomizeDirection()
    {
        direction = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0);
        if (direction.x > 0)
        {
            gameObjectToFlip.transform.localScale = new Vector3(-1, 1, 0);
        }
        else
        {
            gameObjectToFlip.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    private void OnDestroy()
    {
        // Launch all actions on Destroy
        if (MonsterDestroyedDelegate != null)
        {
            MonsterDestroyedDelegate.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
        }
    }
}
