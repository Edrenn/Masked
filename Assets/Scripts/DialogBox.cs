using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void EndOfDialogAction();

public class DialogBox : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Dialog> allLines;
    [SerializeField] private string characterName;
    [SerializeField] private float writingSpeed = 0.03f;
    [SerializeField] private Text textObjectToWrite;
    [SerializeField] private Text nameObjectToWrite;
    [SerializeField] private GameObject dialogBox;

    public EndOfDialogAction endOfDialogAction;

    private Queue<Dialog> linesQueue;
    private string textToWrite;
    private Character currentlySpeakingCharacter;
    /// <summary>
    /// True if the current line is entirely written
    /// </summary>
    private bool canLoadNextLine = true;
    #endregion

    void Start()
    {
        nameObjectToWrite.text = characterName;
        linesQueue = new Queue<Dialog>();
        // Launch 1st lines
        if (allLines != null && allLines.Count > 0)
        {
            SetPlayerCanDoThing(false);
            foreach (var line in allLines)
            {
                linesQueue.Enqueue(line);
            }
            LoadNewLine();
            StartCoroutine(writeText());
        }
        else
        {
            HideDialogBox();
        }
    }
    IEnumerator writeText()
    {
        textObjectToWrite.text = "";
        canLoadNextLine = false;

        foreach (char letter in textToWrite)
        {
            textObjectToWrite.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }

        canLoadNextLine = true;
    }

    private void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonDown("Use") || Input.GetButtonDown("Fire1"))
            {
                if (canLoadNextLine)
                {
                    if (linesQueue.Count > 0)
                    {
                        LoadNewLine();
                        StartCoroutine(writeText());
                    }
                    else
                    {

                        HideDialogBox();
                        if (currentlySpeakingCharacter != null)
                            currentlySpeakingCharacter.SetEndOfSpeaking();
                        if (endOfDialogAction != null)
                        {
                            endOfDialogAction();
                        }
                    }
                }
                else
                {
                    StopAllCoroutines();
                    FastTextFilling();
                    canLoadNextLine = true;
                }
            }
        }
    }

    private void FastTextFilling()
    {
        textObjectToWrite.text = textToWrite;
    }

    private void SetPlayerCanDoThing(bool value)
    {
        Player player = FindObjectOfType<Player>();
        player.SetCanDoThings(value);
    }

    private void StartSpeaking()
    {
        SetPlayerCanDoThing(false);
        LoadNewLine();
        StartCoroutine(writeText());
        dialogBox.SetActive(true);
    }

    public void AddNewLines(List<Dialog> lines)
    {
        linesQueue.Clear();
        foreach (var line in lines)
        {
            linesQueue.Enqueue(line);
        }
        StartSpeaking();
    }

    public void AddNewLine(Dialog dialog)
    {
        linesQueue.Clear();
        linesQueue.Enqueue(dialog);
        StartSpeaking();
    }

    public void SetNameAndCharacter(Character character)
    {
        characterName = character.charactersName;
        currentlySpeakingCharacter = character;
    }

    public void SetName(string name)
    {
        characterName = name;
    }

    public void SetEndOfDialogAction(EndOfDialogAction value)
    {
        endOfDialogAction = value;
    }

    private void HideDialogBox()
    {
        FindObjectOfType<UIManager>().SetCinematicBarsActive(false);
        SetPlayerCanDoThing(true);
        dialogBox.SetActive(false);
    }

    private void LoadNewLine()
    {
        Dialog dialog = linesQueue.Dequeue();
        if (!string.IsNullOrEmpty(dialog.Name))
            nameObjectToWrite.text = dialog.Name + " :";
        textToWrite = dialog.Text;
    }
}
