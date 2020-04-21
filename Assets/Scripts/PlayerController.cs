using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField]
    FloatingJoystick joystick;
    [SerializeField]
    SpriteRenderer sprite;
    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LimitedArea();
        h = joystick.Horizontal;
        v = joystick.Vertical;
    }
    void Move()
    {
        Vector2 movement = new Vector2(h, v);
        transform.Translate(movement * speed * Time.deltaTime);
        if (h < 0)
        {
            sprite.flipX = true;
        }
        if (h > 0)
        {
            sprite.flipX = false;
        }
    }
    void LimitedArea()
    {
        if (transform.position.x <= 0.752f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.755f, transform.position.y), speed * Time.deltaTime);
        }
        if (transform.position.x >= 4.198f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(4.195f, transform.position.y), speed * Time.deltaTime);
        }
        if (transform.position.y <= 1.023f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 1.025f), speed * Time.deltaTime);
        }
        if (transform.position.y >= 3.028f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 3.025f), speed * Time.deltaTime);
        }
    }
}