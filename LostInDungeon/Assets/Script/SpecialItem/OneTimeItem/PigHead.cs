using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigHead : SpecialItem
{
    // Start is called before the first frame update
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "大猪头";
        description = "增加一个红心容器并回复满生命值, 你感觉想睡一觉";
        itemType = ItemType.oneTime;
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

    }

    private void Effect()
    {
        player.maxHp++;
        player.curHp = player.maxHp;
        //Destroy(this.gameObject);
    }

   
}
