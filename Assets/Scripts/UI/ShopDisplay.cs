using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    Item[] items;
    [SerializeField] private GameObject shopContent;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        items = DataManager.instance.itemDataDictionary.Values.ToArray();

        foreach (Item item in items)
        {
            if (item.isBought == false) {

                GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Items/ShopItem");
                GameObject itemObj = Instantiate(itemPrefab, shopContent.transform);
                itemObj.GetComponent<ShopItem>().SetSlot(item);
            }
        }
    }
}
