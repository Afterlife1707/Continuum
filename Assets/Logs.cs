using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logs : MonoBehaviour
{ 
    //#if !UNITY_EDITOR
    static string myLog = "";
    private string output;
    private string stack;
    static Logs instance;
    [SerializeField] TMPro.TMP_Text logs;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        else
            instance = this;
        DontDestroyOnLoad(this);
    }
    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 5000)
        {
            myLog = myLog.Substring(0, 4000);
        }
    }

    private void Update()
    {
        logs.text += "\n" + myLog;
    }

    //void OnGUI()
    //{
    //    //if (!Application.isEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
    //    {
    //        myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), myLog);
    //    }
    //}
    //#endif
}
