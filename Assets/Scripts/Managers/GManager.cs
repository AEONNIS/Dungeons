using UnityEngine;
using System.IO;

public class GManager : MonoBehaviour
{
  public GameObject pauseMenuGO;
  public SaveLoad save;
  public SaveLoad load;
  public SaveLoad resetInventory;

  private static GManager instance = null;
  public static GManager Instance
  {
    get { return instance; }
    private set { }
  }

  private bool gamePaused = false;
  public bool GamePaused
  {
    get { return gamePaused; }
    private set { }
  }

  private string pathSave;

  private string fNameEnv1 = string.Format("{0}.eon", EnvironmentId.Stone1);
  private string fNameEnv2 = string.Format("{0}.eon", EnvironmentId.Stone2);
  private string fNameEnv3 = string.Format("{0}.eon", EnvironmentId.Ground1);
  private string fNameEnv4 = string.Format("{0}.eon", EnvironmentId.Ice1);
  private string fNameEnv5 = string.Format("{0}.eon", EnvironmentId.Tree1);
  private string fNamePlayer = "Player.eon";
  private string fNameItem1 = string.Format("{0}.eon", ItemId.Pick1);
  private string fNameItem2 = string.Format("{0}.eon", ItemId.Axe1);
  private string fNameItem3 = string.Format("{0}.eon", ItemId.Shovel1);
  private string fNameDoor0 = string.Format("{0}.eon", DoorId.Door0);

  private string sFileEnv1;
  private string sFileEnv2;
  private string sFileEnv3;
  private string sFileEnv4;
  private string sFileEnv5;
  private string sFilePlayer;
  private string sFileItem1;
  private string sFileItem2;
  private string sFileItem3;
  private string sFileDoor0;

