using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    // 파일 리스트
    private string characterDataFilePath = "Assets/Resources/DataTable/GUIDungeonData_Character.csv";
    private string textDataFilePath = "Assets/Resources/DataTable/GUIDungeonData_TextTable.csv";

    public Dictionary<uint, CharacterData> characterDataDictionary { get; private set; }

    public event Action OnDataLoadComplete;

    private void Awake()
    {
        Debug.LogWarning("== DataManager Awake() on");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        characterDataDictionary = ReadCSVFile();
        Debug.LogWarning("== DataManager Awake() off");
    }

    private void Start()
    {
        Debug.LogWarning("== DataManager Start() on");
        OnDataLoadComplete?.Invoke();
    }

    Dictionary<uint, CharacterData> ReadCSVFile()
    {
        Dictionary<uint, CharacterData> characterDataDictionary = new Dictionary<uint, CharacterData>();

        // CSV 파일 읽기
        string[] lines = File.ReadAllLines(characterDataFilePath);

        // TODO : 컬럼 확장성? 타입 구분. 가능하면 제네릭으로 변경

        // 헤더를 무시하고 데이터 읽기
        for (int i = 3; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');

            // 데이터 추출
            uint uid = uint.Parse(fields[0]);
            string characterClass = fields[1];
            string nameAlias = fields[2];
            string descAlias = fields[3];
            int defaultGold = int.Parse(fields[4]);
            int level = int.Parse(fields[5]);
            uint HP = uint.Parse(fields[6]);
            uint MP = uint.Parse(fields[7]);
            uint atk = uint.Parse(fields[8]);
            uint def = uint.Parse(fields[9]);
            uint critical = uint.Parse(fields[10]);
            string prefabFile = fields[11];
            float posX = float.Parse(fields[12]);
            float posY = float.Parse(fields[13]);
            // CharacterData 객체 생성
            CharacterData characterData = new CharacterData(uid, characterClass, nameAlias, descAlias, defaultGold, level, HP, MP, atk, def, critical, prefabFile, posX, posY);

            // 딕셔너리에 추가
            characterDataDictionary.Add(uid, characterData);
        }

        return characterDataDictionary;
    }

    // 외부에서 uid를 통해 데이터에 접근하는 메서드
    public CharacterData GetCharacterDataByUID(uint uid)
    {
        if (characterDataDictionary.ContainsKey(uid))
        {
            return characterDataDictionary[uid];
        }

        Debug.LogError("CharacterData not found for UID: " + uid);
        return null;
    }
}



[System.Serializable]
public class CharacterData
{
    public uint uid;
    public string characterClass;
    public string nameAlias;
    public string descAlias;
    public int defaultGold;
    public int level;
    public uint HP;
    public uint MP;
    public uint atk;
    public uint def;
    public uint critical;
    public string prefabFile;
    public float posX;
    public float posY;

    public CharacterData(uint _uid, string _characterClass, string _nameAlias, string _descAlias, int _defaultGold, int _level, uint _HP, uint _MP, uint _atk, uint _def, uint _critical, string _prefabFile, float _posX, float _posY)
    {
        uid = _uid;
        characterClass = _characterClass;
        nameAlias = _nameAlias;
        descAlias = _descAlias;
        defaultGold = _defaultGold;
        level = _level;
        HP = _HP;
        MP = _MP;
        atk = _atk;
        def = _def;
        critical = _critical;
        prefabFile = _prefabFile;
        posX = _posX;
        posY = _posY;
    }
}