using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionScript : MonoBehaviour {
    [SerializeField]
    private GameObject descriptionCanvas;

    [SerializeField]
    private Text myText;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseEnter()
    {
        descriptionCanvas.SetActive(true);
    }

    public void OnMouseLeave()
    {
        descriptionCanvas.SetActive(false);
    }

    public void UpdateDescription(string description, int upgradeCost, string upgradeName, int power, int level)
    {
        myText.text = description + System.Environment.NewLine
            + "Level: " + level + "/6" + System.Environment.NewLine
            + "Upgrade Cost: " + upgradeCost + System.Environment.NewLine
            + upgradeName + ": " + power;
    }

    public void UpdateDescription(string description, int upgradeCost, string upgradeName)
    {
        myText.text = "Consumable item: " + upgradeName
            + description + System.Environment.NewLine
            + "Cost: " + upgradeCost + System.Environment.NewLine;
    }
    
}
