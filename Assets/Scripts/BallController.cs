using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float rateVelocity;
    public float speed;
    public float speedp;
    public float t;
    bool endball;
    float count;
    float angle;
    Vector3 dir;
    public enum State { Set, Atack, Hit, Rebound, Defense };
    public State state;
    [SerializeField]
    GameObject start, end, player, puppymanager;
    Vector2 initpos;
    [SerializeField]
    Vector2 pmax;
    [SerializeField]
    Sprite[] ballshape;


    private void Start()
    {
        state = State.Set;
        initpos = transform.position;
        endball = false;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Set:
                if (!endball)
                {
                    if (player.transform.position.x < 3.198f)
                    {
                        end.transform.position = new Vector3(1f + player.transform.position.x, player.transform.position.y);
                    }
                    else
                    {
                        Debug.Log("Funciona");
                        end.transform.position = new Vector2(4.198f, player.transform.position.y);
                    }
                    endball = true;
                }
                if (puppymanager.GetComponent<PuppyManager>().puppys.Count >= 1)
                {
                    MovimientoParab(initpos, end.transform.position);
                }
                gameObject.GetComponent<SpriteRenderer>().sprite = ballshape[0];
                break;
            case State.Atack:
                start.transform.position = transform.position;
                end.transform.position = puppymanager.GetComponent<PuppyManager>().ObjBall();
                Movimiento(start.transform.position, end.transform.position);
                gameObject.GetComponent<SpriteRenderer>().sprite = ballshape[1];
                dir = end.transform.position - transform.position;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case State.Hit:
                start.transform.position = transform.position;
                end.transform.position = new Vector2(8.424f, 3.458f);
                Movimiento(start.transform.position, end.transform.position);
                if (transform.position == end.transform.position)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = ballshape[2];
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = ballshape[1];
                }
                dir = end.transform.position - transform.position;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case State.Rebound:
                if (!endball)
                {
                    var x = Random.Range(0.752f, 4.198f);
                    var y = Random.Range(1.023f, 3.028f);
                    end.transform.position = new Vector2(x, y);
                    endball = true;
                }
                start.transform.position = transform.position;
                MovimientoParab(start.transform.position, end.transform.position);
                gameObject.GetComponent<SpriteRenderer>().sprite = ballshape[0];
                break;
            case State.Defense:
                start.transform.position = transform.position;
                end.transform.position = new Vector2(2.957f, 3.761f);
                MovimientoParab(start.transform.position, end.transform.position);
                gameObject.GetComponent<SpriteRenderer>().sprite = ballshape[0];
                break;
        }
        if(transform.position == new Vector3(2.957f, 3.761f))
        {
            t = 0;
            state = State.Set;
            end.GetComponentInChildren<SpriteRenderer>().enabled = true;
        } 
        else if (transform.position == new Vector3(8.424f, 3.458f))
        {
            t = 0;
            state = State.Rebound;
            end.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Set)
        {
            if (collision.name == "Player")
            {
                if(collision.GetComponent<PlayerController>().atacking == true)
                {
                    state = State.Atack;
                    endball = false;
                    collision.GetComponent<PlayerController>().atacking = false;
                    end.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }
        }
        if (state == State.Rebound)
        {
            if (collision.name == "Player")
            {
                if (collision.GetComponent<PlayerController>().atacking == false)
                {
                    state = State.Defense;
                    endball = false;
                    t = 0;
                    collision.GetComponent<PlayerController>().atacking = true;
                    end.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }
        }
        if(state == State.Atack)
        {
            if (collision.CompareTag("Puppy"))
            {
                state = State.Hit;
                collision.GetComponent<PuppyController>().ballhit = true;
            }
        }
    }
    void Movimiento(Vector2 a, Vector2 b)
    {
        transform.position = Vector2.MoveTowards(a, b, speed * Time.deltaTime);
    }
    void MovimientoParab(Vector2 a, Vector2 b)
    {
        /*var x = ((b.x - a.x) / 2f) + a.x;
        var y = b.y + 5f;
        pmax = new Vector2(x, y);*/
        pmax = b - a;
        pmax.y = pmax.y + 5f;
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
