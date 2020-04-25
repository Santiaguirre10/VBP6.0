using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField]
    GameObject check;
    [SerializeField]
    FloatingJoystick joystick;
    [SerializeField]
    SpriteRenderer sprite;
    float h;
    float v;

    public Vector3 maxJump;
    public Vector3 groundPos;
    public float jumpSpeed;
    public float fallSpeed;
    public bool inputJump = false;
    public bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        h = joystick.Horizontal;
        v = joystick.Vertical;
        LimitedAreaX();
        if (GameObject.FindGameObjectWithTag("Ball1") != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    groundPos = transform.position;
                    if (transform.position.x <= 3.198f)
                    {
                        maxJump = transform.position + maxJump;
                        check.transform.position = new Vector3(groundPos.x + 1f, groundPos.y - 0.344f);
                    }
                    else
                    {
                        maxJump = new Vector3(transform.position.x, transform.position.y + maxJump.y);
                        check.transform.position = new Vector3(groundPos.x, groundPos.y - 0.344f);
                    }
                    grounded = false;
                    groundPos = transform.position;
                    inputJump = true;
                    StartCoroutine("Jump");
                    
                }
            }
        }
        if (grounded)
        {
            check.transform.position = new Vector3(transform.position.x, transform.position.y - 0.344f);
            Move();
            LimitedAreaY();
        }
    }
    IEnumerator Jump()
    {
        while (true)
        {
            sprite.flipX = false;
            if (transform.position.y >= maxJump.y)
                inputJump = false;
            if (inputJump)
                transform.position = Vector3.MoveTowards(transform.position, maxJump, jumpSpeed * Time.smoothDeltaTime);
            else if (!inputJump)
            {
                if (groundPos.x <= 3.198f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(groundPos.x + 1f, groundPos.y), fallSpeed * Time.smoothDeltaTime);
                    if (transform.position == new Vector3(groundPos.x + 1f, groundPos.y))
                    {
                        maxJump = new Vector3(0.5f, 1.5f);
                        StopAllCoroutines();
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(groundPos.x, groundPos.y), fallSpeed * Time.smoothDeltaTime);
                    if (transform.position == groundPos)
                    {
                        maxJump = new Vector3(0.5f, 1.5f);
                        StopAllCoroutines();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "GroundedCheck")
        {
            grounded = true;
        }
    }
    void LimitedAreaY()
    {
        if (transform.position.y <= 1.023f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 1.023f), speed * Time.deltaTime);
        }
        if (transform.position.y >= 3.028f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 3.028f), speed * Time.deltaTime);
        }
    }
    void LimitedAreaX()
    {
        if (transform.position.x <= 0.752f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.752f, transform.position.y), speed * Time.deltaTime);
        }
        if (transform.position.x >= 4.198f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(4.198f, transform.position.y), speed * Time.deltaTime);
        }
    }
}