using System.Collections.Generic;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Grids2.Graph;
using UnityEngine;
using System.Linq;
using System;
using Gamelogic.Extensions.Examples;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Examples
{

	public class SpaceFillingCurvesExample : GLMonoBehaviour
	{
		#region Constants
		private const int HilbertMax = 1024;
		private const int PeanoMax = 729;
		private const int GosperMax = 2401;

		#endregion

		#region  Types

		public enum CurveType
		{
			Hilbert,
			Peano,
			Gosper
		}

		public enum MarkerType
		{
			Sprite,
			Arrow
		}

		public enum ColoringType
		{
			LowProbaility,
			InterLeave,
			PerlinNoise,
			Gradient,
		}

		#endregion

		#region Public Fields

		[Header("Runtime tweakables")]
		public CurveType curve;

		[Range(0f, 1f)]
		[Comment("Count is normalized, it means that:\n0 is 0 points\n1 is a constant maxPoints for a given curve")]
		public float count;

		[Header("Markers")]
		public MarkerType markerType;
		public GameObject arrowMarker;
		public GameObject spriteMarker;

		[Header("Coloring")]
		public ColoringType coloringType;
		public Gradient gradient;

		#endregion

		#region Private Fields

		private List<GameObject> markers;
		private ObservedValue<int> pointCount;
		private GridMap<GridPoint2> gridMap = null;
		private List<GridPoint2> points;
		private int pointMax;
		private ObservedValue<CurveType> observedCurve;
		private ObservedValue<MarkerType> observedMarker;
		private ObservedValue<ColoringType> observedColoring;

		#endregion

		#region Unity Messages

		public void Start()
		{
			markers = new List<GameObject>();

			observedCurve = new ObservedValue<CurveType>(curve);
			observedCurve.OnValueChange += UpdateCurveType;

			observedMarker = new ObservedValue<MarkerType>(markerType);
			observedMarker.OnValueChange += UpdateMarkers;

			observedColoring = new ObservedValue<ColoringType>(coloringType);
			observedColoring.OnValueChange += Generate;

			pointCount = new ObservedValue<int>(pointMax);
			pointCount.OnValueChange += UpdateCurvePoints;

			UpdateCurveType();
		}

		public void Update()
		{
			pointCount.Value = Mathf.FloorToInt(Mathf.Lerp(1, pointMax, count));
			observedCurve.Value = curve;
			observedMarker.Value = markerType;
			observedColoring.Value = coloringType;
		}

		#endregion

		#region Public Methods

		public void Generate()
		{
			var previous = default(GameObject);
			var colorGen = default(IGenerator<Color>);

			switch (coloringType)
			{
				case ColoringType.LowProbaility:
					colorGen = Generator
						.RandomBoolGenerator(1 / 22f)
						.Select(x => x ? Utils.Blue : Utils.Red);
					break;

				case ColoringType.InterLeave:
					var randomIntG = Generator
						.UniformRandomInt(7)
						.Select(x => x + 7*2);
					var oneG = Generator.Constant(1);

					var selector = Generator.Interleave(randomIntG, oneG);

					colorGen = Generator
						.Count(2)
						.Select(x => x == 1 ? Utils.Blue : Utils.Red)
						.RepeatEach(selector);
					break;

				case ColoringType.PerlinNoise:
					colorGen = ExampleGenerators
						.PerlinNoise(4, 4)
						.Window(20)
						.Select(list => list.Average())
						.Select(x => gradient.Evaluate(x));
					break;

				case ColoringType.Gradient:
					colorGen = Generator.Count(pointCount.Value)
						.Select(x => gradient.Evaluate((float)x / pointCount.Value));
					break;
			}

			for (var i = 0; i < pointCount.Value; i++)
			{
				var position = gridMap.GridToWorld(points[i]);
				var name = i.ToString();
				var color = colorGen.Next();

				switch (markerType)
				{
					case MarkerType.Arrow:
						previous = CreateArrowMarker(i, previous, position, name, color);
						break;
					case MarkerType.Sprite:
						previous = CreateSpriteMarker(i, previous, position, name, color);
						break;
				}
			}

			for (var i = pointCount.Value; i < markers.Count; i++)
			{
				markers[i].SetActive(false);
			}
		}
		#endregion

		#region Private Members
		private void UpdateCurveType()
		{
			markers.ForEach(m => Destroy(m));
			markers.Clear();
			IGenerator<GridPoint2> generator = null;

			switch (curve)
			{
				case CurveType.Hilbert:
					pointMax = HilbertMax;
					gridMap = GetRectMap();
					generator = SpaceFillingCurveGenerator.Hilbert();
					break;

				case CurveType.Peano:
					pointMax = PeanoMax;
					gridMap = GetRectMap();
					generator = SpaceFillingCurveGenerator.Peano();
					break;

				case CurveType.Gosper:
					pointMax = GosperMax;
					gridMap = GetHexMap();
					generator = SpaceFillingCurveGenerator.Gosper();
					break;
			}
			points = generator.Next(pointMax).ToList();
		}

		public void UpdateCurvePoints()
		{
			Generate();
		}

		public void UpdateMarkers()
		{
			markers.ForEach(marker => Destroy(marker));
			markers.Clear();

			Generate();
		}

		private GridMap<GridPoint2> GetHexMap()
		{
			var spaceMap = Map
				.Linear(PointyHexPoint.SpaceMapTransform)
				.PreScale(Vector3.one * 0.5f);
			var roundMap = Map.RectRound();

			return new GridMap<GridPoint2>(spaceMap, roundMap);
		}

		private GridMap<GridPoint2> GetRectMap()
		{
			var spaceMap = Map.Linear(Matrixf33.Identity);
			var roundMap = Map.RectRound();

			spaceMap.PreTranslate(new Vector3(100, 100, 0));

			return new GridMap<GridPoint2>(spaceMap, roundMap);
		}

		private GameObject CreateArrowMarker(int index, GameObject previous, Vector3 position, string markerName, Color color)
		{
			var newMarker = default(GameObject);

			if (markers.Count <= index)
			{
				newMarker = Instantiate(arrowMarker);
				markers.Add(newMarker);

				newMarker.transform.parent = transform;
				newMarker.transform.position = position;
				newMarker.name = markerName;

				var material = newMarker.GetComponent<Renderer>().sharedMaterial;

				material = new Material(material);
				newMarker.GetComponent<Renderer>().sharedMaterial = material;
			}
			else
			{
				newMarker = markers[index];
				newMarker.SetActive(true);
			}

			newMarker.GetComponent<Renderer>().sharedMaterial.color = color;
			newMarker.transform.localScale = Vector3.one * 0.3f;

			if (newMarker.transform.childCount > 0)
			{
				newMarker.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial =
					newMarker.GetComponent<Renderer>().sharedMaterial;
			}

			if (previous != null)
			{
				previous.transform.LookAt(newMarker.transform.position);
				previous.transform.SetScaleZ(Vector3.Distance(previous.transform.position, newMarker.transform.position));
			}

			return newMarker;
		}

		private GameObject CreateSpriteMarker(int index, GameObject previous, Vector3 position, string markerName, Color color)
		{
			var newMarker = default(GameObject);

			if (markers.Count <= index)
			{
				newMarker = Instantiate(spriteMarker);
				markers.Add(newMarker);
			
				newMarker.transform.parent = transform;
				newMarker.transform.position = position;
				newMarker.name = markerName;
			
				var material = newMarker.GetComponent<Renderer>().sharedMaterial;
			
				material = new Material(material);
				newMarker.GetComponent<Renderer>().sharedMaterial = material;
			}
			else
			{
				newMarker = markers[index];
				newMarker.SetActive(true);
			}

			var size = curve == CurveType.Gosper ? 0.0028f : 0.0055f;
			
			newMarker.GetComponent<Renderer>().sharedMaterial.color = color;
			newMarker.transform.localScale = Vector3.one * size;
			
			if (newMarker.transform.childCount > 0)
			{
				newMarker.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial =
					newMarker.GetComponent<Renderer>().sharedMaterial;
			}
			
			return newMarker;
		}
		#endregion
	}
}