using System.Collections.Generic;
using UnityEngine;

public class SpecialBox : Boxes
{
    // Start is called before the first frame update
    public Dictionary<string, SpecialItem> specialDict;
    public Dictionary<string, SpecialItem> specialDictAlways;
    public Dictionary<string, SpecialItem> specialDictAttack;
    public Dictionary<string, SpecialItem> specialDictSkill;
    public SpecialBoxManager specialBoxManager;

    void Start()
    {
        isOpen = false;
        specialBoxManager = GameObject.FindGameObjectWithTag("SpecialBoxManager").GetComponent<SpecialBoxManager>();
        itemDict = specialBoxManager.specialDict;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        playerItem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpecialItem>();
        specialDict = playerItem.specialDict;
        specialDictAlways = playerItem.specialDictAlways;
        specialDictAttack = playerItem.specialDictAttack;
        specialDictSkill = playerItem.specialDictSkill;       
    }

    // Update is called once per frame
    public override void GenerateItem()
    {
        GameObject item = RandomItem();
        ItemDrop(item);
    }

    public override GameObject RandomItem()
    {
        
        GameObject item = null;
        bool isDuplicate = false;
        int count = 0;
        int rand = -1;
        while (item == null && count < 2000)
        {
            rand = UnityEngine.Random.Range(1, 14);
            count++;
            switch (rand)
            {
                case 1:
                    if (itemDict.ContainsKey("AllSeeingEye"))
                    {
                        item = Instantiate(itemDict["AllSeeingEye"]);
                        itemDict.Remove("AllSeeingEye");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 2:
                    if (itemDict.ContainsKey("BloodCost"))
                    {
                        item = Instantiate(itemDict["BloodCost"]);
                        itemDict.Remove("BloodCost");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 3:
                    if (itemDict.ContainsKey("CoinKing"))
                    {
                        item = Instantiate(itemDict["CoinKing"]);
                        itemDict.Remove("CoinKing");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 4:
                    if (itemDict.ContainsKey("EmptyHeart"))
                    {
                        item = Instantiate(itemDict["EmptyHeart"]);
                        itemDict.Remove("EmptyHeart");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 5:
                    if (itemDict.ContainsKey("EnergyBeads"))
                    {
                        item = Instantiate(itemDict["EnergyBeads"]);
                        itemDict.Remove("EnergyBeads");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 6:
                    if (itemDict.ContainsKey("GoldenKey"))
                    {
                        item = Instantiate(itemDict["GoldenKey"]);
                        itemDict.Remove("GoldenKey");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 7:
                    if (itemDict.ContainsKey("GoldenSword"))
                    {
                        item = Instantiate(itemDict["GoldenSword"]);
                        itemDict.Remove("GoldenSword");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 8:
                    if (itemDict.ContainsKey("Meat"))
                    {
                        item = Instantiate(itemDict["Meat"]);
                        itemDict.Remove("Meat");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 9:
                    if (itemDict.ContainsKey("PigHead"))
                    {
                        item = Instantiate(itemDict["PigHead"]);
                        itemDict.Remove("PigHead");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 10:
                    if (itemDict.ContainsKey("PowerRing"))
                    {
                        item = Instantiate(itemDict["PowerRing"]);
                        itemDict.Remove("PowerRing");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 11:
                    if (itemDict.ContainsKey("SpeedShoes"))
                    {
                        item = Instantiate(itemDict["SpeedShoes"]);
                        itemDict.Remove("SpeedShoes");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 12:
                    if (itemDict.ContainsKey("TripleAttack"))
                    {
                        item = Instantiate(itemDict["TripleAttack"]);
                        itemDict.Remove("TripleAttack");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
                case 13:
                    if (itemDict.ContainsKey("UnDeadCross"))
                    {
                        item = Instantiate(itemDict["UnDeadCross"]);
                        itemDict.Remove("UnDeadCross");
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }
                    break;
            }
          
        }
        if (!item) { Debug.LogError("wrong rand:" + rand); }
        
        return item;
    }
}
