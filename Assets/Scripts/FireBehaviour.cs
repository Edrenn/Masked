using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FireBehaviour : MonoBehaviour
    {
        [SerializeField] Transform firePoint;
        [SerializeField] float gelFiringSpeed;
        [SerializeField] private AimArrow aimArrow;
        public bool CanFire = false;
        public bool CanDoThings = false;
        public bool isFiringGel = false;
        public bool isPreparingFire = false;
        private int counter;
        private GameObject currentProjectile;
        Animator currentAnimator;
        private UIManager uiManager;
        private AudioSource audioSource;

        #region Weapons
        [SerializeField] private bool isMaskAvailable = false;
        [SerializeField] private bool isGelAvailable = false;
        [SerializeField] private bool isSringeAvailable = false;
        [SerializeField] GameObject mask;
        [SerializeField] GameObject sringe;
        [SerializeField] GameObject gel;
        [SerializeField] GameObject gelWeapon;
        private bool isGelCoroutineGelOn = false;
        #endregion

        #region Sounds
        [SerializeField] private AudioClip bendingSound;
        [SerializeField] private AudioClip releaseSound;
        [SerializeField] private AudioClip gelFiringSound1;
            
        #endregion

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            uiManager = FindObjectOfType<UIManager>();
            currentAnimator = GetComponent<Animator>();
            currentProjectile = null;
            if (gelWeapon)
                gelWeapon.SetActive(false);
            counter = 0;
        }

        private void Update()
        {
            if (CanDoThings)
            {
                ChangeWeapon();
                if (currentProjectile != null)
                    FiringBehaviour();
            }
        }

        private void FiringBehaviour()
        {
            // Gel behaviour
            if (currentProjectile == gel)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (isFiringGel == false)
                    {
                        if (!isGelCoroutineGelOn){
                            isGelCoroutineGelOn = true;
                            StartCoroutine(FireGelCycle());
                        }
                        else{
                            FireGel();
                        }
                        isFiringGel = true;
                    }
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    StopCoroutine(FireGelCycle());
                    isGelCoroutineGelOn = false;
                    isFiringGel = false;
                }
            }
            // Mask and sringe behaviour
            else
            {
                #region PrepareFiring
                if (Input.GetButtonDown("Fire1"))
                {
                    audioSource.clip = bendingSound;
                    audioSource.Play();
                    aimArrow.ShowAimArrow();
                    currentAnimator.SetTrigger("PrepareProjectile");
                    isPreparingFire = true;
                }
                #endregion

                #region Fire

                if (Input.GetButtonUp("Fire1") && isPreparingFire)
                {
                    if (CanFire)
                    {
                        audioSource.Stop();
                        currentAnimator.SetTrigger("Fire");
                        audioSource.PlayOneShot(releaseSound);
                    }
                    else{
                        currentAnimator.SetTrigger("ReleaseWithoutFiring");
                        audioSource.Stop();
                    }

                    isPreparingFire = false;

                    aimArrow.HideAimArrow();
                }
                #endregion
            }
        }

        private void ChangeWeapon()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (isMaskAvailable)
                {
                    uiManager.SetMaskImage();
                    currentProjectile = mask;
                    gelWeapon.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (isGelAvailable)
                {
                    uiManager.SetGelImage();
                    currentProjectile = gel;
                    gelWeapon.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (isSringeAvailable)
                {
                    uiManager.SetSringeImage();
                    currentProjectile = sringe;
                    gelWeapon.SetActive(false);
                }
            }
        }

        private void FireGel(){

            Projectile proj = Instantiate(gel, new Vector3( transform.position.x,transform.position.y - 0.2f), Quaternion.identity).GetComponent<Projectile>();
            // get mouse position
            Vector3 mousePose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // direction that the projectile will use
            Vector3 direction = new Vector3(mousePose.x, mousePose.y, 0) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.rotation = angle;
            // Tiny modification for the cone shape firing
            Vector3 newDirection = new Vector3(firePoint.right.x + Random.Range(0,0.1f), firePoint.right.y + Random.Range(0, 0.1f), firePoint.right.z);
            rb.AddForce(newDirection * proj.Speed, ForceMode2D.Impulse);
            
            audioSource.PlayOneShot(gelFiringSound1);
        }

        private IEnumerator FireGelCycle()
        {
            FireGel();
            yield return new WaitForSeconds(gelFiringSpeed);
            if (isFiringGel)
            {
                StartCoroutine(FireGelCycle());
            }
        }

        public void Fire()
        {
            if (CanFire)
            {
                counter++;
                Projectile proj = Instantiate(currentProjectile, transform.position, Quaternion.identity).GetComponent<Projectile>();

                float horizontalAxeValue = Input.GetAxisRaw("RightHorizontal");
                float verticalAxeValue = Input.GetAxisRaw("RightVertical");
                Vector3 direction = new Vector3();

                if (horizontalAxeValue != 0 || verticalAxeValue != 0)
                {
                    direction = new Vector2(horizontalAxeValue, verticalAxeValue);
                }
                else
                {
                    Vector3 mousePose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    direction = new Vector3(mousePose.x, mousePose.y, 0) - transform.position;
                }


                  
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                proj.GetComponent<Rigidbody2D>().rotation = angle;
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * proj.Speed, ForceMode2D.Impulse);
                CanFire = false;
            }
        }

        public void SetMaskProjectile()
        {
            currentProjectile = mask;
        }
        public void SetSringeProjectile()
        {
            currentProjectile = sringe;
        }

         public void UnlockMask()
        {
            FindObjectOfType<Player>().SetCanTakeDamage(true);
            isMaskAvailable = true;
            uiManager.SetMaskImage();
            currentProjectile = mask;
            uiManager.ShowWeaponUI(true);
        }

        public void UnlockGel()
        {
            isGelAvailable = true;
            uiManager.SetGelImage();
            currentProjectile = gel;
            gelWeapon.SetActive(true);
        }

        public void UnlockSringe()
        {
            isSringeAvailable = true;
            uiManager.SetSringeImage();
            currentProjectile = sringe;
        }
    }
}
