using BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具类
/// </summary>
public static class Util
{
    public static GameObject CalculateClosestPlayerToBall(Transform pTarget,Team pTeam)
    {
        List<GameObject> playerLs = pTeam.GetPlayer();
        if (playerLs == null)
        {
            Debug.Log("球队列表为空");
            return null;
        }
        float distance = 0;
        int index = 0;
        for (int i = 0; i < playerLs.Count; i++)
        {
            float temp = Vector3.Distance(pTarget.localPosition, playerLs[i].transform.localPosition);
            if (distance == 0 || distance > temp)
            {
                distance = temp;
                index = i;
            }
        }
        return playerLs[index];
    }

}
