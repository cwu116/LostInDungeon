using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour
{
    float hitCD;
    public int hp;
    public Dictionary<String,GameObject> itemDict;
    public Transform PrefabManager;
    Animator anim;
    BoxCollider2D collider;

    public GameObject parent;
    public BoxCollider2D parentCollider;
    

    private void Start()
    {
        hitCD = 0.3f;
        hp = 3;
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        itemDict = new Dictionary<String, GameObject>();
        itemDict.Add("CoinGold", Resources.Load("CoinGold") as GameObject);
        itemDict.Add("CoinSliver", Resources.Load("CoinSliver") as GameObject);
        itemDict.Add("CoinCopper", Resources.Load("CoinCopper") as GameObject);
        itemDict.Add("EnergyPotion", Resources.Load("EnergyPotion") as GameObject);
        itemDict.Add("Heart", Resources.Load("Heart") as GameObject);
        itemDict.Add("HalfHeart", Resources.Load("HalfHeart") as GameObject);
        itemDict.Add("Key", Resources.Load("Key") as GameObject);
    }

    private void Update()
    {
        hitCD += Time.deltaTime;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            if (hitCD > 0.3f)
            {
                hitCD = 0;

                if (hp > 1)
                    hp--;
                else
                {
                    anim.SetTrigger("Break");
                    collider.enabled = false;
                    parentCollider.enabled = false;
                    StartCoroutine(CreateItem(0.4f));
                    
                }
            }

        }
    }
  
    

    IEnumerator DestoryPot(float timer)
    {
        yield return new WaitForSeconds(timer); 
        Destroy(this.gameObject);
        Destroy(parent);
    }

    IEnumerator CreateItem(float timer)
    {
        yield return new WaitForSeconds(timer);
        GenerateItem();
    }


    private void GenerateItem()
    {
        int rand = UnityEngine.Random.Range(1,101);
        if(rand <= 3)
        {
            GameObject item = Instantiate(itemDict["CoinGold"], PrefabManager );
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        else if (rand > 3 && rand <= 8 )
        {
            GameObject item = Instantiate(itemDict["CoinSliver"], PrefabManager);
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }


        else if (rand > 8 && rand <= 18)
        {
            GameObject item = Instantiate(itemDict["EnergyPotion"], PrefabManager);
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        else if (rand > 18 && rand <= 23)
        {
            GameObject item = Instantiate(itemDict["Heart"], PrefabManager);
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        else if (rand > 23 && rand <= 33)
        {
            GameObject item = Instantiate(itemDict["HalfHeart"], PrefabManager);
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        else if (rand > 33 && rand <= 36)
        {
            GameObject item = Instantiate(itemDict["Key"], PrefabManager);
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        else if (rand > 36 && rand <= 51)
        {
            GameObject item = Instantiate(itemDict["CoinCopper"], PrefabManager);
            item.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }

        else
        {
            Debug.Log("没掉" + rand);

        }



    }
}
