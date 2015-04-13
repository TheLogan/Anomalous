using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class LargeBouyancyGO : MonoBehaviour
{

	public bool autoSetWaterLevel = true;

	public float waterLevel = 0.0f;
	public float floatHeight = 1.0f;
	public float bounceDamp = 0.05f;

	public ForceMode forceMode;

	public Transform[] bPoints;



	void Start()
	{
		if (autoSetWaterLevel)
			waterLevel = transform.position.y;

		if (bPoints[0] == null)
		{
			bPoints[0].transform.position = transform.position;
		}
	}

	void FixedUpdate()
	{
		for (int i = 0; i < bPoints.Length; i++)
		{
			Vector3 actionPoint = bPoints[i].transform.position;
			float forceFactor = (1f - ((actionPoint.y - waterLevel) / floatHeight)) / bPoints.Length;

			if (forceFactor > 0f)
			{
				Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * ((bounceDamp / bPoints.Length) * Time.deltaTime));
				GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint, forceMode);
			}
		}
	}
}
