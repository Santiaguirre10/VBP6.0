using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyController : MonoBehaviour
{
    Vector2 pmax;
    float rateVelocity;
    public float speed;
    public float speedp;
    float t;
    int life;
    public enum State { Zone, Escape, Kicked };
    public State state;
    bool addpuppy;
    public bool ballhit;
    GameObject gameover;
    Vector3 initpos;
    [SerializeField]
    GameObject puppymanager;

    void Start()
    {
        life = 3;
        puppymanager = GameObject.Find("PuppyManager");
        gameover = GameObject.Find("GameOver");
        state = State.Zone;
        initpos = transform.position; 
    }
    void Update()
    {
        switch (state)
        {
            case State.Zone:
                if (life > 0) {
                    if (!addpuppy)
                    {
                        puppymanager.GetComponent<PuppyManager>().puppys.Add(gameObject);
                        puppymanager.GetComponent<PuppyManager>().xpuppys.Add(transform.position.x);
                        puppymanager.GetComponent<PuppyManager>().minx = 8.8f;
                        addpuppy = true;
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case State.Escape:
                transform.position = Vector2.MoveTowards(transform.position, gameover.transform.position, speed * Time.deltaTime);
                break;
            case State.Kicked:
                MovimientoParab(transform.position, initpos);
                break;
        }
        if (transform.position == initpos)
        {
            state = State.Zone;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            life--;
            state = State.Kicked;
        }
        if (collision.CompareTag("Fence"))
        {
            if (collision.transform.position.y == initpos.y)
            {
                state = State.Escape;
                addpuppy = false;
                puppymanager.GetComponent<PuppyManager>().puppys.Remove(gameObject);
                puppymanager.GetComponent<PuppyManager>().xpuppys.Remove(transform.position.x);
                puppymanager.GetComponent<PuppyManager>().minx = 8.8f;
            } 
        }
        if (collision.name == "Ball")
        {
            if (transform.position == puppymanager.GetComponent<PuppyManager>().ObjBall()) {
                addpuppy = false;
                puppymanager.GetComponent<PuppyManager>().puppys.Remove(gameObject);
                puppymanager.GetComponent<PuppyManager>().xpuppys.Remove(transform.position.x);
                puppymanager.GetComponent<PuppyManager>().minx = 8.8f;
                state = State.Kicked;
            }
        }        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    public void MovimientoParab(Vector2 a, Vector2 b)
    {
        pmax = new Vector2((b.x - a.x) / 2 + a.x, b.y + 1.5f);
        rateVelocity = 1f / Vector2.Distance(a, b) * speed;
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
            t = 0;
        }
    }
    Vector2 Parabola(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }
}
