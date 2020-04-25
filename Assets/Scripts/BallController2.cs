using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2 : MonoBehaviour
{
    public float speed;
    float count;
    float angle;
    Vector3 dir;
    [SerializeField]
    GameObject puppymanager, ball3;
    Vector2 initpos;


    private void Start()
    {
        
        puppymanager = GameObject.Find("PuppyManager");
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puppymanager.GetComponent<PuppyManager>().ObjBall().transform.position, speed * Time.deltaTime);
        dir = puppymanager.GetComponent<PuppyManager>().ObjBall().transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Puppy"))
        {
            Instantiate(ball3, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
