using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float hitCD;
    float dmg;
    public KnightController player;
    Transform knightPosition;

    private void Start()
    {
        hitCD = 0.3f;
        knightPosition = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
       hitCD += Time.deltaTime;
       dmg = player.attackDmg;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (hitCD > 0.3f)
            {
                Vector3 diff = (collision.transform.position - knightPosition.position);
                Debug.Log("hit monster "+diff);
                collision.transform.position = collision.transform.position + diff /2;
                hitCD = 0;

                if (player.curEnergy + 10 < 100)
                {
                    player.curEnergy += 10;
                }
                else
                {
                    player.curEnergy = 100;
                }

            }

        }
    }
}
