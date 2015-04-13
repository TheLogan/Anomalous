using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    
    [SerializeField] private GameObject _playerShip;
	[SerializeField] private int depth = 35;

	private bool flyToBase = false;
	private GameObject _audioList;

	void Start()
	{
		_audioList = new GameObject("MyAudioListener");
		_audioList.AddComponent<AudioListener>();
	}

	void FixedUpdate()
	{
		_audioList.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		Vector3 myPos = transform.position;
		Vector3 targetPos = _playerShip.transform.position;
		targetPos.z = -depth;
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime);
	}

}
