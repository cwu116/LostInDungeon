using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripleAttack : SpecialItem
{
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "三重攻击";
        description = "释放的技能变为三方向， 三个总比一个好";
        itemType = ItemType.skill; 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        playerList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpecialItem>();
        collider = transform.GetComponent<BoxCollider2D>();
        render = transform.GetComponent<SpriteRenderer>();
    }

    public override void ItemEffect()
    {
        GameObject attack1 = Instantiate(player.knightAttack, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject attack2 = Instantiate(player.knightAttack, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        attack1.transform.up = Quaternion.Euler(0, 0, - 60) * player.weapon.up;
        attack2.transform.up = Quaternion.Euler(0, 0, - 120) * player.weapon.up;
    }

    private void Effect()
    {
 
    }

    private void Update()
    {
        if (isPickAble)
        {
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.ShowCollectInfo(this.gameObject);
                playerList.AddItem(this);
                Effect();
                render.enabled = false;
                collider.enabled = false;
                
            }
        }
    }
}
