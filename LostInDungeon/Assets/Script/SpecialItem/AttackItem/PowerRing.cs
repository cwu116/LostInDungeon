using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerRing : SpecialItem
{
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "力量戒指";
        description = "血量满时，攻击力提高，请保持血量"; 
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
        if(player.curHp == player.maxHp)
        {
            player.attackDmg = 25;
        }
        else
        {
            player.attackDmg = 10;
        }
    }

    private void Effect()
    {

    }

    
}
