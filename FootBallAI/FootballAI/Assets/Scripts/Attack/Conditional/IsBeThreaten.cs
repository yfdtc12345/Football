using BehaviorDesigner.Runtime.Tasks;
using Gamelogic.Grids.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 是否受到
/// </summary>
public class IsBeThreaten : Conditional
{
    /// <summary>
    /// 敌方队伍
    /// </summary>
    private Team EnemyTeam;

    private FieldPlayer v_Player;
    public override void OnStart()
    {
        EnemyTeam = CreatGrid.Instance.GetBlueTeam();
        v_Player = this.GetComponent<FieldPlayer>();
    }

    public override TaskStatus OnUpdate()
    {
        //如果能够看到球,就返回成功;否则,返回失败



        if (false)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
