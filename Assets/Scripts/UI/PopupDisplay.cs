using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button confirmBtn;

    public Item curSelectItem;

    public void InitPopup(Item item)
    {
        curSelectItem = item;

        if (item.isEquiped)
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
        curSelectItem.isEquiped = !curSelectItem.isEquiped;
        gameObject.SetActive(false);
    }
}
