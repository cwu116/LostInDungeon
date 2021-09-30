using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    // Start is called before the first frame update
    float up;
    float down;
    bool moveUp;
    KnightController player;
    void Start()
    {
        up = transform.position.y + 0.1f;
        down = transform.position.y - 0.1f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < up && transform.position.y < down)
        {
            moveUp = true;
        }

        if(transform.position.y > up){
            moveUp = false;
        }

        if (moveUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 0.1f, transform.position.z);
        }
        else {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 0.1f, transform.position.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (this.gameObject.tag)
            {
                case "EnergyPotion":
                    if (player.curEnergy + 40 > player.maxEnergy)
                    {
                        player.curEnergy = player.maxEnergy;
                    }
                    else
                    {
                        player.curEnergy += 40;
                    }
                    Destroy(this.gameObject);
                    break;

                case "Heart":
                    if (player.curHp == player.maxHp)
                        break;
                    if (player.curHp + 1 > player.maxHp)
                    {
                        player.curHp = player.maxHp;
                    }
                    else
                    {
                        player.curHp ++;
                    }
                    Destroy(this.gameObject);
                    break;

                case "HalfHeart":
                    if (player.curHp == player.maxHp)
                        break;

                    if (player.curHp + 0.5f >= player.maxHp)
                    {
                        player.curHp = player.maxHp;
                    }
                    else
                    {
                        player.curHp += 0.5f;
                    }
                    Destroy(this.gameObject);
                    break;

                case "Key":
                    player.keyNum++;
                    Destroy(this.gameObject);
                    break;




            }
           
        }
    }
}
