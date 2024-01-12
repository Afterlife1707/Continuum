using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static Action PlayerDeathEvent;
    public GameObject DeathPanel, DialoguePanel, pausePanel;
    public GameObject player, gun;
    public static GameManager instance;
    public SceneManagerHandler sceneManagerHandler;
    public int levelNum;
    public bool isDialogueDisplayed, levelComplete, gameComplete;
    private void OnEnable()
    {
        PlayerDeathEvent += PlayerDead;
    }
    private void OnDisable()
    {
        PlayerDeathEvent -= PlayerDead;
    }
    private void Start()
    {
        Cursor.visible = false;
        instance = this;
        isDialogueDisplayed = true;
        StartCoroutine(WaitBeforeAction(2, DialogueSystem.StartInitialDialogueEvent));
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeInHierarchy)
        {
            PauseMenu.IsPaused = true;
            OpenPauseMenu();
        }
    }
    void PlayerDead()
    {
        Debug.Log("PlayerDead");
        Cursor.visible = true;
        player.GetComponent<PlayerMovement>().moveSpeed = 0;
        player.GetComponent<Animator>().SetTrigger("Dead");
        gun.SetActive(false);
        StartCoroutine(DelayAfterDeath());
    }
    IEnumerator DelayAfterDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(player);
        DeathPanel.SetActive(true);
    }
    public void OnLevelComplete()
    {
        DataHandler.instance.level = levelNum;
        DataHandler.instance.SaveDataHandler();
        if(levelNum==3)
        {
            //endgame
            gameComplete = true;
            isDialogueDisplayed = true;
            levelComplete = true;
            StartCoroutine(WaitBeforeAction(1, DialogueSystem.StartGameEndDialogueEvent));
            return;
        }
        else
        {
            sceneManagerHandler.LoadNextLevel(levelNum + 1);
        }
    }
    public void StartEndDialogues()
    {
        isDialogueDisplayed = true;
        levelComplete = true;
        DialoguePanel.SetActive(true);
        StartCoroutine(WaitBeforeAction(2, DialogueSystem.StartEndDialogueEvent));
    }
    IEnumerator WaitBeforeAction(float f, Action action)
    {
        yield return new WaitForSeconds(f);
        DialoguePanel.SetActive(true);
        action?.Invoke();
    }

    void OpenPauseMenu()
    {
        Cursor.visible = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
