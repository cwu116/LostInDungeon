using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodCost : SpecialItem
{
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "血之消耗";
        description = "能量不足时，消耗血量使用技能";
        itemType = ItemType.skill;
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
        if(player.curEnergy < player.skillCost)
        {
            player.curHp -= 0.5f;
        }

        if(player.curHp == 0)
        {
            player.Dead();
        }
    }

    private void Effect()
    {

    }

}
