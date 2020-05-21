using BehaviorDesigner.Runtime.Tasks;
using Gamelogic.Grids.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : Action
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
        Vector3 target = CreatGrid.Instance.curPlayerList[GameController.Instance.curSupportIndex].transform.localPosition;
        v_Ball.transform.parent = CreatGrid.Instance.root.transform;
        //如果Ag
        if (v_Ball.transform.localPosition == target)
        {
            Debug.Log("<color=#00ff00>传球成功</color>");
            //1.转身
            //2.球脱离父物体
            //3.传球
            this.v_Player.m_AttachedBall = false;
            CreatGrid.Instance.curPlayerList[3].GetComponent<FieldPlayer>().m_AttachedBall = true;
            GameController.Instance.SetPassNumber();
            GameController.Instance.curSupportIndex = GameController.Instance.rdIndex[GameController.Instance.PassNumber];
            //GameController.Instance.SetPassNumber();
            return TaskStatus.Success;
        }
        else
        {
            //Debug.Log("正在传球");           
            // 返回Running状态        
            v_Ball.transform.localPosition = Vector3.MoveTowards(v_Ball.transform.localPosition, target, 300 * Time.deltaTime);            
            return TaskStatus.Running;
        }
    }
}
