using UnityEngine;
using System.Collections;

public class BlackHole : Anomaly
{
	[SerializeField]
	private GameObject _playerGo;
	[SerializeField]
	private Rigidbody2D _playerRigid;

	[SerializeField] private Renderer _anomalyRenderer;
	[SerializeField] private float forceMultiplier;
	private Color _startColour;
	private float _startRimPower;
	private Color lerpedColor;

	private float _radius;

	void Start()
	{
		_radius = GetComponent<CircleCollider2D>().radius + 0.5f;
		_startColour = _anomalyRenderer.material.GetColor("_rimColor");
		_startRimPower = _anomalyRenderer.material.GetFloat("_FrenselPower");

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			_playerGo = other.gameObject;
			_playerRigid = other.GetComponent<Rigidbody2D>();
		}
	}

	void Update()
	{
		if (_playerGo != null)
		{
			var dist = Vector2.Distance(transform.position, _playerGo.transform.position);
			if (dist < _radius)
			{
				float strength = 1 - dist/_radius;
				lerpedColor = Color.Lerp(_startColour, Color.red, strength);
				float fresnelStrength = Mathf.Lerp(2.5f, 0.6f, strength);
				_anomalyRenderer.material.SetColor("_rimColor", lerpedColor);
				_anomalyRenderer.material.SetFloat("_FrenselPower", fresnelStrength);
				
				float f = _radius - dist;
				_playerRigid.AddForce((transform.position - _playerGo.transform.position) * f * forceMultiplier);
			}
		}
	}
}
