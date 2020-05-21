using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class PlayerState 
{
    protected PlayerBase player;

    public bool LockedInForPreparation;//准备锁定状态
    public bool NeedsBallOnEntering;//需要球进入
    public bool LosesBallOnCompletion;//完成时丢球
    public bool DirectlyInvolvedInAction;//直接参与活动
    

}
    