using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] Text objectiveText;
    [SerializeField] Text objectiveStatsText;
    [SerializeField] private GameObject statsCanvas;
    private int AliveObjective;


    bool pauseTimer = false;
    float timer;
    int objectives;
    bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        statsCanvas.SetActive(false);
        pauseTimer = true;
        UpdateObjectives();
        AliveObjective = FindObjectsOfType<Character>().Where(c => c.characterStatus == CharacterStatusEnum.healthyAndUnmasked).Count();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ShowLevelStats();
        }
        if (isGamePaused == false)
        {
            UpdateObjectives();
        }
    }

    public void UpdateObjectives()
    {
        // objectives = FindObjectsOfType<Character>().Where(c => c.characterStatus == CharacterStatusEnum.healthyAndUnmasked).Count();
        // //objectives += FindObjectsOfType<Character>().Where(c => c.characterStatus == CharacterStatusEnum.sickAndUnmasked).Count();
        // objectiveText.text = objectives.ToString();
        // if (objectives <= 0)
        // {
        //     ShowVictoryScreen();
        //     isGamePaused = true;
        // }
    }

    private void ShowVictoryScreen()
    {
        pauseTimer = true;
        Time.timeScale = .3f;
    }

    public void ShowLevelStats()
    {
        Time.timeScale = 1f;
        statsCanvas.SetActive(true);
        int nbAlive = FindObjectsOfType<Character>().Where(c => c.characterStatus == CharacterStatusEnum.healthyAndMasked).Count();
        objectiveStatsText.text = nbAlive + " / " + AliveObjective;
    }

    private void ShowFailedScreen()
    {
        pauseTimer = true;
        Time.timeScale = .3f;
    }

    IEnumerator WaitAndShowStats()
    {
        yield return new WaitForSeconds(1.5f);
        ShowLevelStats();
    }
}
