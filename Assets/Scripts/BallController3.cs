using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController3 : MonoBehaviour
{
    public float rateVelocity;
    public float speed;
    public float speedp;
    public float t;
    [SerializeField]
    GameObject start, end, ball1;
    Vector2 initpos;
    [SerializeField]
    Vector2 pmax;


    private void Start()
    {
        start = GameObject.Find("Start");
        end = GameObject.Find("End");
        start.transform.position = transform.position;
        end.transform.position = new Vector2(2.711f, 3.635f);
    }
    private void Update()
    {
        MovimientoParab(start.transform.position, end.transform.position);
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
        //t += Time.deltaTime * rateVelocity;
        if (Vector2.Distance(a, b) >= 3)
        {
            t += Time.deltaTime * (speedp / 2);
        }
        else
        {
            t += Time.deltaTime * speedp;
        }
        if (t < 1.0f)
        {
            transform.position = Parabola(t, a, pmax, b);
        }
        else
        {
            transform.position = b;
            
        }
    }
    private Vector2 Parabola(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = Vector2.Lerp(a, b, t);
        var bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }
}
