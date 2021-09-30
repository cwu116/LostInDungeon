using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpecialItem : MonoBehaviour
{
    public string desname;
    public string description;
    public ItemType itemType;
    public bool isPickAble;
    // Start is called before the first frame update

    public abstract void ItemEffect();
    
}

public enum ItemType
{
    oneTime,
    attack,
    skill,
    always,
    ult
}
