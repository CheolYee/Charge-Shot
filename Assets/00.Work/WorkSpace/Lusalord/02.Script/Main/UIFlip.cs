using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIFlip : MonoBehaviour
{
    public bool isRotated = false;
    [SerializeField] private RectTransform visual;

    public void OnButtonClicked()
    {
        isRotated = !isRotated;
        float targetY = isRotated ? 180f : 0f;
        visual.localRotation = Quaternion.Euler(0f, targetY, 0f);
    }
}
