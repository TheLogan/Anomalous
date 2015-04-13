using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour
{
	private List<GameObject> _allGOs;
	public float _radius;
	[SerializeField] private float _rotationalForce = 10;
	[SerializeField] private float bounceDamp = 0.000005f; //how rapidly it bounces
	private SpaceShip _playerShip;

	public SpaceShip SpaceShip
	{
		get
		{
			return _playerGo.GetComponent<SpaceShip>();
		}
	}

	private GameObject _playerGo;
	public GameObject PlayerGo
	{
		get { return _playerGo ?? (_playerGo = GameObject.FindWithTag("Player")); }
	}

	public void SetInitialInfo(float range, List<GameObject> allGOs )
	{
		_radius = range;
		_allGOs = allGOs;
	}


	public void GoListAdd(GameObject go)
	{
		_allGOs.Add(go);
	}
	

	void Update()
	{
		DamagePlayer();
	}

	void DamagePlayer()
	{
		float dist = Vector3.Distance(transform.position, PlayerGo.transform.position);
		if (dist < _radius)
		{
//			_playerGo.GetComponent<SpaceShip>().MyEnergyCore.DoDamage(_radius - dist, Time.deltaTime);
			SpaceShip.CurrentPressure = _radius - dist;

			if (dist < 2)
			{
				PlayerReachedCenter();
			}
		}
		else
		{
			SpaceShip.CurrentPressure = 0;
		}
	}

	void PlayerReachedCenter()
	{
		Application.LoadLevel("GameWon");
	}
	

	void FixedUpdate()
	{
		for (int i = 0; i < _allGOs.Count; i++)
		{
			var go = _allGOs[i];
			if (go == null)
			{
				_allGOs.Remove(go);
				continue;
			}
			var goRigid = go.GetComponent<Rigidbody2D>();
			float goInitialDistance = go.GetComponent<SpawnableGO>().InitialDistance;

			var step1 = go.transform.position - transform.position;
			var step2 = new Vector2(step1.y, -step1.x);
			var c = (Vector2)(transform.position) + step2;

			var targetDirection = (Vector2)go.transform.position - c * 0.02f;
			//			var targetDirection = Vector2.zero;

			float dist = Vector2.Distance(transform.position, go.transform.position);

			targetDirection += CalcBouyancy(go, goInitialDistance, goRigid, dist, targetDirection);

			goRigid.AddForce(targetDirection);
		}

	}
	Vector2 CalcBouyancy (GameObject go, float preSetDistance, Rigidbody2D goRigid, float dist, Vector2 targetDirection) {
		Vector2 wantedPosition = (go.transform.position - transform.position).normalized * preSetDistance;
		Vector2 wantedVector2 = (wantedPosition - (Vector2)go.transform.position);
		Vector2 uplift = wantedVector2 * (dist - preSetDistance);
		return uplift;
	}
}
