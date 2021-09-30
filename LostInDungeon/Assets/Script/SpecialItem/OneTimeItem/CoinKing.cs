using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinKing : SpecialItem
{
    // Start is called before the first frame update
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    // Update is called once per frame
    void Start()
    {
        desname = "超级硬币";
        description = "可以获得100枚硬币，好椰";
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
        player.coinNum += 100;
        //Destroy(this.gameObject);
    }

    
}
