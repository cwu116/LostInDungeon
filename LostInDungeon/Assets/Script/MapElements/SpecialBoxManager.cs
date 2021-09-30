using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBoxManager : MonoBehaviour
{

    public static SpecialBoxManager BoxManager;
    public Dictionary<string, GameObject> specialDict;
    // Start is called before the first frame update
    void Awake()
    {
        BoxManager = this;
        specialDict = new Dictionary<string, GameObject>();
        specialDict.Add("AllSeeingEye", Resources.Load("SpecialItem/AllSeeingEye") as GameObject);
        specialDict.Add("BloodCost", Resources.Load("SpecialItem/BloodCost") as GameObject);
        specialDict.Add("CoinKing", Resources.Load("SpecialItem/CoinKing") as GameObject);
        specialDict.Add("EmptyHeart", Resources.Load("SpecialItem/EmptyHeart") as GameObject);
        specialDict.Add("EnergyBeads", Resources.Load("SpecialItem/EnergyBeads") as GameObject);
        specialDict.Add("GoldenKey", Resources.Load("SpecialItem/GoldenKey") as GameObject);
        specialDict.Add("GoldenSword", Resources.Load("SpecialItem/GoldenSword") as GameObject);
        specialDict.Add("Meat", Resources.Load("SpecialItem/Meat") as GameObject);
        specialDict.Add("PigHead", Resources.Load("SpecialItem/PigHead") as GameObject);
        specialDict.Add("PowerRing", Resources.Load("SpecialItem/PowerRing") as GameObject);
        specialDict.Add("SpeedShoes", Resources.Load("SpecialItem/SpeedShoes") as GameObject);
        specialDict.Add("TripleAttack", Resources.Load("SpecialItem/TripleAttack") as GameObject);
        specialDict.Add("UnDeadCross", Resources.Load("SpecialItem/UnDeadCross") as GameObject);
    }

    // Update is called once per frame
  
}
