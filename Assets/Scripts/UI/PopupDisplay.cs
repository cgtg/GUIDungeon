using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button confirmBtn;

    [HideInInspector] public ItemSlot curSelectItemSlot;

    [SerializeField] private GameObject icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemStat;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void InitPopup(ItemSlot itemSlot)
    {
        curSelectItemSlot = itemSlot;

        GameObject itemPrefab = Resources.Load<GameObject>(curSelectItemSlot.curItem.prefabFile);
        GameObject itemInstance = Instantiate(itemPrefab, icon.transform);

        itemName.text = DataManager.instance.GetTextData(curSelectItemSlot.curItem.nameAlias);
        itemDesc.text = DataManager.instance.GetTextData(curSelectItemSlot.curItem.descAlias);
        if (curSelectItemSlot.curItem.atk > 0)
        {
            itemStat.text = $"ATK +{curSelectItemSlot.curItem.atk}";
        }
        else if (curSelectItemSlot.curItem.def > 0)
        {
            itemStat.text = $"ATK +{curSelectItemSlot.curItem.atk}";
        }

        if (curSelectItemSlot.curItem.isEquiped)
        {
            text.text = "장착을 해지 하시겠습니까?";
        }
        else
        {
            text.text = "장착 하시겠습니까?";
        }

        gameObject.SetActive(true);
    }

    public void ToggleItemEquip()
    {
        curSelectItemSlot.curItem.isEquiped = !curSelectItemSlot.curItem.isEquiped;
        curSelectItemSlot.ChangeEquipState(curSelectItemSlot.curItem.isEquiped);
        CharacterStats.instance.UpdateUI();
        gameObject.SetActive(false);
    }
}
