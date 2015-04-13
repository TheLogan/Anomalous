using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

public class EnergyCore : MonoBehaviour
{
	[SerializeField]
	private float _energyLevel = 100;

	public void DoDamage(float damageAmount)
	{
		_energyLevel -= damageAmount * Time.deltaTime;
	}

	public float GetEnergyLevel()
	{
		return _energyLevel;
	}

	public void ResetPower()
	{
		_energyLevel = GetComponent<Inventory>().baseEnergy;
	}

    public void AddEnergy(float amount)
    {
        Inventory inventory = GetComponent<Inventory>();
        _energyLevel += amount;
        if (inventory.baseEnergy < _energyLevel)
        {
            _energyLevel = inventory.baseEnergy;
        }
    }
}
