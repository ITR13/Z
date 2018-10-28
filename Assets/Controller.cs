using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static float Lenght = 20;

    private float _invincible = 0f;

    private Transform _lifeT, _timeT;
    private float _life, _time;

    private Renderer _renderer;
    private Rigidbody2D _rigidBody;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
        _invincible = 3;

        _bed = GameObject.Find("Bed");
        _office = GameObject.Find("Office");

        _lifeT = transform.Find("Life");
        _timeT = transform.Find("Time");
        _lifeT.GetComponent<Renderer>().material.color = Color.blue;
        _timeT.GetComponent<Renderer>().material.color = Color.green;
        _life = 1;
        _time = 0;

        Switch();
    }

    Vector2 _direction;
    private void Update()
    {
        _direction = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        _invincible -= Time.deltaTime;
        if (_invincible >= 0)
        {
            _renderer.material.color = Color.red;
        }
        else
        {
            _renderer.material.color = Color.white;
        }
        _life -= Time.deltaTime / (4 * Lenght);
        _time -= Time.deltaTime / (Lenght);

        _lifeT.localScale = new Vector3(_life, 0.05f, 1);
        _timeT.localScale = new Vector3(_time, 0.05f, 1);

        if (_life < 0)
        {
            Destroy(gameObject);
        }
        else if (_time < 0)
        {
            Switch();
        }
    }

    private GameObject _bed, _office;
    private bool _day = false;
    private void Switch()
    {
        _bed.SetActive(_day);
        _day = !_day;
        _office.SetActive(_day);
        _invincible = _day ? 3 : 0;
        _time += 1;
        if (_day && Lenght > 6)
        {
            Lenght -= 1;
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _direction * 5;
    }

    private static int A;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Z"))
        {
            var pos = collision.transform.localPosition;
            if (pos.magnitude < 3)
            {
                pos += (Vector3)Random.insideUnitCircle.normalized * 3;
            }

            pos.x *= -1;
            collision.transform.localPosition = pos;
            if (A++ < 20)
            {
                var next = Instantiate(collision.gameObject);
                pos.x *= -1;
                pos.y *= -1;
                next.transform.localPosition = pos;
            }
            if (_day)
            {
                if (_invincible <= 0)
                {
                    _life -= 0.2f;
                }
            }
            else
            {
                _life += 0.2f;
            }
        }
    }
}
