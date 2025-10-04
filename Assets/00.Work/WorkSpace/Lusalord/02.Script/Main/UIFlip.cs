using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIFlip : MonoBehaviour
{
    public RectTransform visual;
    public bool isRotated;

    public void Flip()
    {
        visual.localEulerAngles = isRotated ? Vector3.zero : new Vector3(0f, 180f, 0f);
        isRotated = !isRotated;
    }
}
