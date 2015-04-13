using UnityEngine;
using System.Collections;

public class Pulling : MonoBehaviour
{

    public GameObject CenterObject;
    public int[] PullabledLayers;
    protected CenterStats cs;

    void start()
    {
        cs = CenterObject.GetComponent<CenterStats>();
    }
    

    void OnTriggerEnter2D(Collider2D coll)
    {
        foreach (int pullLayer in PullabledLayers)
        {
            if (coll.gameObject.layer == pullLayer)
            {
                cs.pullingObject.Add(coll.GetComponent<Rigidbody2D>());
            }
        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        foreach (int pullLayer in PullabledLayers)
        {
            if (coll.gameObject.layer == pullLayer)
            {
                cs.pullingObject.Remove(coll.GetComponent<Rigidbody2D>());
            }
        }

    }
                
}
