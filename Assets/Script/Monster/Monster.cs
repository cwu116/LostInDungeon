using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string MonsterName;
    public float moveSpeed;
    public float maxHp;
    public float curHp;
    Transform target;
    Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
    }
}
