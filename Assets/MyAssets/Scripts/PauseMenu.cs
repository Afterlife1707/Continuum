using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel, controlsPanel, optionsPanel;
    [SerializeField] AudioSource bgMusic;
    bool isMusic = true, isSound = true;
    [SerializeField]TMP_Text musicText, soundText, sliderValue;
    //Slider sensSlider;
    public static bool IsPaused = false;
    //TMP_Text popUp;

    private void Start()
    {
        if (bgMusic == null)
            bgMusic = FindObjectOfType<BGMusic>().gameObject.GetComponent<AudioSource>();
        SaveData data = DataHandler.instance.LoadDataHandler();
        //isMusic = data.isMusic;
        isSound = data.isSound;
        // ToggleMusic(musicText);
        if (!isSound)
            soundText.text = "Sound:Off";
        AudioListener.pause = isSound;
    }
    private void Update()
    {
        //sliderValue.text = sensSlider.value.ToString();
    }
    public void Unpause()
    {
        if (GameManager.instance.isDialogueDisplayed || GameManager.instance.levelComplete || GameManager.instance.gameComplete)
            Cursor.visible = true;
        else
            Cursor.visible = false;
        IsPaused = false;
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    //public void OpenPopUp(string popUpText)
    //{
    //    popUp.gameObject.SetActive(true);
    //    popUp.text = popUpText;
    //}
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //public void ToggleMusic(TMP_Text text) //not being used
    //{
    //    isMusic = !isMusic;
    //    if (isMusic)
    //    {
    //        if (bgMusic.isPlaying)
    //            return;
    //        text.text = "On";
    //        bgMusic.Play();
    //    }
    //    else
    //    {
    //        text.text = "Off";
    //        bgMusic.Pause();
    //    }

    //}
    public void ToggleSound(TMP_Text text)
    {
        isSound = !isSound;
        AudioListener.pause = isSound;
        DataHandler.instance.SaveDataHandler();
        if (AudioListener.pause)
        {
            text.text = "Sound:Off";
        }
        else
        {
            text.text = "Sound:On";
        }
    }
}
