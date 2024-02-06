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
    private string itemDataFilePath = "Assets/Resources/DataTable/GUIDungeonData_ItemTable.csv";

    public Dictionary<uint, CharacterData> characterDataDictionary { get; private set; }
    public Dictionary<uint, Item> itemDataDictionary { get; private set; }
    public Dictionary<string, string> textDataDictionary { get; private set; }

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
        ReadTextData();
        ReadItemData();
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

    private void ReadTextData()
    {
        textDataDictionary = new Dictionary<string, string>();
        string[] lines = File.ReadAllLines(textDataFilePath);

        for (int i = 3; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            string keyString = fields[0];
            string textKR = fields[1];
            textDataDictionary.Add(keyString, textKR);
        }
    }

    public string GetTextData(string key)
    {
        return textDataDictionary[key];
    }

    public string GetCharacterName(uint uid)
    {
        return textDataDictionary[characterDataDictionary[uid].nameAlias];
    }
    public string GetCharacterDesc(uint uid)
    {
        return textDataDictionary[characterDataDictionary[uid].descAlias];
    }
    public string GetCharacterStat(uint uid)
    {
        return $"{characterDataDictionary[uid].HP}\n{characterDataDictionary[uid].MP}\n{characterDataDictionary[uid].atk}\n{characterDataDictionary[uid].def}\n{characterDataDictionary[uid].critical}\n{string.Format("{0:N0}", characterDataDictionary[uid].defaultGold)}";
    }



    private void ReadItemData()
    {
        itemDataDictionary = new Dictionary<uint, Item>();
        string[] lines = File.ReadAllLines(itemDataFilePath);

        for (int i = 3; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            uint uid = uint.Parse(fields[0]);
            string itemType = fields[1];
            string nameAlias = fields[2];
            string descAlias = fields[3];
            int price = int.Parse(fields[4]);
            string prefabFile = fields[5];
            int atk = int.Parse(fields[6]);
            int def = int.Parse(fields[7]);

            Item item = new Item(uid, itemType, nameAlias, descAlias, price, prefabFile, atk, def);
            itemDataDictionary.Add(uid, item);
        }
    }

    internal Item GetItemDataByUID(uint uid)
    {
        if (itemDataDictionary.ContainsKey(uid))
        {
            return itemDataDictionary[uid];
        }

        Debug.LogError("not found for UID: " + uid);
        return null;
    }
}



//[System.Serializable]
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

public enum ItemType
{
    Weapon,
    Shield
}

//[System.Serializable]
public class Item
{
    public uint uid;
    public string itemType;
    public string nameAlias;
    public string descAlias;
    public int price;
    public string prefabFile;
    public int atk;
    public int def;
    public bool isBought;
    public bool isEquiped;

    public Item(uint _uid, string _itemType, string _nameAlias, string _descAlias, int _price, string _prefabFile, int _atk, int _def)
    {
        uid = _uid;
        itemType = _itemType;
        nameAlias = _nameAlias;
        descAlias = _descAlias;
        price = _price;
        prefabFile = _prefabFile;
        atk = _atk;
        def = _def;
        isBought = false;
        isEquiped = false;
    }
}