using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public TeamColor m_teamColor;
    public List<GameObject> m_Players;
    private GoalKeeper m_GoalKeeper = new GoalKeeper();
    private Tactics m_CurTactics;

    private PlayerBase m_pControllingPlayer;
    private PlayerBase m_pSupportingPlayer;
    private PlayerBase m_pReceivingPlayer;
    private PlayerBase m_pPlayerClosestToBall;

    public Team(TeamColor pColor)
    {
        m_teamColor = pColor;      
    }

    public void SetPlayer(List<GameObject> pPlayer)
    {
        m_Players = pPlayer;
    }

    /// <summary>
    /// 创建球员
    /// </summary>
    public void CreatePlayers()
    { 
    
    }

    private void CalculateClosestPlayerToBall()
    { 
    
    
    }

    public void CalculateClosestTwoPlayerToBall(Transform pTarget)
    { 
        
    
    }

    public List<GameObject> GetPlayer()
    {
        return m_Players;
    }


}
