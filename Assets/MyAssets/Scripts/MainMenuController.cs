using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    public GameObject levelsBtn,levelsPanel;
    public Animator anim;
    public Image fade;
    private void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        instance = this;
    }

    public void EnableLevels(int levels)
    {
        levelsBtn.SetActive(true);
        for (int i = 0; i < levels; i++)
        {
            levelsPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void LoadLevel(int num)
    {
        //SceneManager.LoadScene(num);
        StartCoroutine(Fading(num));
    }
    IEnumerator Fading(int level)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => fade.color.a == 1);
        SceneManager.LoadScene(level);
    }

    public void QuitBtn()
    {
        Application.Quit(0);
    }

}
