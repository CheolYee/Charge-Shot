using _00.Work.WorkSpace.CheolYee._04.Scripts.Managers;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Turret;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.UI
{
    public class CapacitorTextUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI capacitorText;
        
        private LaserTurret _laserTurret;
        private bool _playing;

        private void Awake()
        {
            capacitorText.gameObject.SetActive(false);
        }

        private void Start()
        {
            _laserTurret = GameManager.Instance.TargetTransform.GetComponent<LaserTurret>();
            _laserTurret.NoCapacitor += LaserNotYet;
        }

        private void LaserNotYet()
        {
            if (_playing) return;
            
            _playing = true;
            capacitorText.gameObject.SetActive(true);
            capacitorText.text = "축전기가 성공적으로 완료되지 않았습니다.";
            
            capacitorText.DOKill();

            //시작 상태 초기화
            Color startColor = capacitorText.color;
            startColor.a = 1f;
            capacitorText.color = startColor;

            RectTransform textRect = capacitorText.GetComponent<RectTransform>();
            Vector3 originalPos = textRect.anchoredPosition;

            //위로 올라가면서 페이드아웃
            Sequence seq = DOTween.Sequence();

            seq.Append(textRect.DOAnchorPosY(originalPos.y + 50f, 1.0f).SetEase(Ease.OutCubic)); // 위로 50픽셀 이동
            seq.Join(capacitorText.DOFade(0f, 1.0f)); // 동시에 페이드 아웃
            seq.OnComplete(() =>
            {
                //완료 후 원래 위치로 복귀
                textRect.anchoredPosition = originalPos;
                Color c = capacitorText.color;
                c.a = 1f;
                capacitorText.color = c;

                capacitorText.text = "";
                capacitorText.gameObject.SetActive(false);
                _playing = false;
            });
        }
    }
}
