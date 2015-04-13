using UnityEngine;
using System.Collections;

public class SpawnableGO : MonoBehaviour
{
	[SerializeField] private int _rarity;
	[SerializeField] public float InitialDistance;

	public int GetRarity()
	{
		return _rarity;
	}
}
