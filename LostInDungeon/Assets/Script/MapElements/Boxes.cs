using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Boxes : MonoBehaviour
{
    // Start is called before the first frame update
    public KnightController player;
    public Dictionary<string, GameObject> itemDict;
    public PlayerSpecialItem playerItem;
    public GameObject parent;
    public BoxCollider2D collider;
    public Animator anim;
    public bool isOpen;
    public bool canOpen;
    void Start()
    {
        isOpen = false;
        itemDict = new Dictionary<string, GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        playerItem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpecialItem>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        itemDict.Add("CoinGold", Resources.Load("CoinGold") as GameObject);
        itemDict.Add("CoinSliver", Resources.Load("CoinSliver") as GameObject);
        itemDict.Add("CoinCopper", Resources.Load("CoinCopper") as GameObject);
        itemDict.Add("EnergyPotion", Resources.Load("EnergyPotion") as GameObject);
        itemDict.Add("Heart", Resources.Load("Heart") as GameObject);
        itemDict.Add("HalfHeart", Resources.Load("HalfHeart") as GameObject);
        itemDict.Add("Key", Resources.Load("Key") as GameObject);
    }

    // Update is called once per frame

    private void Update()
    {
        if (isOpen)
            return;

        if (canOpen)
        {
           
                if (Input.GetKeyDown(KeyCode.E))
                {
                    switch (this.gameObject.tag)
                    {
                        case "WoodBox":
                            if (player.keyNum >= 1)
                            {
                                isOpen = true;
                                player.keyNum--;
                                anim.SetTrigger("Open");
                            }
                            break;
                    }
                }
            }
        
    } 
    IEnumerator DestoryBox(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
        Destroy(parent);
    }

    public virtual void GenerateItem()
    {
        int itemNum = UnityEngine.Random.Range(1, 11);

        if (itemNum <= 5)
        {
            GameObject item = RandomItem();
            ItemDrop(item);

        }

        else if (itemNum > 5 && itemNum <= 9)
        {
            GameObject item1 = RandomItem();
            ItemDrop(item1);

            GameObject item2 = RandomItem();
            ItemDrop(item2);
        }
        else
        {
            GameObject item11 = RandomItem();
            ItemDrop(item11);

            GameObject item22 = RandomItem();
            ItemDrop(item22);

            GameObject item33 = RandomItem();
            ItemDrop(item33);


        }
    }

    public void ItemDrop(GameObject item)
    {
        int xLeftOrRight = UnityEngine.Random.Range(0, 2);
        float xMove;
        if (xLeftOrRight == 1)
        {
             xMove = UnityEngine.Random.Range(10, 26) / 10;
        }
        else
        {
             xMove = UnityEngine.Random.Range(-26, -10) / 10;
        }
        float yMove = 0.3f * Mathf.Abs(xMove);
        item.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        item.transform.DOMoveX(transform.position.x + xMove, 0.6f);
        item.transform.DOMoveY(transform.position.y + yMove, 0.3f);
        item.transform.DOMoveY(transform.position.y + yMove, 0.3f).OnComplete(() => {
            item.transform.DOMoveY(transform.position.y , 0.4f);
        });
    }

    public virtual GameObject RandomItem()
    {
        int rand = UnityEngine.Random.Range(1, 101);
        if (rand <= 5)
        {
            GameObject item = Instantiate(itemDict["CoinGold"]);
            return item;
        }

        else if (rand > 5 && rand <= 20)
        {
            GameObject item = Instantiate(itemDict["CoinSliver"]);
            return item;
        }

        else if (rand > 20 && rand <= 35)
        {
            GameObject item = Instantiate(itemDict["CoinCopper"]);
            return item;
        }

        else if (rand > 35 && rand <= 55)
        {
            GameObject item = Instantiate(itemDict["Heart"]);
            return item;
        }

        else if (rand > 55 && rand <= 85)
        {
            GameObject item = Instantiate(itemDict["HalfHeart"]);
            return item;
        }
        else
        {
            GameObject item = Instantiate(itemDict["Key"]);
            return item;
        }
    }
}
