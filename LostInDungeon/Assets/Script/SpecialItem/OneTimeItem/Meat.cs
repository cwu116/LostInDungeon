using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meat : SpecialItem
{
    // Start is called before the first frame update
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
   
    public override void ItemEffect()
    {
        
    }

    private void Effect()
    {
        player.maxHp++;
        player.curHp += 1;
        //Destroy(this.gameObject);
    }

    void Start()
    {
        desname = "火鸡肉";
        description = "红心容器和血量增加一, 饱餐一顿";
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

}
