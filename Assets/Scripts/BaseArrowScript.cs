using UnityEngine;
using System.Collections;
using System;

public class BaseArrowScript : MonoBehaviour {

    public GameObject track;
    public GameObject player;
    public float distanceFromPlayer;
    public float minimumDistance;

    [NonSerialized]
    public int distanceToBase;

    private SpriteRenderer _spriteRenderer;
	// Use this for initialization
	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	void Update()
    {
        JumpToPlayer();
    }

    void FixedUpdate()
    {
        RotateToTrack();
    }


    void RotateToTrack()
    {
        Vector3 goPos = track.transform.position;
        goPos.z = 0;
        var dir = goPos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void JumpToPlayer()
    {
        Vector3 wayToBase = track.transform.position - player.gameObject.transform.position;
        distanceToBase = (int)wayToBase.sqrMagnitude;
        Vector3 direction = wayToBase.normalized;
        this.transform.position = player.transform.position + direction * distanceFromPlayer;

        if (distanceToBase < minimumDistance)
        {
            _spriteRenderer.enabled = false;
        }
        else
        {
            _spriteRenderer.enabled = true;
        }
    }

    
}
