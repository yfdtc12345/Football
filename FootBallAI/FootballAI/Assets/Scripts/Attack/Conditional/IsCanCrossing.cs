using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 判断是否可以传中
/// 首先判断是否在底线，再判断是否有人接应
/// </summary>
public class IsCrossing : Conditional
{
    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        //如果能够看到球,就返回成功;否则,返回失败
        if (true)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
