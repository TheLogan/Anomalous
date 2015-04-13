using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowText : MonoBehaviour {
    [SerializeField]
    private Text myText;

    [SerializeField]
    private GameObject arrow;

    private BaseArrowScript arrowScript;
    private CanvasRenderer _renderer;

    void Start()
    {
        myText = GetComponent<Text>();
        arrowScript = arrow.GetComponent<BaseArrowScript>();
        _renderer = GetComponent<CanvasRenderer>();
    }

    void Update()
    {
        transform.eulerAngles = Vector3.zero;

        myText.text = arrowScript.distanceToBase.ToString();

        if (arrowScript.distanceToBase < arrowScript.minimumDistance)
        {
            _renderer.SetAlpha(0);
        }
        else
        {
            _renderer.SetAlpha(1);
        }
    }
}
