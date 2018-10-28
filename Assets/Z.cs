using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * (1 / Controller.Lenght);
        var rb = Random.insideUnitCircle;
        GetComponent<Renderer>().material.color = new Color(rb.x, rb.y, 1, 1);
    }

}
