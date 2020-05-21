using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 球员当前是否控球
/// </summary>
public class IsGetNoBall : Conditional
{
    /// <summary>
    /// 当前场上球员类
    /// </summary>
    private FieldPlayer v_Player;
    public override void OnStart()
    {
        v_Player = this.GetComponent<FieldPlayer>();
    }

    public override TaskStatus OnUpdate()
    {
        //如果能够看到球,就返回成功;否则,返回失败
        if (v_Player.m_AttachedBall == false)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}