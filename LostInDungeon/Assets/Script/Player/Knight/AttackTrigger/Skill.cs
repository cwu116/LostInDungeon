using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    float hitCD;
    float dmg;
    KnightController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        hitCD = 0.3f;
    }

    private void Update()
    {
        hitCD += Time.deltaTime;
        dmg = player.skillDamage;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (hitCD > 0.3f)
            {
                Vector3 diff = (collision.transform.position - transform.position);
                Debug.Log("hit monster " + diff);
                collision.transform.position = collision.transform.position + diff / 2;
                hitCD = 0;
            }
        }
    }
}
