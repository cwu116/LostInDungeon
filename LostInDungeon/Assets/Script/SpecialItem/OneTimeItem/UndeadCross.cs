using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndeadCross : SpecialItem
{
    // Start is called before the first frame update
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "不死十字架";
        description = "玩家死亡时，不会死亡并恢复所有生命值，仅限一次，请珍惜生命";
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
        player.curHp = player.maxHp;
        player.curState = PlayerState.Move;
        playerList.specialDict.Remove(this.name);
    }

    private void Effect()
    {
       
    }


}
