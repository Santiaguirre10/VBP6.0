using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController1 : MonoBehaviour
{
    public float rateVelocity;
    public float speed;
    public float speedp;
    public float t;
    float count;
    float angle;
    Vector3 dir;
    public enum State { Set, Atack};
    public State state;
    [SerializeField]
    GameObject start, end, player, puppymanager, ball4;
    Vector2 initpos;
    [SerializeField]
    Vector2 pmax;
    [SerializeField]
    Sprite[] ballshape;
    SpriteRenderer sprite;


    private void Start()
    {
        start = GameObject.Find("Start");
        end = GameObject.Find("End");
        player = GameObject.Find("Player");
        puppymanager = GameObject.Find("PuppyManager");
        state = State.Set;
        initpos = transform.position;
        end.GetComponentInChildren<SpriteRenderer>().enabled = true;
        if (player.transform.position.x < 3.198f)
        {
            end.transform.position = new Vector3(1f + player.transform.position.x, player.transform.position.y);
        }
        else
        {
            end.transform.position = new Vector2(4.198f, player.transform.position.y);
        }
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        switch (state)
        {
            case State.Set:
                if (puppymanager.GetComponent<PuppyManager>().puppys.Count >= 1)
                {
                    MovimientoParab(initpos, end.transform.position);
                }
                break;
            case State.Atack:
                start.transform.position = transform.position;
                end.transform.position = puppymanager.GetComponent<PuppyManager>().ObjBall().transform.position;
                Movimiento(start.transform.position, end.transform.position);
                dir = end.transform.position - transform.position;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                sprite.sprite = ballshape[1];
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Set)
        {
            if (collision.name == "Player")
            {
                    state = State.Atack;
                    end.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
        if (state == State.Atack)
        {
            if (collision.CompareTag("Puppy"))
            {
                Destroy(gameObject);
                Instantiate(ball4, transform.position, Quaternion.identity);
            }
        }
    }
    void Movimiento(Vector2 a, Vector2 b)
    {
        transform.position = Vector2.MoveTowards(a, b, speed * Time.deltaTime);
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
            count += Time.deltaTime;
            if (count >= 3)
            {
                state = State.Set;
                count = 0;
                t = 0;
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
