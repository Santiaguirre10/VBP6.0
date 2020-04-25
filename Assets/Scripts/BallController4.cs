using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController4 : MonoBehaviour
{
    float rateVelocity;
    public float speedp;
    public float t;
    public float r;
    [SerializeField]
    GameObject end, ball5, ball6;
    Vector2 initpos;
    [SerializeField]
    Vector2 pmax;
    public bool hitball;
    [SerializeField]
    SpriteRenderer sprite;

    private void Start()
    {
        t = 0;
        end = GameObject.Find("End");
        initpos = transform.position;
        var x = Random.Range(0.752f, 4.198f);
        var y = Random.Range(1.023f, 3.028f);
        end.transform.position = new Vector2(x, y);
        end.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    private void Update()
    {
        MovimientoParab(initpos, end.transform.position);
        if (Input.GetKey(KeyCode.Space))
        {
            hitball = true;
        }
        else
        {
            hitball = false;
        }
    }
    public void AllButton()
    {
        hitball = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(ball5, transform.position, Quaternion.identity);
                end.GetComponentInChildren<SpriteRenderer>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (t == 1)
            {
                Instantiate(ball6, transform.position, Quaternion.identity);
                end.GetComponentInChildren<SpriteRenderer>().enabled = false;
                Destroy(gameObject);
            }
        }
    }
    void MovimientoParab(Vector2 a, Vector2 b)
    {
        var x = ((b.x - a.x) / 2f) + a.x;
        var y = b.y + 10f;
        pmax = new Vector2(x, y);
        rateVelocity = 1f / Vector2.Distance(a, b) * speedp;
        t += Time.deltaTime * rateVelocity;
        if (t < 1.0f)
        {
            transform.position = Parabola(t, a, pmax, b);
        }
        else
        {
            transform.position = b;
            t = 1;
        }
        if (t > 0.75f)
        {
            r += Time.deltaTime * rateVelocity * 4;
            if (r < 1.0f)
            {
                sprite.color = Color.Lerp(Color.white, Color.red, r);
            }
            else
            {
                sprite.color = Color.white;
                r = 1;
            }
        }
    }
    private Vector2 Parabola(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = Vector2.Lerp(a, b, t);
        var bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }
}