using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ItemSlot : MonoBehaviour
{
    public Item curItem;

    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject equip;

    private PopupDisplay popupDisplay;

    public void SetSlot(Item _item, PopupDisplay _popupDisplay)
    {
        if (_item == null)
        {
            ChangeEquipState(false);
            return;
        }

        popupDisplay = _popupDisplay;

        curItem = _item;
        GameObject itemPrefab = Resources.Load<GameObject>(curItem.prefabFile);
        GameObject itemInstance = Instantiate(itemPrefab, icon.transform);

        ChangeEquipState(curItem.isEquiped);
    }

    public void ChangeEquipState(bool isEquiped)
    {
        equip.SetActive(isEquiped);
    }

    public void Popup()
    {

        if (popupDisplay != null)
        {
            //Debug.Log("Popup()");
            popupDisplay.InitPopup(this);
        }
    }
}
