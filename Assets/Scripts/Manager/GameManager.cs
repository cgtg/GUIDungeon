using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public string nickname;
    [HideInInspector] public uint selectedCharacter;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Debug.LogWarning("== GameManager Awake() off");

        // === Set Default ===
        nickname = "test";
        selectedCharacter = 1000002;
    }

    public void SetGame(string name, uint uid)
    {
        nickname = name;
        selectedCharacter = uid;
    }
}
