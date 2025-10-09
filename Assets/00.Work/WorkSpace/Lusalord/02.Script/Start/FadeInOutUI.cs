using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FadeInOutUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform panelTrs; //움직일 패널
    private readonly float _duration = 0.5f; // 패널이 움직이는 속도
    [SerializeField] private float pos; // 마우스를 대고 있지 않을 때 패널의 위치


    private void Start() // 패널의 초기 위치 조정
    {
        var vector2 = panelTrs.anchoredPosition;
        vector2.x = pos; // 패널의 x값의 초기 위치를 pos의 값으로 조절함
        panelTrs.anchoredPosition = vector2;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelTrs.DOAnchorPosX(0, _duration); // 두트윈을 활용하여 패널의 위치를 0으로 이동함
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelTrs.DOAnchorPosX(pos, _duration); // 두트윈을 활용하여 패널의 위치를 화면에서 안 보이도록 이동함
    }
}
