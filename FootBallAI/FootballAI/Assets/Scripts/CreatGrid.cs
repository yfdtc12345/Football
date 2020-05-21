//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions;
using UnityEngine;
using Gamelogic.Grids2;
using Boo.Lang;
using BehaviorDesigner.Runtime;

namespace Gamelogic.Grids.Examples
{
	public class CreatGrid : GLMonoBehaviour
	{
		private static CreatGrid _instance = null;

		public static CreatGrid Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (CreatGrid)GameObject.FindObjectOfType(typeof(CreatGrid));
				}
				return _instance;
			}
		}

		[HideInInspector]  public int m_Zoffset = -1;

		public SpriteCell cellPrefab;
		// All cells will be parented to this object.
		public GameObject root;
		/// <summary>
		/// 进攻球员预制件
		/// </summary>
		public GameObject player;
		/// <summary>
		/// 敌人球员预制件
		/// </summary>
		public GameObject enemy;

		public GameObject ballPrefab;
		[HideInInspector]
		public GameObject m_Ball;

		private List<RectPoint> rectPointList = new List<RectPoint>();

		public List<Vector2> posList = new List<Vector2>(10);		

		public int[] testX = new int[10];
		public int[] testY = new int[10];

		RectPoint[,] rectPointArr = new RectPoint[51,111];

		private Team redTeam;
		private Team blueTeam;

		// The grid data structure that contains all cell.
		private RectGrid<SpriteCell> grid;
	
		private IMap3D<RectPoint> map;

		private Color Color1= new Color(23/255.0f,166f/255.0f,41/255.0f);
		private Color Color2 = new Color(27/255.0f, 90/ 255.0f, 35 / 255.0f);
		/// <summary>
		/// 当前进攻队员
		/// </summary>
		public List<GameObject> curPlayerList = new List<GameObject>(10);
		/// <summary>
		/// 当前防守队员
		/// </summary>
		public List<GameObject> curEnemyList = new List<GameObject>(10);
		public void Awake()
		{
			BuildGrid();
		}

		public void Start()
		{
			AddPos();
		}

		private void BuildGrid()
		{
			// Creates a grid in a rectangular shape.
			grid = RectGrid<SpriteCell>.Rectangle(51, 111 );

			// Creates a map...
			map = new RectMap(cellPrefab.Dimensions) // The cell dimensions usually correspond to the visual 
													 // part of the sprite in pixels. Here we use the actual 
													 // sprite size, which causes a border between cells.
													 // 

				.WithWindow(ExampleUtils.ScreenRect) // ...that is centered in the rectangle provided
				.AlignMiddleCenter(grid) // by this and the previous line.
				.To3DXY(); // This makes the 2D map returned by the last function into a 3D map
						   // This map assumes the grid is in the XY plane.
						   // To3DXZ assumes the grid is in the XZ plane (you have to make sure 
						   //your tiles are similarly aligned / rotated).


			foreach (RectPoint point in grid) //Iterates over all points (coordinates) contained in the grid
			{
				SpriteCell cell = Instantiate(cellPrefab); // Instantiate a cell from the given prefab.
														   //This generic version of Instantiate is defined in GLMonoBehaviour
														   //If you don't want to use GLMonoBehvaiour, you have to cast the result of
														   //Instantiate

				Vector3 worldPoint = map[point]; //Calculate the world point of the current grid point

				cell.transform.parent = root.transform; //Parent the cell to the root
				cell.transform.localScale = Vector3.one; //Readjust the scale - the re-parenting above may have changed it.
				cell.transform.localPosition = worldPoint; //Set the localPosition of the cell.


				cell.Color = (point.Magnitude() % 2 == 0 )? Color2 : Color2;
				if((point.X==3|| point.X==47)&&( point.Y > 2 && point.Y < 108))
				{
					cell.Color = new Color(0.8f, 0.8f, 0.8f);
				}
				if ((point.Y == 3 || point.Y == 107 || point.Y == 55) && (point.X > 2 && point.X < 48))
				{
					cell.Color = new Color(0.8f, 0.8f, 0.8f);
				}

				if ((point.X == 21 || point.X ==29) && (point.Y <= 2 || point.Y >= 108))
				{
					cell.Color = new Color(0.8f, 0.8f, 0.8f);
				}
				if ((point.Y== 0 || point.Y == 110) && (point.X > 21 && point.X <29))
				{
					cell.Color = new Color(0.8f, 0.8f, 0.8f);
				}
				cell.name = point.ToString(); // Makes it easier to identify cells in the editor.
				grid[point] = cell; // Finally, put the cell in the grid.				

				//if (point.X >= 3 && point.X <= 47 && point.Y >= 3 && point.Y <= 107)
				//{
					rectPointArr[point.X, point.Y] = point;
				//}				
			}		

		}

		public void Update()
		{
			//Vector3 test = GetPosByXY(25, 107)- m_Ball.transform.localPosition;
			//float angle = Vector3.Angle(m_Ball.transform.up, test);
			//float dot = Vector3.Dot(m_Ball.transform.up, test.normalized);
			//float angle2 = Mathf.Acos(dot)*Mathf.Rad2Deg;
			////Vector3 d1 = transform.forward;
			////Vector3 d2 = new Vector3(0, 3, 0);
			//Vector3 cross = Vector3.Cross(m_Ball.transform.up, test.normalized);
			//float angle3 = Mathf.Atan2( Vector3.Dot(m_Ball.transform.forward, cross), dot) * Mathf.Rad2Deg;
			
			//Debug.Log(angle3);
		}
		/// <summary>
		/// 创建球，初始位置在中心
		/// </summary>
		private void CreateBall(GameObject pRoot)
		{
			m_Ball = Instantiate(ballPrefab);
			m_Ball.transform.parent = pRoot.transform; //Parent the cell to the root
			m_Ball.transform.localScale = new Vector3(150f, 150f, 1);
			m_Ball.transform.localPosition = new Vector3(150, 0, 0);
			//int X = rectPointArr.GetLength(0);
			//int Y = rectPointArr.GetLength(1);
			//m_Ball.transform.localPosition = map[rectPointArr[Mathf.FloorToInt(X/2), Mathf.FloorToInt(Y/2)]] + new Vector3(0,0,-1);
		}

		/// <summary>
		/// 根据X,Y索引值获取位置
		/// </summary>
		/// <param name="X"></param>
		/// <param name="Y"></param>
		/// <returns></returns>
		public Vector3 GetPosByXY(int X, int Y)
		{
			if (X < 0 || Y < 0 || X >= 51 || Y >= 111)
			{
				Debug.LogError("该点不在球场上");
				return Vector3.zero;
			}
			Vector3 res = map[rectPointArr[X, Y]] + new Vector3(0, 0, -1);
			return res;
		}

		public Vector3 GetPosByXY(Vector2 vec)
		{
			if (vec.x < 0 || vec.y < 0 || vec.x >= 51 || vec.y >= 111)
			{
				Debug.LogError("该点不在球场上");
				return Vector3.zero;
			}
			Vector3 res = map[rectPointArr[Mathf.FloorToInt(vec.x), Mathf.FloorToInt(vec.y)]] + new Vector3(0, 0, -1);
			return res;
		}


		public Team GetRedTeam()
		{
			return redTeam;
		}

		public Team GetBlueTeam()
		{
			return blueTeam;
		}

		public void CreatPlayer()
		{
			redTeam = new Team(TeamColor.Red);
			for (int i = 0; i < posList.Count; i++)
			{
				GameObject temp = Instantiate(player);
				temp.transform.parent = root.transform; //Parent the cell to the root
				temp.transform.localScale = new Vector3(2, 2, 1);
				if (i == 8)
				{
					temp.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90f);
					temp.GetComponent<FieldPlayer>().m_AttachedBall = true;					
				}
				else
				{
					temp.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90f);
					temp.GetComponent<FieldPlayer>().m_AttachedBall = false;
				}
				temp.transform.localPosition = map[rectPointArr[Mathf.FloorToInt(posList[i].x), Mathf.FloorToInt(posList[i].y)]] + new Vector3(0, 0, -1);
				temp.GetComponent<FieldPlayer>().curTeam = redTeam;
				temp.GetComponent<FieldPlayer>().m_Number = i ;
				curPlayerList.Add(temp);			
			}

			
			//redTeam.SetPlayer(curPlayerList);
			CreateBall(curPlayerList[8]);
			//为所有球员挂上球
			foreach (GameObject playerObj in curPlayerList)
			{
				playerObj.GetComponent<FieldPlayer>().AddBall(m_Ball);
			}
			Debug.Log("创建球员");		
		}

		private void AddPos()
		{
			for (int i = 0; i < 10; i++)
			{
				Vector2 temp = new Vector2(testX[i],testY[i]);
				posList.Add(temp);
			}
		}

		public void GameStart()
		{
			Debug.Log("开始比赛");
			//激活所有球员的行为树
			GameController.Instance.SetGameState(GameState.Running);
			foreach (var obj in curPlayerList)
			{
				BehaviorTree temp = obj.GetComponent<BehaviorTree>();
				temp.EnableBehavior();
			}		

		}

		public void AddEnemy()
		{
			blueTeam = new Team(TeamColor.Blue);
			for (int i = 0; i < posList.Count; i++)
			{
				GameObject temp = Instantiate(enemy);
				temp.transform.parent = root.transform; //Parent the cell to the root
				temp.transform.localScale = new Vector3(2, 2, 1);						
				temp.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90f);
				temp.GetComponent<FieldPlayer>().m_AttachedBall = false;
				if (i >= 7)
				{
					temp.transform.localPosition = map[rectPointArr[Mathf.FloorToInt(50 - posList[i].x), Mathf.FloorToInt(110 - posList[i].y)]] + new Vector3(0, 1800, -1);
				}
				else
				{
					temp.transform.localPosition = map[rectPointArr[Mathf.FloorToInt(50 - posList[i].x), Mathf.FloorToInt(110 - posList[i].y)]] + new Vector3(0, 0, -1);
				}				
				temp.GetComponent<FieldPlayer>().curTeam = blueTeam;
				temp.GetComponent<FieldPlayer>().m_Number = i + 1;
				curEnemyList.Add(temp);
			}
			//为所有球员挂上球
			foreach (GameObject playerObj in curEnemyList)
			{
				playerObj.GetComponent<FieldPlayer>().AddBall(m_Ball);
			}
			Debug.Log("创建敌方球员");
		}
	}
}