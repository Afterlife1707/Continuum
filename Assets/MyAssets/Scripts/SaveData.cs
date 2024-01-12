[System.Serializable]
public class SaveData 
{
    public int level=1;
    public bool isMusic=true, isSound=true;
    public float sens=1;
    
    public SaveData(DataHandler dataHandler)
    {
        level = dataHandler.level;
        isMusic = dataHandler.isMusic;
        isSound = dataHandler.isSound;
        sens = dataHandler.sens;
    }
}
