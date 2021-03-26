using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    public void LoadSelectedLevel(int levelId)
    {
        FindObjectOfType<SceneController>().LoadLevel(levelId);
    }
}
