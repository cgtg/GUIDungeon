using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Item curItem;

    [SerializeField] private GameObject icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemStat;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject buyBtn;

    public void SetSlot(Item _item)
    {
        curItem = _item;
        GameObject itemPrefab = Resources.Load<GameObject>(curItem.prefabFile);
        GameObject itemInstance = Instantiate(itemPrefab, icon.transform);

        itemName.text = DataManager.instance.GetTextData(curItem.nameAlias);
        itemDesc.text = DataManager.instance.GetTextData(curItem.descAlias);
        if (curItem.atk > 0)
        {
            itemStat.text = $"ATK\n+{curItem.atk}";
        }
        else if (curItem.def > 0)
        {
            itemStat.text = $"DEF\n+{curItem.def}";
        }
        goldText.text = curItem.price.ToString();
    }

    public void BuyItem()
    {
        bool buy = CharacterStats.instance.BuyItem(curItem.uid);
        if (buy)
        {
            gameObject.SetActive(false);
        }
    }
}
