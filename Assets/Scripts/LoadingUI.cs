using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    string s = ".";
    private float waitingForNextDot = 0;
    private void Update()
    {
        PrintLoading();
    }
    private void PrintLoading()
    {
        waitingForNextDot -= Time.deltaTime;
        if (waitingForNextDot < 0)
        {
            loadingText.text = "Loading " + s;
            if (s.Length >= 3)
                s = ".";
            else
                s += ".";
            waitingForNextDot = .5f;
        }
    }
}
