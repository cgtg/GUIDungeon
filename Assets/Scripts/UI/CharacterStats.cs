using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats instance;

    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI critText;

    [SerializeField] private TextMeshProUGUI nickName;
    [SerializeField] private TextMeshProUGUI characterDesc;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;

    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private PopupDisplay popupDisplay;


    // 케릭터 스탯 저장
    CharacterData myPlayer;
    private int addAtk;
    private int addDef;

    private Dictionary<ItemType, uint> equipItems = new Dictionary<ItemType, uint>();
    public Dictionary<uint, Item> myItems = new Dictionary<uint, Item>();

    private void Awake()
    {
        Debug.LogWarning("== CharacterStats Awake() on");
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
    }

    // 스탯 상태 셋
    private void Start()
    {
        Debug.LogWarning("== CharacterInfo Start()");

        uint uid = GameManager.instance.selectedCharacter;
        myPlayer = DataManager.instance.GetCharacterDataByUID(uid);

        SetInitCharacter();
        SetInventory();
        UpdateUI();
    }

    private void SetInventory()
    {
        foreach (Item item in myItems.Values)
        {
            GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Items/ItemSlot");
            GameObject itemObj = Instantiate(itemPrefab, inventoryContent.transform);
            itemObj.GetComponent<ItemSlot>().SetSlot(item, popupDisplay);
        }

        int incCnt = 4 - myItems.Count % 4;
        for (int i = 0; i < incCnt; i++)
        {
            GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Items/ItemSlot");
            GameObject itemObj = Instantiate(itemPrefab, inventoryContent.transform);
            itemObj.GetComponent<ItemSlot>().SetSlot(null, popupDisplay);
        }

        // myItems 길이 4로 나눠서 나머지 빈칸 빈슬롯으로 채우기
    }

    private void SetInitCharacter()
    {
        AddItem(3000005);
        AddItem(3000006);
        UpdateUI();
    }

    public void UpdateUI()
    {
        calcItemStat();

        coinText.text = string.Format("{0:N0}", myPlayer.defaultGold);

        atkText.text = (myPlayer.atk + addAtk).ToString();
        defText.text = (myPlayer.def + addDef).ToString();
        hpText.text = myPlayer.HP.ToString();
        critText.text = myPlayer.critical.ToString();

        nickName.text = $"{GameManager.instance.nickname} ({DataManager.instance.GetTextData(myPlayer.nameAlias)})";
        characterDesc.text = DataManager.instance.GetTextData(myPlayer.descAlias);
        levelText.text = myPlayer.level.ToString();
        //expText.text = myPlayer.
    }


    public void AddItem(uint uid)
    {
        myItems.Add(uid, DataManager.instance.GetItemDataByUID(uid));
        myItems[uid].isBought = true;
        myItems[uid].isEquiped = true;
        UpdateUI();
    }

    public void RemoveItem(uint uid)
    {
        myItems.Remove(uid);
        UpdateUI();
    }

    public void calcItemStat()
    {
        addAtk = 0;
        addDef = 0;
        foreach (Item item in myItems.Values)
        {
            if (item.isEquiped)
            {
                addAtk += item.atk;
                addDef += item.def;
            }
        }
    }

}


