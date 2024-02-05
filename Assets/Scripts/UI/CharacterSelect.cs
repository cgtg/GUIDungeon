using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] characterInfoPanal;
    public Button startGame;

    private List<Animator> CharacterAnim = new List<Animator>();
    private Camera camera;

    private uint selectedUID = 0;

    private void Awake()
    {
        camera = Camera.main;
        //selectButtons = GetComponentsInChildren<Button>();
        DataManager.instance.OnDataLoadComplete += CreateCharacterList;
        Debug.LogWarning("== CharacterSelect Awake() done");

        startGame.onClick.AddListener(StartGame);
    }

    private void OnMouseClick(InputValue input)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (hit.collider != null)
        {
            // 충돌한 대상 가져오기
            GameObject collidedObject = hit.collider.gameObject;
            foreach (Animator obj in CharacterAnim)
            {
                obj.SetBool("Walking", false);
            }
            collidedObject.GetComponent<Animator>().SetBool("Walking", true);

            uint uid = collidedObject.GetComponent<CharacterInfo>().uid;
            if (characterInfoPanal.Length > 0)
            {
                characterInfoPanal[0].GetComponent<TMP_InputField>().text = DataManager.instance.GetCharacterName(uid);
                characterInfoPanal[1].GetComponent<TextMeshProUGUI>().text = DataManager.instance.GetCharacterDesc(uid);
                characterInfoPanal[2].GetComponent<TextMeshProUGUI>().text = $"HP :\nMP :\nATK :\nDEF :\nCRIT :\nGold :";
                characterInfoPanal[3].GetComponent<TextMeshProUGUI>().text = DataManager.instance.GetCharacterStat(uid);
            }
            selectedUID = uid;
        }
    }

    private void CreateCharacterList()
    {
        GameObject characterListObject = new GameObject("CharacterList");

        foreach (uint uid in DataManager.instance.characterDataDictionary.Keys)
        {
            //Debug.Log(uid);
            CharacterData characterData = DataManager.instance.characterDataDictionary[uid];
            GameObject characterPrefab = Resources.Load<GameObject>(characterData.prefabFile);

            GameObject characterInstance = Instantiate(characterPrefab, characterListObject.transform);
            characterInstance.transform.localPosition = new Vector3(characterData.posX, characterData.posY, 0);
            characterInstance.name = DataManager.instance.characterDataDictionary[uid].characterClass.ToString();
            characterInstance.GetComponent<CharacterInfo>().uid = uid;
            CharacterAnim.Add(characterInstance.GetComponent<Animator>());
        }

        //Debug.LogWarning("== CreateCharacterList Done");
    }

    private void StartGame()
    {
        Debug.LogWarning("== StartGame");
        if (selectedUID > 0)
        {
            GameManager.instance.SetGame(characterInfoPanal[0].GetComponent<TMP_InputField>().text, selectedUID);
            SceneManager.LoadScene(1);
        }
    }

}