  private int env1Index = 0, env2Index = 0, env3Index = 0, env4Index = 0, env5Index = 0,
              playerIndex = 0, item1Index = 0, item2Index = 0, item3Index = 0, door0Index = 0;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
      DestroyImmediate(this);
  }

  private void Start()
  {
    pathSave = Application.persistentDataPath + "/Saves/Save1";
  }

  public void AddObjectToSave(EnvironmentId envId, string envToSave)
  {
    switch (envId)
    {
      case EnvironmentId.Stone1:
        sFileEnv1 += envToSave;
        break;
      case EnvironmentId.Stone2:
        sFileEnv2 += envToSave;
        break;
      case EnvironmentId.Ground1:
        sFileEnv3 += envToSave;
        break;
      case EnvironmentId.Ice1:
        sFileEnv4 += envToSave;
        break;
      case EnvironmentId.Tree1:
        sFileEnv5 += envToSave;
        break;
    }
  }
  public void AddObjectToSave(ItemId itemId, string itemToSave)
  {
    switch (itemId)
    {
      case ItemId.Pick1:
        sFileItem1 += itemToSave;
        break;
      case ItemId.Axe1:
        sFileItem2 += itemToSave;
        break;
      case ItemId.Shovel1:
        sFileItem3 += itemToSave;
        break;
    }
  }
  public void AddObjectToSave(string playerToSave)
  {
    sFilePlayer += playerToSave;
  }
  public void AddObjectToSave(DoorId doorId, string doorToSave)
  {
    switch (doorId)
    {
      case DoorId.Door0:
        sFileDoor0 += doorToSave;
        break;
    }
  }

  public void SaveGame()
  {
    if (!Directory.Exists(pathSave))
      Directory.CreateDirectory(pathSave);

    ResetSFiles();
    if (save != null)
      save();

    File.WriteAllText(pathSave + "/" + fNameEnv1, sFileEnv1);
    File.WriteAllText(pathSave + "/" + fNameEnv2, sFileEnv2);
    File.WriteAllText(pathSave + "/" + fNameEnv3, sFileEnv3);
    File.WriteAllText(pathSave + "/" + fNameEnv4, sFileEnv4);
    File.WriteAllText(pathSave + "/" + fNameEnv5, sFileEnv5);

    File.WriteAllText(pathSave + "/" + fNamePlayer, sFilePlayer);

    File.WriteAllText(pathSave + "/" + fNameItem1, sFileItem1);
    File.WriteAllText(pathSave + "/" + fNameItem2, sFileItem2);
    File.WriteAllText(pathSave + "/" + fNameItem3, sFileItem3);

    File.WriteAllText(pathSave + "/" + fNameDoor0, sFileDoor0);
  }

  public void LoadGame()
  {
    ReadSaveFiles();
    ResetIndexes();
    if (resetInventory != null)
      resetInventory();
    if (load != null)
      load();
  }

  /// <summary>
  /// Читаем информацию из всех файлов сохранения, сохраняя ее в соответствующих полях этого класса.
  /// </summary>
  private void ReadSaveFiles()
  {
    sFileEnv1 = File.ReadAllText(pathSave + "/" + fNameEnv1);
    sFileEnv2 = File.ReadAllText(pathSave + "/" + fNameEnv2);
    sFileEnv3 = File.ReadAllText(pathSave + "/" + fNameEnv3);
    sFileEnv4 = File.ReadAllText(pathSave + "/" + fNameEnv4);
    sFileEnv5 = File.ReadAllText(pathSave + "/" + fNameEnv5);

    sFilePlayer = File.ReadAllText(pathSave + "/" + fNamePlayer);

    sFileItem1 = File.ReadAllText(pathSave + "/" + fNameItem1);
    sFileItem2 = File.ReadAllText(pathSave + "/" + fNameItem2);
    sFileItem3 = File.ReadAllText(pathSave + "/" + fNameItem3);

    sFileDoor0 = File.ReadAllText(pathSave + "/" + fNameDoor0);
  }

  private void ResetSFiles()
  {
    sFileEnv1 = "";
    sFileEnv2 = "";
    sFileEnv3 = "";
    sFileEnv4 = "";
    sFileEnv5 = "";
    sFilePlayer = "";
    sFileItem1 = "";
    sFileItem2 = "";
    sFileItem3 = "";
    sFileDoor0 = "";
  }

  private void ResetIndexes()
  {
    env1Index = 0;
    env2Index = 0;
    env3Index = 0;
    env4Index = 0;
    env5Index = 0;
    playerIndex = 0;
    item1Index = 0;
    item2Index = 0;
    item3Index = 0;
    door0Index = 0;
  }

  public string ReadNextObjectToLoad(EnvironmentId envId)
  {
    string sEnv = "";
    switch (envId)
    {
      case EnvironmentId.Stone1:
        sEnv = FindNextObject(sFileEnv1, ref env1Index);
        break;
      case EnvironmentId.Stone2:
        sEnv = FindNextObject(sFileEnv2, ref env2Index);
        break;
      case EnvironmentId.Ground1:
        sEnv = FindNextObject(sFileEnv3, ref env3Index);
        break;
      case EnvironmentId.Ice1:
        sEnv = FindNextObject(sFileEnv4, ref env4Index);
        break;
      case EnvironmentId.Tree1:
        sEnv = FindNextObject(sFileEnv5, ref env5Index);
        break;
    }
    return sEnv;
  }
  public string ReadNextObjectToLoad(ItemId itemId)
  {
    string sItem = "";
    switch (itemId)
    {
      case ItemId.Pick1:
        sItem = FindNextObject(sFileItem1, ref item1Index);
        break;
      case ItemId.Axe1:
        sItem = FindNextObject(sFileItem2, ref item2Index);
        break;
      case ItemId.Shovel1:
        sItem = FindNextObject(sFileItem3, ref item3Index);
        break;
    }
    return sItem;
  }
  public string ReadNextObjectToLoad(DoorId doorId)
  {
    string sDoor = "";
    switch (doorId)
    {
      case DoorId.Door0:
        sDoor = FindNextObject(sFileDoor0, ref door0Index);
        break;
    }
    return sDoor;
  }
  public string ReadNextObjectToLoad()
  {
    return FindNextObject(sFilePlayer, ref playerIndex);
  }

  private string FindNextObject(string saveFile, ref int startIndex, char end = '}')
  {
    int endIndex = saveFile.IndexOf(end, startIndex) + 1;

    if (endIndex == 0) // Переход на начало. Можно убрать, если не нужно.
    {
      startIndex = 0;
      endIndex = saveFile.IndexOf(end, startIndex) + 1;
      Debug.Log("Достигнут конец файла. Возвращен первый объект.");
    } // Переход на начало. Можно убрать, если не нужно.

    string sObject = saveFile.Substring(startIndex, endIndex - startIndex);
    startIndex = ++endIndex;
    return sObject;
  }

  public void PauseGame()
  {
    gamePaused = true;
    Time.timeScale = 0;
    pauseMenuGO.SetActive(true);
  }

  public void ResumeGame()
  {
    gamePaused = false;
    Time.timeScale = 1;
    pauseMenuGO.SetActive(false);
  }

  public void ExitGame()
  {
    Debug.Log("QuitGame..."); // DEBUG
    Application.Quit();
  }

  public static string FindStringParameterInObject(string sObject, string nameParam, string before = "\"", string after = "\"", string endString = "\n")
  {
    int startIndex, endIndex;
    string temp = nameParam + " = " + before;
    startIndex = sObject.IndexOf(temp) + temp.Length;
    temp = after + endString;
    endIndex = sObject.IndexOf(temp, startIndex);
    return sObject.Substring(startIndex, endIndex - startIndex);
  }

  public static float FindFloatParamInSObject(string sObject, string nameFloatParam)
  {
    int i1, i2;
    string temp = nameFloatParam + " = ";
    i1 = sObject.IndexOf(temp) + temp.Length;
    i2 = sObject.IndexOf("\n", i1);
    float fValue;
    float.TryParse(sObject.Substring(i1, i2 - i1), out fValue);
    return fValue;
  }

  public static bool FindBoolParamInSObject(string sObject, string nameParam)
  {
    int i1, i2;
    string boolParam = nameParam + " = ";
    i1 = sObject.IndexOf(boolParam) + boolParam.Length;
    i2 = sObject.IndexOf("\n", i1);
    boolParam = sObject.Substring(i1, i2 - i1);
    return bool.Parse(boolParam);
  }

  public static Vector2 FindVector2InObject(string sObject, string nameVector2)
  {
    Vector2 vect2Param = new Vector2(0, 0);
    int i1, i2;
    string temp = nameVector2 + " = (";

    i1 = sObject.IndexOf(temp) + temp.Length;
    i2 = sObject.IndexOf(',', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out vect2Param.x);
    i1 = i2 + 1;
    i2 = sObject.IndexOf(')', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out vect2Param.y);

    return vect2Param;
  }

  public static Vector3 FindVector3InObject(string sObject, string nameVector3)
  {
    Vector3 vect3 = new Vector3(0, 0, 0);
    int i1, i2;
    string temp = nameVector3 + " = (";
    i1 = sObject.IndexOf(temp) + temp.Length;

    i2 = sObject.IndexOf(',', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out vect3.x);

    i1 = i2 + 1;
    i2 = sObject.IndexOf(',', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out vect3.y);

    i1 = i2 + 1;
    i2 = sObject.IndexOf(')', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out vect3.z);

    return vect3;
  }

  public static Quaternion FindQuaternionInSObject(string sObject, string nameQutarnion)
  {
    Quaternion quatParam = new Quaternion();
    int i1, i2;
    string temp = nameQutarnion + " = (";

    i1 = sObject.IndexOf(temp) + temp.Length;
    i2 = sObject.IndexOf(',', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out quatParam.x);
    i1 = i2 + 1;
    i2 = sObject.IndexOf(',', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out quatParam.y);
    i1 = i2 + 1;
    i2 = sObject.IndexOf(',', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out quatParam.z);
    i1 = i2 + 1;
    i2 = sObject.IndexOf(')', i1);
    float.TryParse(sObject.Substring(i1, i2 - i1), out quatParam.w);

    return quatParam;
  }
}

public delegate void SaveLoad();