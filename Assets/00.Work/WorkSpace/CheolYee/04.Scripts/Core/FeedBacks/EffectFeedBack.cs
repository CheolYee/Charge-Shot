using _00.Work.Resource.Scripts.Managers;
using _00.Work.Resource.Scripts.SO;
using _00.Work.Scripts.Managers;
using _00.Work.Scripts.SO;
using _00.Work.WorkSpace.CheolYee._04.Scripts.Core.Effects;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Core.FeedBacks
{
    public class EffectFeedBack : FeedBack
    {
        [SerializeField] private PoolItem effectItem;
        [SerializeField] private float effectDuration;
        [SerializeField] Transform firePos;
        public override void CreateFeedback()
        {
            EffectPlayerSystem effect = PoolManager.Instance.Pop(effectItem.poolName) as EffectPlayerSystem;
            if (effect != null) effect.SetPosAndPlay(firePos.transform.position, effectDuration);
        }

        public override void FinishFeedback()
        {
        }
    }
}