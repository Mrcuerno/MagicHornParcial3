using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Vector2 target;
    public float Speed;

    // Start is called before the first frame update
    

    private void Start()
    {
        target = new Vector2(6.36f, -1.69f);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed* Time.deltaTime);
    }

    // Update is called once per frame

}
