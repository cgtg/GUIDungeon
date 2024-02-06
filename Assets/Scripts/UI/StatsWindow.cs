using UnityEngine;

public class StatsWindow : MonoBehaviour
{
    private void Awake()
    {
        //statsWindow = this.gameObject;
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
