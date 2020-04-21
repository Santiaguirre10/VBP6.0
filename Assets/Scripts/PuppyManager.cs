using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyManager : MonoBehaviour
{
    public List<GameObject> puppys;
    public List<float> xpuppys;
    public Transform[] puppyspamer;
    public GameObject[] puppytype;
    public float minx;
    [SerializeField]
    GameObject objball;

    // Start is called before the first frame update
    void Start()
    {
        minx = 8.8f;
    }
    // Update is called once per frame
    void Update()
    {
        ObjBall();
    }
    private void OnEnable()
    {
        InvokeRepeating("PuppyCreator", 0, 5);
    }
    void PuppyCreator()
    {
        int rdm = Random.Range(0, 3);
        int rdmp = Random.Range(0, 4);
        Instantiate(puppytype[rdmp], puppyspamer[rdm].transform.position, Quaternion.identity);
    }
    public GameObject ObjBall()
    {
        for (int i = 0; i < puppys.Count; i++)
        {
            if (minx > puppys[i].transform.position.x)
            {
                minx = puppys[i].transform.position.x;
            }
        }
        for (int j = 0; j < puppys.Count; j++)
        {
            if (minx == puppys[j].transform.position.x)
            {
                objball = puppys[j];
            }
        }
        return objball;
    }
}
