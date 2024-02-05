using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelect : MonoBehaviour
{
    private List<Animator> CharacterAnim = new List<Animator>();
    //private Button[] selectButtons;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;

        //selectButtons = GetComponentsInChildren<Button>();
        DataManager.instance.OnDataLoadComplete += CreateCharacterList;
        Debug.LogWarning("== CharacterSelect Awake() done");

    }

    private void Start()
    {
        //Debug.Log($"== selectButtons.Length {selectButtons.Length}");
        //for (int i = 1; i < selectButtons.Length; i++)
        //{
        //    Button button = selectButtons[i];
        //    button.GetComponent<Button>().onClick.AddListener(ActiveSelect);
        //}
    }

    //private void OnMouseClick(InputAction.CallbackContext context)
    private void OnMouseClick(InputValue input)
    {
        //Vector2 mousePosition = context.ReadValue<Vector2>();
        //Debug.Log(context);
        // https://www.youtube.com/watch?v=mRkFj8J7y_I

        //Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity);

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

            CharacterAnim.Add(characterInstance.GetComponent<Animator>());
        }

        //Debug.LogWarning("== CreateCharacterList Done");
    }


}
