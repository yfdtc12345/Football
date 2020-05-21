using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameController _instance = null;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameController)GameObject.FindObjectOfType(typeof(GameController));
            }
            return _instance;
        }
    }


    public int[] rdIndex = { 0,1,2,3,4,5,6,7,8,9 };

    private GameState curGameState = GameState.WaitStart;

    public int curSupportIndex = -1;

    public int PassNumber = 5;

    void Awake()
    {

    }

    void Start()
    {
        GameController.Instance.curSupportIndex = GameController.Instance.rdIndex[GameController.Instance.PassNumber];
    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameState GetGameState()
    {
        return curGameState;
    }

    public void SetGameState(GameState pGameState)
    {
        curGameState = pGameState;
    }
    /// <summary>
    ///  设置队伍的当前接球人
    /// </summary>
    /// <param name="pCurTeam"></param>
    /// <param name="pNumber"></param>
    public int SetTeamSupporter(int pNumber)
    {
        int rand = Random.Range(1, 9);
        if (rand == pNumber)
            return SetTeamSupporter(pNumber);
        else
        {
            return rand;
        }
    }

    public void SetPassNumber()
    {
        if (PassNumber > 0)
            PassNumber--;
        else
            PassNumber = Random.Range(1, 9);
    }


    /// <summary>
    /// 激活所有球员的行为树
    /// </summary>
    public void EnableAllPlayerBehaviour()
    { 
    
    }

}
