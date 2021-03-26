using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] public int healthPoint;
    [SerializeField] private int currentPhase;
    [SerializeField] private float timeCounter;
    private Animator bossAnimator;

    #region Phase 1
    [SerializeField] private GameObject rightAlert;
    [SerializeField] private GameObject leftAlert;
    [SerializeField] private Animator stoneFallRightAnimator;
    [SerializeField] private Animator stoneFallLeftAnimator;
    [SerializeField] private float timeBetweenStones;
    [SerializeField] private float timeForStonePhase;
    [SerializeField] private float timeForFiringPhase;
    [SerializeField] private PolygonCollider2D phaseOneCollider;
    [SerializeField] private TurretBehaviour phaseOneTurret;
    [SerializeField] private bool isLaunchingStones;
    #endregion

    #region Interphase 1
    [SerializeField] private GameObject virusesGroup;
    private int virusCounter;
    #endregion

    #region Phase 2
    [SerializeField] private Animator globalTentaclesAnimator;
    [SerializeField] private List<Animator> tentacles;
    [SerializeField] private TentacleManager tentacleManager;
    #endregion
    #endregion

    void Start()
    {
        bossAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
    }



    #region Phase1
    public void StartPhaseOne()
    {
        StartCoroutine(SetIdleTentacles());
        //StartCoroutine(PhaseOneAttacksCycle());
        phaseOneTurret.StartFiring();
    }

    /// <summary>
    /// Wait between 1 & 4 sec and launch the LaunchStones Coroutine with a random stone selection
    /// If the stone phase is finished launch the firing phase
    /// </summary>
    /// <returns></returns>
    private IEnumerator PhaseOneAttacksCycle()
    {
        // Stone fall phase 
        if (timeCounter < timeForStonePhase)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 4));
            int stoneSelector = UnityEngine.Random.Range(0, 2);
            if (stoneSelector == 0)
                StartCoroutine(LaunchStones(rightAlert, leftAlert, stoneFallRightAnimator, stoneFallLeftAnimator));
            else
                StartCoroutine(LaunchStones(leftAlert, rightAlert, stoneFallLeftAnimator, stoneFallRightAnimator));
        }
        // Fire phase 
        else
        {
            // Wait until all stones animation are done
            yield return new WaitUntil(() => isLaunchingStones == false);
            if (phaseOneTurret.isFiring == false)
                phaseOneTurret.StartHeavyFiring();
            yield return new WaitUntil(() => timeCounter >= timeForFiringPhase);
            phaseOneTurret.StopHeavyFiring();
            timeCounter = 0;

            // Reload the cycle
            StartCoroutine(PhaseOneAttacksCycle());
        }
    }

    private IEnumerator LaunchStones(GameObject firstAlert, GameObject secondAlert, Animator firstStone, Animator secondStone)
    {
        isLaunchingStones = true;
        // Show first alert
        firstAlert.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        // Show second alert / Hide first alert
        firstAlert.SetActive(false);
        secondAlert.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        // Hide second alert
        secondAlert.SetActive(false);

        // Launch 1st stone fall animation
        firstStone.SetTrigger("LaunchFall");
        yield return new WaitForSeconds(timeBetweenStones);
        // Launch 2nd stone fall animation
        secondStone.SetTrigger("LaunchFall");

        // relaunch the cycle
        yield return new WaitForSeconds(timeBetweenStones);

        isLaunchingStones = false;
        StartCoroutine(PhaseOneAttacksCycle());
    }
    #endregion

    #region Interphase
    /// <summary>
    /// Method that purpose is to be called by lil' viruses when they dies
    /// </summary>
    private void MonsterDestroyed()
    {
        virusCounter--;
        if (virusCounter <= 0)
        {
            ChangePhase(3);
        }
    }
    #endregion

    #region Phase3
    private IEnumerator PhaseTwoAttackCycle()
    {
        yield return new WaitForSeconds(3);
        // LaunchTentacleAttack
        tentacleManager.Attack();
        yield return new WaitForSeconds(3);
        StartCoroutine(PhaseTwoAttackCycle());
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sringe")
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        healthPoint -= 1;
        bossAnimator.SetTrigger("TakeDamage");
        if (healthPoint <= 0)
        {
            if (currentPhase == 1)
            {
                phaseOneTurret.StopHeavyFiring();
                bossAnimator.SetTrigger("Phase1Disappear");
            }
        }
    }

    private void ChangePhase(int newPhase = 0)
    {
        // Optionnal parameter to load a specific phase
        if (newPhase != 0)
        {
            currentPhase = newPhase;
        }
        
        // Changes from P1 to P2
        if (currentPhase == 1)
        {
            StopAllCoroutines();
            currentPhase = 2;
            // Spawn small viruses
            virusesGroup.SetActive(true);
            virusCounter = FindObjectsOfType<Monster>().Length;
            foreach (Monster virus in FindObjectsOfType<Monster>())
            {
                virus.MonsterDestroyedDelegate += MonsterDestroyed;
            }
        }

        // Load P3
        if (currentPhase == 3)
        {
            // Spawn tentacles
            StartCoroutine(SetIdleTentacles());
        }
    }

    public void ActivateHitBox()
    {
        switch (currentPhase)
        {
            case 1:
                phaseOneCollider.enabled = true;
                break;
        }
    }

    public void DesactivateHitBox()
    {
        switch (currentPhase)
        {
            case 1:
                phaseOneCollider.enabled = false;
                break;
        }
    }

    public IEnumerator SetIdleTentacles()
    {
        globalTentaclesAnimator.SetTrigger("ShowTentacles");
        yield return new WaitForSeconds(4.3f);
        foreach (Animator tentacle in tentacles)
        {
            tentacle.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(PhaseTwoAttackCycle());
    }
}
