using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Gamelogic.Grids.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Support : Action
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
        if (this.v_Player.m_AttachedBall == true)
        {
            Debug.Log("接应成功");        
            return TaskStatus.Success;
        }
        else
        {
           
            Vector3 test = this.transform.localPosition - v_Ball.transform.localPosition;
            float dot = Vector3.Dot(v_Ball.transform.up, test.normalized);
            Vector3 cross = Vector3.Cross(v_Ball.transform.up, test.normalized);
            float angle = Mathf.Atan2(Vector3.Dot(v_Ball.transform.forward, cross), dot) * Mathf.Rad2Deg;

            //Debug.Log("正在接应  "+angle);
            this.v_Player.transform. RotateAroundLocal(this.transform.forward , angle);           
            return TaskStatus.Running;
        }
    }

}
