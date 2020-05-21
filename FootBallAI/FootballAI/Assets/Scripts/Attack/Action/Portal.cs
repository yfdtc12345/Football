using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Action
{
    public override void OnStart()
    {
      
    }

    public override TaskStatus OnUpdate()
    {
     
        if (false)
        {            
            return TaskStatus.Success;
        }
        else
        {
            //Debug.Log("正在巡逻");
            return TaskStatus.Running;
        }
    }
}
