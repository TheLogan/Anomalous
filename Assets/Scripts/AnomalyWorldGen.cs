using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Collections;

public class AnomalyWorldGen : MonoBehaviour
{
	[SerializeField] private Vector2 _anomalyCenter;
	[SerializeField] private float _radius;
	[SerializeField] private int _complexity;
	[SerializeField] private BaseArrowScript _anomArrow;
	
	void Start ()
	{
		Object[] spawnablePrefabs = Resources.LoadAll("WorldGenFabs");
		
		var anomalousWorldGo = new GameObject("Anomalous world GO");
		anomalousWorldGo.transform.position = _anomalyCenter;
		
		var allObjects = new List<GameObject>();
		var anonCont = anomalousWorldGo.AddComponent<WorldController>();
		if(_anomArrow != null)
			_anomArrow.GetComponent<BaseArrowScript>().track = anomalousWorldGo;
		foreach (var o in spawnablePrefabs)
		{
			var prefab = (GameObject) o;
			int count = _complexity - prefab.GetComponent<SpawnableGO>().GetRarity();

			for (int i = 0; i < count; i++)
			{
				float randDir = Random.Range(0, 360) * Mathf.PI / 180;
				float randLength = Random.Range(0, _radius);
				var randPos = new Vector2(randLength * Mathf.Cos(randDir) + _anomalyCenter.x, randLength * Mathf.Sin(randDir) + _anomalyCenter.y); 
				
				GameObject go = (GameObject)Instantiate(prefab, randPos, Quaternion.identity);
				go.GetComponent<SpawnableGO>().InitialDistance = randLength;
				allObjects.Add(go);
			}
		}
		anonCont.SetInitialInfo(_radius, allObjects);
	}
}
