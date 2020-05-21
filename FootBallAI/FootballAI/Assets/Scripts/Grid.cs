using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{


    private static Grid _instance = null;

    public static Grid Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Grid();
            }
            return _instance;
        }
    }

    
    /// <summary>
    /// 右边门的中心点
    /// </summary>
    [HideInInspector] public Vector2 m_rightDoor = new Vector2(25,107);

    /// <summary>
    /// 左侧门的中心点
    /// </summary>
    [HideInInspector] public Vector2 m_leftDoor = new Vector2(25, 3);


    /// <summary>
    /// 球员和球的Z轴坐标
    /// </summary>
    [HideInInspector]  public int m_Zoffset = -1;

    /// <summary>
    /// 门的宽度
    /// </summary>
    [HideInInspector] public int m_DoorWidth = 7;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
