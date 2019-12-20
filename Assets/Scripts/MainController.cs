using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class MainController : SerializedMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mainText;

    [ContextMenu("StartLott"), Button("StartLott", ButtonSizes.Large)]
    public void StartLott()
    {
        Debug.Log("ok");
    }

    /// <summary>
    /// 数字のアニメーション
    /// </summary>
    /// <returns></returns>
    private IEnumerator Animation(int time)
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            if (t > time)
                break;
        }

        yield break;
    }
}
