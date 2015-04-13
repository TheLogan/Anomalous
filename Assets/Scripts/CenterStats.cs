using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CenterStats : MonoBehaviour
{
    public float pullForceAmount;

    public List<Rigidbody2D> pullingObject;


    void Start()
    {
        pullingObject = new List<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        foreach (Rigidbody2D rb in pullingObject)
        {
            Move(rb);
        }
    }


    void Move(Rigidbody2D rb)
    {
        rb.AddForce((transform.position - rb.transform.position).normalized * pullForceAmount * Time.smoothDeltaTime);
    }
}
