using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Action
{
    public SharedGameObject Ball;

    private Transform PlayerTransform;

    public override void OnStart()
    {
        PlayerTransform = GetComponent<Transform>();
        //Ball = mAgent.GetBall().GetComponent<Ball>();
    }

    public override TaskStatus OnUpdate()
    {

        if (true)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
      
    }

}
