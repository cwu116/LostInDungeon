using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldenSword : SpecialItem
{
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "黄金之剑";
        description = "攻击的时候有几率释放一次无消耗的技能, 白嫖永远是最香的";
        itemType = ItemType.attack;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        playerList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpecialItem>();
        collider = transform.GetComponent<BoxCollider2D>();
        render = transform.GetComponent<SpriteRenderer>();
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

    public override void ItemEffect()
    {
        int rand = UnityEngine.Random.Range(1, 11);
        if (rand == 1)
        {
            GameObject attack1 = Instantiate(player.knightAttack, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
            attack1.transform.up = Quaternion.Euler(0, 0, -90) * player.weapon.up;
        }
      
    }

    private void Effect()
    {

    }


}
