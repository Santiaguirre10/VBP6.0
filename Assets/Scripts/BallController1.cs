using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController1 : MonoBehaviour
{
    public float rateVelocity;
    public float speedp;
    public float t;
    public float r;
    [SerializeField]
    GameObject start, end, player, puppymanager, ball2, ball6;
    [SerializeField]
    Vector2 pmax;
    public bool hitball;
    Vector2 initpos;
    public Color lerpedcolor;
    [SerializeField]
    SpriteRenderer sprite;

    private void Start()
    {
        t = 0;
        sprite.color = Color.white;
        start = GameObject.Find("Start");
        end = GameObject.Find("End");
        player = GameObject.Find("Player");
        puppymanager = GameObject.Find("PuppyManager");
        end.GetComponentInChildren<SpriteRenderer>().enabled = true;
        initpos = transform.position;
        if (player.transform.position.x < 3.198f)
        {
            end.transform.position = new Vector3(1f + player.transform.position.x, player.transform.position.y);
        }
        else
        {
            end.transform.position = new Vector2(3.198f, player.transform.position.y);
        }
        hitball = false;
    }
    private void Update()
    {
        if (puppymanager.GetComponent<PuppyManager>().puppys.Count >= 1)
        {
            MovimientoParab(initpos, end.transform.position);
        }
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
            if (t < 1f)
            {
                if (collision.GetComponent<PlayerController>().inputJump)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Instantiate(ball2, transform.position, Quaternion.identity);
                        end.GetComponentInChildren<SpriteRenderer>().enabled = false;
                        Destroy(gameObject);
                    }
                }
            }
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (t == 1f)
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
