using BehaviorDesigner.Runtime.Tasks;
using Gamelogic.Grids.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 是否可以射门
/// </summary>
public class IsCanShoot : Conditional
{
    public Grid grid;

    private Vector3 rightDoor;

    private Vector3 leftDoor;

    private FieldPlayer v_Player;

    private GameObject v_Ball;

    public override void OnStart()
    {
        //Debug.Log("IsCanshot Start");      
        rightDoor = CreatGrid.Instance.GetPosByXY(Define.m_rightDoor);
        leftDoor = CreatGrid.Instance.GetPosByXY(Define.m_leftDoor);        
        v_Player = this.GetComponent<FieldPlayer>();
        v_Ball = CreatGrid.Instance.m_Ball;
    }

    public override TaskStatus OnUpdate()
    {
        if (v_Player == null)
            return TaskStatus.Failure;

        //if (v_Player.m_AttachedBall == false)
        //{
        //    return TaskStatus.Success;
        //}
        //else
        //{
        //    return TaskStatus.Failure;
        //}

        //判断是否射门条件：
        //1.球员进攻方向与球员到进攻球门中点位置的向量夹角大于45度   
        //2.球员距离球门的距离小于5000
        //3.球在该球员的脚下控制
        //获取距离和夹角       
        float distance = 0;
        float angle = 0;
        if (v_Player.curTeam.m_teamColor == TeamColor.Red)
        {
            Debug.Log("红队进攻状态");
            distance = Vector3.Distance(v_Player.transform.localPosition, rightDoor);
            Vector3 test =  CreatGrid.Instance.GetPosByXY(Grid.Instance.m_rightDoor) - v_Ball.transform.localPosition;
            float dot = Vector3.Dot(v_Ball.transform.up, test.normalized);
            Vector3 cross = Vector3.Cross(v_Ball.transform.up, test.normalized);
            angle = Mathf.Atan2(Vector3.Dot(v_Ball.transform.forward, cross), dot) * Mathf.Rad2Deg;
            //Debug.Log(angle);
        }
        else
        {
            Debug.Log("蓝队进攻状态");
            distance = Vector3.Distance(v_Player.transform.localPosition, leftDoor);         
            Vector3 test = CreatGrid.Instance.GetPosByXY(Grid.Instance.m_leftDoor) - v_Ball.transform.localPosition;
            float dot = Vector3.Dot(v_Ball.transform.up, test.normalized);
            Vector3 cross = Vector3.Cross(v_Ball.transform.up, test.normalized);
            angle = Mathf.Atan2(Vector3.Dot(v_Ball.transform.forward, cross), dot) * Mathf.Rad2Deg;
            //Debug.Log(angle);
        }      
        if (v_Player.m_AttachedBall == true&&distance<5000&&Mathf.Abs(angle)<35 )
        {         
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
