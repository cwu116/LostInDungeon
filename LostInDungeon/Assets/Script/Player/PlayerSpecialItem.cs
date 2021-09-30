using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string,SpecialItem> specialDict;
    public Dictionary<string,SpecialItem> specialDictAlways;
    public Dictionary<string,SpecialItem> specialDictAttack;
    public Dictionary<string,SpecialItem> specialDictSkill;
    void Start()
    {
        specialDict = new Dictionary<string, SpecialItem>();
        specialDictAlways = new Dictionary<string, SpecialItem>();
        specialDictAttack = new Dictionary<string, SpecialItem>();
        specialDictSkill  = new Dictionary<string, SpecialItem>();
    }

    public void ItemsEffectAttack()
    {

        foreach (var item in specialDictAttack)
        {

            item.Value.ItemEffect();
        }
    }

    public void ItemsEffectSkill()
    {

        foreach (var item in specialDictSkill)
        {

                item.Value.ItemEffect();
        }
     }

    public void ItemsEffectAlways()
    {

        foreach (var item in specialDictAlways)
        {

            item.Value.ItemEffect();
        }
    }

    public void AddItem(SpecialItem item)
    {
        switch (item.itemType)
        {
            case ItemType.oneTime:
                specialDict.Add(item.name, item);
                break;
            case ItemType.attack:
                specialDictAttack.Add(item.name, item);
                break;
            case ItemType.skill:
                specialDictSkill.Add(item.name, item);
                break;
            case ItemType.always:
                specialDictAlways.Add(item.name, item);
                break;
        }

        Debug.Log(item.name);
        
    }
}
