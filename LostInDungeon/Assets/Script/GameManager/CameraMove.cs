using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed;
    public Transform target;
    public static CameraMove cameramove;
    // Start is called before the first frame update
    private void Awake()
    {
        cameramove = this;
    }
    private void Update()
    {
        if (target != null)
        transform.position = Vector3.MoveTowards(transform.position,  new Vector3 (target.position.x, target.position.y,transform.position.z),speed * Time.deltaTime);
    }
}
