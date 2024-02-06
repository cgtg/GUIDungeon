using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button confirmBtn;

    public ItemSlot curSelectItemSlot;

    public void InitPopup(ItemSlot itemSlot)
    {
        curSelectItemSlot = itemSlot;

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
        gameObject.SetActive(false);
    }
}
