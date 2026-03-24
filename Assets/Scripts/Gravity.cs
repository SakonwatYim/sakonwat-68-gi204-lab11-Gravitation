using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f; //Gravitinal constant 6.674

    public static List<Gravity> otherObjectList;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjectList == null)
        {
            otherObjectList = new List<Gravity>();
        }

        otherObjectList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }
    void Attract (Gravity other)
    {
        Rigidbody otherRb = other.rb;
        //get direction between 2 objs
        Vector3 direction = rb.position - otherRb.position;

        //get only distance between 2 objs
        float distance = direction.magnitude;
        
        //if 2 objs are at the same position, just return = do nothing to avoid collition
        if(distance == 0f) {return; }

        //F = G*((m1*m2)/r*r
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        //combine force manitude with its direction (nomalize)
        //to from final gravitinal force
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    } 
}
