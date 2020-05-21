using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNotSupport : Conditional
{
    /// <summary>
    /// Agent
    /// </summary>
    private FieldPlayer v_Player;
    /// <summary>
    /// 足球
    /// </summary>
    private Ball v_Ball;

    public override void OnStart()
    {
        v_Player = GetComponent<FieldPlayer>();
        v_Ball = v_Player.GetBall().GetComponent<Ball>();
    }


    public override TaskStatus OnUpdate()
    {
        //如果能够看到球,就返回成功;否则,返回失败
        if (GameController.Instance.curSupportIndex != v_Player.m_Number)
        {
            Debug.Log("接应号码：    " + v_Player.m_Number);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
