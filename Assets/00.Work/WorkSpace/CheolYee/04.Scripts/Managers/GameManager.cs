using _00.Work.Scripts.Managers;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField] public Transform TargetTransform { get; private set; }
    }
}