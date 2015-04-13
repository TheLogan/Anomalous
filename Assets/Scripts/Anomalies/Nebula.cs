using UnityEngine;
using System.Collections;

public class Nebula : MonoBehaviour
{

	[SerializeField] private ParticleSystem _partSys;
	private ParticleSystem.Particle[] _particles;
	
	void Start()
	{
		Color colA = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		Color colB = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		_partSys.startColor = colA;
//		_partSys.startColor[1] = colB;
//		_partSys.Emit(100);
		_partSys.emissionRate = 6;
		_partSys.enableEmission = true;
	}
}
