using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class MainController : SerializedMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private float _updateInterval;
    [SerializeField] private List<GameObject> _lottNumObjList = new List<GameObject>();
    [SerializeField] private List<int> _lotNumLogList = new List<int>();
    [SerializeField] private GameObject _lotNumObjParent;
    [SerializeField] private GameObject _lotNumObjPrefab;
    [SerializeField] private int _maxNum;

    //private int row;
    private int line;

    private void Start()
    {
        //row = 0;
        line = 0;
    }

    [ContextMenu("StartLott"), Button("StartLott", ButtonSizes.Large)]
    public void StartLott()
    {
        Debug.Log("Start Lott");

        StartCoroutine(Animation(3f));
    }

    /// <summary>
    /// 数字のアニメーション
    /// </summary>
    /// <returns></returns>
    private IEnumerator Animation(float time)
    {
        float t = 0;
        while (true)
        {
            t += _updateInterval;

            var num = UnityEngine.Random.Range(1, _maxNum);
            _mainText.text = num.ToString();

            if (t > time )
            {
                AddUnique();

                break;
            }

            yield return new WaitForSeconds(_updateInterval);
        }

        yield break;
    }

    [Button("AddNum", ButtonSizes.Large)]
    public void AddNum(string text)
    {
        var g = Instantiate(_lotNumObjPrefab, _lotNumObjParent.transform);

        var len = _lottNumObjList.Count;
        len--;

        Debug.Log(text);
        _mainText.text = text;

        g.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f * (len % 8) + 210f, (-100f * line) - 300f);
        g.GetComponent<TextMeshProUGUI>().text = text;

        if ((len % 8) == 7) line++;

        _lottNumObjList.Add(g);
        _lotNumLogList.Add(int.Parse(text));
    }

    /// <summary>
    /// Adds the unique.
    /// </summary>
    private void AddUnique()
    {
        if(_lottNumObjList.Count >= _maxNum - 1)
        {
            Debug.Log("Game End");
            return;
        }

        var num = UnityEngine.Random.Range(1, _maxNum);

        if (_lotNumLogList.Contains(num))
        {
            //再抽選
            AddUnique();
        }
        else
        {
            AddNum(num.ToString());
        }

        //try
        //{
        //    foreach (var g in _lottNumObjList)
        //    {
        //        //被ったら
        //        //Debug.Log(g.GetComponent<TextMeshProUGUI>().text + " " + num.ToString());
        //        if (g.GetComponent<TextMeshProUGUI>().text == num.ToString())
        //        {
        //            //再抽選
        //            AddUnique();
        //            return;
        //        }
        //    }
        //}
        //catch (Exception)
        //{

        //}
    }

    [Button("Reset", ButtonSizes.Large)]
    public void Reset()
    {
        foreach (var g in _lottNumObjList)
        {
            Destroy(g);
        }

        _lottNumObjList.Clear();
        _lottNumObjList.Add(new GameObject());

        _lotNumLogList.Clear();

        _mainText.text = "0";
    }
}
