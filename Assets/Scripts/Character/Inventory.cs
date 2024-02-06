using UnityEngine;

public class Inventory : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }


    public void Toggle()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            //onCloseInventory?.Invoke();
        }
        else
        {
            gameObject.SetActive(true);
            //onOpenInventory?.Invoke();
        }

    }

}
