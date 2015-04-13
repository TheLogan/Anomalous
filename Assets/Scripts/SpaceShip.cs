using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : MonoBehaviour
{
	private Rigidbody2D _myRigidbody2D;
	public bool IsDead = false;

	[SerializeField] private float _speed;
	public EnergyCore MyEnergyCore { get; private set; }

	[SerializeField] private GameObject _explosion;

	public float CurrentPressure { get; set; }
	[SerializeField] private VignetteAndChromaticAberration _camVignette;

	[SerializeField] private WorldController worldController;

	[SerializeField] private Inventory inventory;

	public bool isBoosted = false;

	private WorldController WorldController
	{
		get
		{
			if (worldController == null)
				worldController = GameObject.Find("Anomalous world GO").GetComponent<WorldController>();
			return worldController;
		}
	}


    void Awake()
    {
        if (MyEnergyCore == null)
        {
            MyEnergyCore = gameObject.AddComponent<EnergyCore>();
        }
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate ()
	{
		RotateToMouse();
		ApplyMovement();
	}

	void ApplyMovement()
	{
		float myBoost = inventory.baseThrusterPower;
		if (isBoosted)
			myBoost *= 1.5f;
		Vector2 forceVector = Vector3.zero;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (_myRigidbody2D.velocity.magnitude > 0.35f)
				forceVector = _myRigidbody2D.velocity*-1;
			else
				forceVector = _myRigidbody2D.velocity*-3;
		}
		else
		{
			if (Input.GetAxis("Vertical") > 0)
				forceVector += (Vector2)transform.right * 2 * _speed * Time.fixedDeltaTime * myBoost;
			else if (Input.GetAxis("Vertical") < 0)
				forceVector -= (Vector2)transform.right * _speed * Time.fixedDeltaTime * myBoost;
			forceVector -= (Vector2)transform.up * Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime * myBoost;
		}
		
		_myRigidbody2D.AddForce(forceVector);
	}

	void Update()
	{
		if (CurrentPressure > 0)
		{
			MyEnergyCore.DoDamage(CurrentPressure);


			float myLevel = MyEnergyCore.GetEnergyLevel();
			float val = (inventory.baseEnergy/myLevel) - 1;
			print(val);
			_camVignette.intensity = Mathf.Lerp(0, 20, val); // use current energy upgrade level
			
//			GetComponent<Inventory>().baseEnergy
//			CurrentPressure/WorldController._radius;


//			_camVignette.intensity = Mathf.Lerp(0, 20, CurrentPressure / WorldController._radius); // use current energy upgrade level
		}
		else
		{
			if (_camVignette != null)
			{
				_camVignette.intensity = Mathf.Lerp(_camVignette.intensity, 0, Time.deltaTime);
//				_camVignette.intensity = 0;
			}
		}


		if (!IsDead && MyEnergyCore.GetEnergyLevel() <= 0)
		{
			IsDead = true;
			var hq = GameObject.Find("HQ").GetComponent<Respawner>();
			var go =(GameObject)Instantiate(_explosion, transform.position, Quaternion.identity);
			Destroy(go, 3);
			inventory.DropInventory();
			hq.Die(this);
		}
	}
	
	[SerializeField] private Vector3 _mousPos;

	void RotateToMouse()
	{
		_mousPos = Input.mousePosition;
		_mousPos.z = 35;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(_mousPos);

		var dir = mousePos - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void Respawn()
	{
		IsDead = false;
		MyEnergyCore.ResetPower();
	}
}
