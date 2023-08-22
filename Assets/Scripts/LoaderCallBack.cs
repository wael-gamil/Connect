using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        Loader.LoaderCallBack();
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
        }
    }
}
