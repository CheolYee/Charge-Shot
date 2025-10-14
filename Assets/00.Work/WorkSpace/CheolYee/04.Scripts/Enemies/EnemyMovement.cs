using _00.Work.WorkSpace.CheolYee._04.Scripts.Agents;
using UnityEngine;

namespace _00.Work.WorkSpace.CheolYee._04.Scripts.Enemies
{
    public class EnemyMovement : AgentMovement
    {
        public override void Initialize(float moveSpeed, float jumpForce)
        {
            MoveSpeed = moveSpeed;
            JumpForce = jumpForce;
        }
    }
}