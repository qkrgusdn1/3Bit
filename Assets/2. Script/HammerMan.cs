using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMan : Player
{
    public override void Update()
    {
        if (!photonView.IsMine)
            return;
        if (currentSkill == Skill.Stun)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                animator.SetTrigger("EndStun");
                currentSkill = Skill.Default;
                stunTimer = maxStunTimer;
                moveSpeed = maxMoveSpeed;
                jumpForce = maxJumpForce;
                rb.velocity = Vector3.zero;

            }
            else
            {
                moveSpeed = 0;
                jumpForce = 0;
            }
        }
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC("RpcAttack", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RpcAttack()
    {
        animator.Play("Attack");
    }
    public override void ApplySkill(Skill skill)
    {
        currentSkill = skill;
        if (currentSkill == Skill.Default)
        {

        }
        else if (currentSkill == Skill.Stun)
        {
            animator.Play("Stun");
        }
    }
}
