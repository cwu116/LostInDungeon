using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    float count;
    Rigidbody2D rb;
    public int Speed;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        rb.velocity = -transform.right * Speed;
        if (count > 1.5)
        {
            count = 0;
            Destroy(gameObject);
        }
    }

}
