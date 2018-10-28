using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = Random.insideUnitCircle * (10f / Controller.Lenght + 3);
        var c = Random.insideUnitCircle;
        GetComponent<Renderer>().material.color = new Color(c.x, c.y, 1, 1);
    }

    private void Update()
    {
        if (rb.velocity.sqrMagnitude < 0.001f)
        {
            rb.velocity = Random.insideUnitCircle * (20 / Controller.Lenght);
        }
    }

}
