//using Gamelogic.Grids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase:MonoBehaviour
{
    #region 属性
    /// <summary>
    /// 球员姓名
    /// </summary>
    [HideInInspector]  public string m_PlayerName;
    /// <summary>
    /// 号码
    /// </summary>
    [HideInInspector] public int m_Number;
    /// <summary>
    /// 球员类型
    /// </summary>
    [HideInInspector] public PlayerType Type = PlayerType.Defender;
    /// <summary>
    /// 跑动速度
    /// </summary>
    [HideInInspector] private int speed = 0;

    [HideInInspector] private GameObject v_Ball;

    private int Strength = 1;
    //public int Control = 1;

    //public RectPoint curPos;
    /// <summary>
    /// 当前队伍
    /// </summary>
    public Team curTeam;

    private float lastTimeTicked = 0;

   

    //public RectPoint homePos;

    #endregion

    private void Start()
    {
        
    }


    public void AddBall(GameObject pBall)
    {
        v_Ball = pBall;
    }

    public Ball GetBall()
    {
        return v_Ball.GetComponent<Ball>();
    }
}
