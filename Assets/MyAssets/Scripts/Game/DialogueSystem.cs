using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent, speakerName;
    public string[] initialDialogues;
    public string[] endDialogues;
    public string[] gameEndDialogues;
    public float textSpeed;
    public static Action StartInitialDialogueEvent;
    public static Action StartEndDialogueEvent;
    public static Action StartGameEndDialogueEvent;
    public GameObject OldGuy, GameEndPanel, skipBtn;
    Animator anim;
    public bool animPlaying;

    [SerializeField]private int index;

    private void OnEnable()
    {
        StartInitialDialogueEvent += StartInitialDialogue;
        StartEndDialogueEvent += StartEndDialogue;
        StartGameEndDialogueEvent += StartGameEndDialogue;
        anim = GetComponent<Animator>();
    }
    private void OnDisable()
    {
        StartInitialDialogueEvent -= StartInitialDialogue;
        StartEndDialogueEvent -= StartEndDialogue;
        StartGameEndDialogueEvent -= StartGameEndDialogue;
    }

    void Update()
    {
        if (animPlaying || PauseMenu.IsPaused)
            return;
        if(Input.GetMouseButtonDown(0))
        {
            if(!GameManager.instance.levelComplete && !GameManager.instance.gameComplete)
            {
                if (textComponent.text == initialDialogues[index])
                    NextDialogue(initialDialogues);
                else
                {
                    StopAllCoroutines();
                    textComponent.text = initialDialogues[index];
                }
            }
            else if(GameManager.instance.levelComplete && !GameManager.instance.gameComplete)
            {
                if (textComponent.text == endDialogues[index])
                    NextDialogue(endDialogues);
                else
                {
                    StopAllCoroutines();
                    textComponent.text = endDialogues[index];
                }
            }
            else if(GameManager.instance.gameComplete)
            {
                if (textComponent.text == gameEndDialogues[index])
                    NextDialogue(gameEndDialogues);
                else
                {
                    StopAllCoroutines();
                    textComponent.text = gameEndDialogues[index];
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.isDialogueDisplayed)
        //    DialogueEnd();
    }

    void StartInitialDialogue()
    {
        index = 0;
        speakerName.text = "OLD GUY";
        textComponent.text = string.Empty;
        OldGuy.SetActive(true);
        StartCoroutine(TypeLine(initialDialogues));
    }
    void StartEndDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        OldGuy.SetActive(true);
        StartCoroutine(TypeLine(endDialogues));
    }
    void StartGameEndDialogue()
    {
        index = 0;
        speakerName.text = "ME";
        textComponent.text = string.Empty;
        OldGuy.SetActive(false);
        StartCoroutine(TypeLine(gameEndDialogues));
    }

    public void SkipDialogueBtn()
    {
        DialogueEnd();
    }


    IEnumerator TypeLine(string [] dialogues)
    {
        Cursor.visible = true;
        skipBtn.SetActive(true);
        foreach (char c in dialogues[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextDialogue(string[] dialogues)
    {
        if(index< dialogues.Length-1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine(dialogues));
        }
        else
        {
            DialogueEnd();
        }
    }
    void DialogueEnd()
    {
        index = 0;
        if (GameManager.instance.gameComplete)
        {
            Cursor.visible = true;
            skipBtn.SetActive(true);
            GameEndPanel.SetActive(true); //not required in lvl 1 and 2
            return;
        }

        Cursor.visible = false;
        skipBtn.SetActive(false);
        anim.SetTrigger("FadeOut");
        if (GameManager.instance.levelComplete)
        {
            GameManager.instance.OnLevelComplete();
        }
        Shooting.instance.closeUIFire = true;
        GameManager.instance.isDialogueDisplayed = false;
        textComponent.text = string.Empty;
        OldGuy.SetActive(false);
        gameObject.SetActive(false);
    }
}
