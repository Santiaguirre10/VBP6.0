using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController3 : MonoBehaviour
{
    float count;
    float angle;
    Vector3 dir;
    public float speed;
    Vector2 initpos;
    [SerializeField]
    GameObject end, puppymanager, ball4;
    [SerializeField]
    Sprite[] ballshape;
    SpriteRenderer sprite;
    Vector3 rebound;
    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("End");
        puppymanager = GameObject.Find("PuppyManager");
        sprite = GetComponent<SpriteRenderer>();
        rebound = new Vector2(8.424f, 3.458f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, rebound, speed * Time.deltaTime);
        dir = rebound - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (transform.position == rebound)
        {
            sprite.sprite = ballshape[0];
            end.GetComponentInChildren<SpriteRenderer>().enabled = true;
            Instantiate(ball4, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
