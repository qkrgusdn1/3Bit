using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ServerJoinPanel : MonoBehaviour
{
    public TMP_Text loadingText;
    void OnEnable()
    {
        StartCoroutine(CoLoadingText());
    }

    IEnumerator CoLoadingText()
    {
        while (true)
        {
            loadingText.text = "서버 접속 중.";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "서버 접속 중..";
            yield return new WaitForSeconds(0.5f);
            loadingText.text = "서버 접속 중...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
