using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSeeingEye : SpecialItem
{
    KnightController player;
    PlayerSpecialItem playerList;
    BoxCollider2D collider;
    SpriteRenderer render;
    MapGenerator mapManager;
    // Update is called once per frame
   

    public override void ItemEffect()
    {
       for(int i = 0; i <mapManager.rooms.GetLength(0); i ++)
        {
            for (int j = 0; j < mapManager.rooms.GetLength(1); j++)
                if (mapManager.rooms[i, j] != null)
                {
                    mapManager.rooms[i, j].background.active = true;                
                }           
        }

        foreach (KeyValuePair<string, GameObject> wall in mapManager.walls)
        {
            wall.Value.GetComponent<ShowRoomInMap>().wall.gameObject.active= true;
            wall.Value.GetComponent<ShowRoomInMap>().wall2.gameObject.active= true;
        }
    }

    private void Effect()
    {

    }

    void Start()
    {
        desname = "全视之眼";
        description = "可以看到全部地图，但是你并不是全知全能的";
        itemType = ItemType.always;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        playerList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpecialItem>();
        collider = transform.GetComponent<BoxCollider2D>();
        render = transform.GetComponent<SpriteRenderer>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapGenerator>();
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
