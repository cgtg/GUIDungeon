using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    private Button button;
    public int index;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SelectPrefab);
        }
    }

    private void SelectPrefab()
    {
        Debug.Log(gameObject.name);
        //GameManager.instance.
    }
}
