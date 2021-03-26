using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Character : MonoBehaviour, IInfectable
    {
        #region Variables
        #region SerializedVariables
        [SerializeField] Sprite maskedSprite;
        [SerializeField] Sprite deathSprite;
        [SerializeField] GameObject gameObjectToFlip;
        [SerializeField] float speed = 1f;
        [SerializeField] GameObject RangeUi;
        [SerializeField] HealthBar healthBar;
        [SerializeField] bool canDoAnything = true;
        [SerializeField] List<Dialog> lines;
        [SerializeField] Image bubbleNotif;
        #endregion

        public string charactersName;
        public bool isSpeaking = false;
        public bool isPlayerInSpeakRange = false;

        bool hasSpeak = false;
        bool canMove = true;
        bool isAlive = true;
        float countdownBeforeStop;
        float countdownBeforeMovingAgain;
        Animator currentAnimator;
        Vector3 direction = new Vector3(1, 0, 0);
        Rigidbody2D rigidbody;
        SoundManager soundManager;
        SneezeRange sneezeRange;

        public CharacterStatusEnum characterStatus;
        #endregion

        private void Start()
        {
            // Disable bubble notif if the character won't speak
            if (lines == null || lines.Count <= 0)
                bubbleNotif.enabled = false;
            currentAnimator = GetComponentInChildren<Animator>();
            ChangeCharacterStatus(characterStatus);
            RangeUi.SetActive(false);
            soundManager = GetComponent<SoundManager>();
            rigidbody = GetComponent<Rigidbody2D>();
            gameObject.transform.localScale = new Vector3(-1, 1, 0);
            RandomizeDirection();
            countdownBeforeStop = UnityEngine.Random.Range(1, 5);
            sneezeRange = GetComponentInChildren<SneezeRange>();
            healthBar.hpReachesZero_Event += Die;
        }

        private void Update()
        {
            if (lines.Count > 0 && isPlayerInSpeakRange && isSpeaking == false && Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<DialogBox>().SetNameAndCharacter(this);
                FindObjectOfType<DialogBox>().AddNewLines(lines);
                canDoAnything = false;
                Stop();
                isSpeaking = true;
            }

            if (canDoAnything)
            {
                MovingBehaviour();
            }
            else
            {
                Stop();
            }
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
            if (canDoAnything)
            {
                direction = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0);
                if (direction.x > 0)
                {
                    gameObjectToFlip.transform.localScale = new Vector3(1, 1, 0);
                }
                else
                {
                    gameObjectToFlip.transform.localScale = new Vector3(-1, 1, 0);
                }
            }
        }

        public void Stop()
        {
            if (rigidbody)
                rigidbody.velocity = Vector2.zero;
            direction = Vector2.zero;
            canMove = false;
            if (currentAnimator)
                currentAnimator.SetBool("isWalking", false);
        }

        public void RestartMoving()
        {
            RandomizeDirection();
            countdownBeforeMovingAgain = UnityEngine.Random.Range(1, 5);
            canDoAnything = true;
            canMove = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isAlive)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("AllyProjectile"))
                {
                    Projectile projectile = collision.GetComponent<Projectile>();
                    switch (projectile.ProjectileType)
                    {
                        case ProjectileTypeEnum.Mask:
                            if (characterStatus.HasFlag(CharacterStatusEnum.unmasked))
                            {
                                Masked();
                                Destroy(projectile.gameObject);
                            }
                            break;
                        case ProjectileTypeEnum.Sringe:
                            if (!characterStatus.HasFlag(CharacterStatusEnum.healthy))
                            {
                                Heal();
                                Destroy(projectile.gameObject);
                            }
                            break;
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (canMove == false && collision.gameObject.GetComponent<Player>())
                Stop();

            if (collision.gameObject.layer == LayerMask.NameToLayer("Foreground"))
            {
                countdownBeforeStop = 0;
            }
        }

        private void ChangeCharacterStatus(CharacterStatusEnum newStatus)
        {
            characterStatus = newStatus;
            switch (newStatus)
            {
                case CharacterStatusEnum.healthyAndMasked:
                    GetComponentInChildren<SpriteRenderer>().sprite = maskedSprite;
                    gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    break;

                case CharacterStatusEnum.healthyAndUnmasked:
                    gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    break;

                case CharacterStatusEnum.sickAndMasked:
                    GetComponentInChildren<SpriteRenderer>().sprite = maskedSprite;
                    gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    healthBar.takeInfectiousDamage = true;
                    StartCoroutine(LaunchSneezeCycle());
                    break;

                case CharacterStatusEnum.sickAndUnmasked:
                    if (SceneManager.GetActiveScene().buildIndex != 0)
                    {
                        healthBar.takeInfectiousDamage = true;
                        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                        StartCoroutine(LaunchSneezeCycle());
                    }
                    break;
                default:
                    break;
            }
        }

        #region Sneeze
        IEnumerator LaunchSneezeCycle()
        {
            int timeBeforeSneeze = UnityEngine.Random.Range(1, 5);
            yield return new WaitForSeconds(timeBeforeSneeze);
            if (characterStatus.HasFlag(CharacterStatusEnum.sick))
            {
                Stop();
                canDoAnything = false;
                currentAnimator.SetTrigger("Sneeze");
            }
        }

        public void Sneeze()
        {
            if (isAlive == true)
            {
                if (characterStatus.HasFlag(CharacterStatusEnum.sick))
                {
                    foreach (var infectable in sneezeRange.GetAllInfectablesInRange())
                    {
                        infectable.GotSneezedOn();
                    }
                    StartCoroutine(LaunchSneezeCycle());
                }
                canDoAnything = true;
            }
        }

        public void GotSneezedOn()
        {
            if (characterStatus == CharacterStatusEnum.healthyAndUnmasked)
            {
                ChangeCharacterStatus(CharacterStatusEnum.sickAndUnmasked);
            }
        }
        #endregion

        /// <summary>
        /// Allow the character to move again, and set the hasSpeak notification
        /// </summary>
        public void SetEndOfSpeaking()
        {
            isSpeaking = false;
            hasSpeak = true;
            RestartMoving();
            bubbleNotif.color = new Color(0.2641f,0.2641f,0.2641f);
        }

        private void Die()
        {
            // Stopping the character
            Stop();
            canDoAnything = false;
            canMove = false;
            isAlive = false;

            // Changing the psrite
            SpriteRenderer spriteRendrer = GetComponentInChildren<SpriteRenderer>();
            spriteRendrer.sprite = deathSprite;
            spriteRendrer.color = new Color(spriteRendrer.color.r, spriteRendrer.color.g, spriteRendrer.color.b, 1);
            currentAnimator.enabled = false;
          
            RangeUi.SetActive(false);
            healthBar.gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
            StopAllCoroutines();
        }

        /// <summary>
        /// Behaviour when a character get masked
        /// </summary>
        private void Masked()
        {
            // Sound
            soundManager.PlaySound();
            // Animation 
            currentAnimator.SetTrigger("Masked");
            canDoAnything = false;
            switch (characterStatus)
            {
                case CharacterStatusEnum.healthyAndUnmasked:
                    ChangeCharacterStatus(CharacterStatusEnum.healthyAndMasked);
                    break;
                case CharacterStatusEnum.sickAndUnmasked:
                    ChangeCharacterStatus(CharacterStatusEnum.sickAndMasked);
                    break;
                default:
                    break;
            }
        }

        private void Heal()
        {
            // Sound
            soundManager.PlaySound();
            // Animation
            currentAnimator.SetTrigger("Healed");

            healthBar.takeInfectiousDamage = false;
            StopCoroutine(LaunchSneezeCycle());
            canDoAnything = true;
            switch (characterStatus)
            {
                case CharacterStatusEnum.sickAndUnmasked:
                    ChangeCharacterStatus(CharacterStatusEnum.healthyAndUnmasked);
                    break;
                case CharacterStatusEnum.sickAndMasked:
                    ChangeCharacterStatus(CharacterStatusEnum.healthyAndMasked);
                    break;
                default:
                    break;
            }
        }
    }
}