
/// <summary>
/// 队伍
/// </summary>
public enum TeamColor
{ 
    Red=0,//从左向右进攻
    Blue=1//从右向左进攻
}
/// <summary>
/// 队形
/// </summary>
public enum Tactics
{ 
    t_352=0,
    t_442=1,
    t_451=2,
    t_433=3
}

public enum PlayerType
{ 
    Defender = 0,
    Attacker =1,
    GoalKeeper = 2,
}

public enum GameState
{ 
    WaitStart = 0,
    Running = 1,
    OutofBorder =2,
    WaitConer = 3,
    WaitSideLineBall = 4,
    WaitGoalKick = 5,
    WaitPenalty = 6,
    WaitFreeKick = 7,
    HalftimeBreak = 9,
    AfterGoal =10,
    EndGame =11,
}




