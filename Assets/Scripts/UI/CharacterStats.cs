using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
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
    private Dictionary<ItemType, uint> equipItems = new Dictionary<ItemType, uint>();
    public Dictionary<uint, Item> myItems = new Dictionary<uint, Item>();

    private void Awake()
    {
        Debug.LogWarning("== CharacterInfo Awake()");
    }

    // 스탯 상태 셋
    private void Start()
    {
        Debug.LogWarning("== CharacterInfo Start()");

        uint uid = GameManager.instance.selectedCharacter;
        myPlayer = DataManager.instance.GetCharacterDataByUID(uid);

        SetInitCharacter();
        SetInventory();
        SetUI();
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
        myItems.Add(3000005, DataManager.instance.GetItemDataByUID(3000005));
        myItems[3000005].isBought = true;
        myItems[3000005].isEquiped = true;

        myItems.Add(3000006, DataManager.instance.GetItemDataByUID(3000006));
        myItems[3000006].isBought = true;
        myItems[3000006].isEquiped = true;
    }

    private void SetUI()
    {
        coinText.text = string.Format("{0:N0}", myPlayer.defaultGold);

        atkText.text = myPlayer.atk.ToString();
        defText.text = myPlayer.def.ToString();
        hpText.text = myPlayer.HP.ToString();
        critText.text = myPlayer.critical.ToString();

        nickName.text = $"{GameManager.instance.nickname} ({DataManager.instance.GetTextData(myPlayer.nameAlias)})";
        characterDesc.text = DataManager.instance.GetTextData(myPlayer.descAlias);
        levelText.text = myPlayer.level.ToString();
        //expText.text = myPlayer.
    }
}


