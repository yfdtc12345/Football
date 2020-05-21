using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCanPassForward : Conditional
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
