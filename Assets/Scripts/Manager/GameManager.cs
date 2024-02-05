using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private string nickname;
    private uint selectedCharacter;

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

        nickname = "test";
        selectedCharacter = 1000000;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGame(string name, uint uid)
    {
        nickname = name;
        selectedCharacter = uid;
    }


}
