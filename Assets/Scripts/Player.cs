using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        #region Variables
        [SerializeField] float walkSpeed = 2f;
        [SerializeField] float runSpeed = 6f;
        [SerializeField] GameObject gameObjectToFlip;
        [SerializeField] FireBehaviour fireBehaviour;
        [SerializeField] private Animator currentAnimator;
        [SerializeField] private int healthPoint = 3;
        [SerializeField] private float invicibilityFrameDuration;
        [SerializeField] private bool canTakeDamage = true;
        public bool CanDoThings = true;
        bool isAimingWeaponLeft = false;
        bool isAimingWeaponRight = false;
        float currentSpeed = 2f;
        float horizontalAxeValue;
        float verticalAxeValue;
        private Rigidbody2D rigidbody;
        #endregion

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            currentSpeed = walkSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (CanDoThings)
            {
                MoveBehaviour();
            }
        }

        private void MoveBehaviour()
        {
            // Movement
            horizontalAxeValue = Input.GetAxisRaw("Horizontal");
            if (horizontalAxeValue > 0)
                FlipSprite(1);
            else if (horizontalAxeValue < 0)
                FlipSprite(-1);
            verticalAxeValue = Input.GetAxisRaw("Vertical");

            // Set Running
            if (Input.GetButton("Run"))
            {
                currentSpeed = runSpeed;
            }

            if (Input.GetButtonUp("Run"))
            {
                currentSpeed = walkSpeed;
            }
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = new Vector2(horizontalAxeValue * currentSpeed, verticalAxeValue * currentSpeed);
            if (rigidbody.velocity != Vector2.zero)
                currentAnimator.SetBool("isWalking", true);
            else
                currentAnimator.SetBool("isWalking", false);
        }

        void FlipSprite(int value)
        {
            gameObjectToFlip.transform.localScale = new Vector3(value, 1, 0);
        }

        public void SetCanDoThings(bool value)
        {
            CanDoThings = value;
            fireBehaviour.CanDoThings = value;
            currentAnimator.SetBool("isWalking", false);
            horizontalAxeValue = 0;
            verticalAxeValue = 0;
            if (rigidbody != null)
                rigidbody.velocity = Vector2.zero;
        }

        public void TakeDamage()
        {
            if (canTakeDamage)
            {
                healthPoint -= 1;
                StartCoroutine(InvicibilityFrame());
                Debug.Log("Take 1 dmg");
                if (healthPoint <= 0)
                {
                    // DEAD TODO : Add Behaviour de ses morts
                    Destroy(this.gameObject);
                    Debug.Log("DED");
                }
            }
        }

        private IEnumerator InvicibilityFrame()
        {
            canTakeDamage = false;
            currentAnimator.SetBool("CanTakeDamage", false);
            yield return new WaitForSeconds(invicibilityFrameDuration);
            currentAnimator.SetBool("CanTakeDamage", true);
            canTakeDamage = true;
        }
    }
}
