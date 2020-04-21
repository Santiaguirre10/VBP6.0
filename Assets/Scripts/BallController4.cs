using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController4 : MonoBehaviour
{

    float count;
    float angle;
    Vector3 dir;
    public float speed;
    [SerializeField]
    GameObject start, end, player, puppymanager, ball2;
    [SerializeField]
    Sprite[] ballshape;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("Start");
        end = GameObject.Find("End");
        player = GameObject.Find("Player");
        puppymanager = GameObject.Find("PuppyManager");
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        start.transform.position = transform.position;
        end.transform.position = new Vector2(8.424f, 3.458f);
        Movimiento(start.transform.position, end.transform.position);
        dir = end.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (transform.position == end.transform.position)
        {
            sprite.sprite = ballshape[2];
        }
        else
        {
            sprite.sprite = ballshape[1];
        }

        if (transform.position == new Vector3(8.424f, 3.458f))
        {
            end.GetComponentInChildren<SpriteRenderer>().enabled = true;
            Instantiate(ball2, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void Movimiento(Vector2 a, Vector2 b)
    {
        transform.position = Vector2.MoveTowards(a, b, speed * Time.deltaTime);
    }
}
