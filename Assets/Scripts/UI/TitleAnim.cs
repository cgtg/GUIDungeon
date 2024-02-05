using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float moveSpeed = 0.001f;
    public float scaleSpeed = 0.001f;
    public float stopYPosition = 256f;

    //private WaitForSeconds wait = new WaitForSeconds(0.05f);

    private void Start()
    {
        if (text != null)
            StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        Vector3 targetPosition = new Vector3(text.rectTransform.position.x, text.rectTransform.position.y + stopYPosition, text.rectTransform.position.z);
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        float elapsedTime = 0.0f;
        while (text.rectTransform.position.y < text.rectTransform.position.y + stopYPosition)
        {
            // 텍스트 이동 및 스케일 변경
            text.rectTransform.position = Vector3.Lerp(text.rectTransform.position, targetPosition, elapsedTime * moveSpeed);
            text.rectTransform.localScale = Vector3.Lerp(text.rectTransform.localScale, targetScale, elapsedTime * scaleSpeed);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 특정 위치에 도달하면 움직임을 멈춤
        text.rectTransform.position = targetPosition;
        text.rectTransform.localScale = targetScale;
    }
}
