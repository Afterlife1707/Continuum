using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public int level = 1;
    public bool isMusic=true, isSound=true;
    public float sens;
    public static DataHandler instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        SaveData data = SaveSystem.Load();
        if (data != null)
        {
            level = data.level;
            MainMenuController.instance.EnableLevels(level);
            Debug.Log("loaded with lvl " + level);
        }
        else
        {
            Debug.Log("new game");
            level = 1;
        }
        SaveDataHandler();
    }

    public void SaveDataHandler()
    {
        SaveSystem.Save(this);
    }
    public SaveData LoadDataHandler() //required for loading settings
    {
        SaveData data = SaveSystem.Load();
        return data;
    }
}
