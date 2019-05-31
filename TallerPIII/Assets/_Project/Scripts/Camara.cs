
using UnityEngine;

public class Camara : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed;

    public Vector3 offset;

    // Update is called once per frame

    void FixedUpdate()
    {

        Vector3 desirePosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed);
        transform.position = smoothedPosition;


       
    }
}
