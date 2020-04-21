using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2 : MonoBehaviour
{
    public float rateVelocity;
    public float speed;
    public float speedp;
    public float t;
    [SerializeField]
    GameObject start, end, ball3;
    Vector2 initpos;
    [SerializeField]
    Vector2 pmax;


    private void Start()
    {
        start = GameObject.Find("Start");
        end = GameObject.Find("End");
        start.transform.position = transform.position;
        var x = Random.Range(0.752f, 4.198f);
        var y = Random.Range(1.023f, 3.028f);
        end.transform.position = new Vector2(x, y);
    }
    private void Update()
    {
        MovimientoParab(start.transform.position, end.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Instantiate(ball3, transform.position, Quaternion.identity);
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
        }
    }
    private Vector2 Parabola(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = Vector2.Lerp(a, b, t);
        var bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }
}
