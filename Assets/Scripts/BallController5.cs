using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController5 : MonoBehaviour
{
    float rateVelocity;
    public float speedp;
    public float t;
    [SerializeField]
    GameObject end, ball1;
    Vector2 initpos;
    [SerializeField]
    Vector2 pmax;

    private void Start()
    {
        t = 0;
        end = GameObject.Find("End");
        end.transform.position = new Vector2(2.946f, 3.709f);
        end.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
    private void Update()
    {
        MovimientoParab(transform.position, end.transform.position);
        if (transform.position == end.transform.position)
        {
            Instantiate(ball1, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void MovimientoParab(Vector2 a, Vector2 b)
    {
        var x = ((b.x - a.x) / 2f) + a.x;
        var y = b.y + 5f;
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
    }
    private Vector2 Parabola(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = Vector2.Lerp(a, b, t);
        var bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }
}