using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerHeat;
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public GameObject emptyHeartPrefab;
    public KnightController player;
    public Image energyBar;
    public float maxHp;
    public float curHp;

    public int coinNum;
    public int keyNum;
    public Text coinTxt;
    public Text keyTxt;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightController>();
        UpdateHpUI();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateEnergy();
        UpdateItemNum();
        UpdateHpUI();
    }

    public void UpdateHpUI()
    {
        RefreshUI();
        maxHp = player.maxHp;
        curHp = player.curHp;
        if(maxHp - curHp == 0)
        {
            for (int i = 0; i < maxHp; i++)
            {
                Instantiate(fullHeartPrefab, playerHeat);
            }
        }
        else
        {
            float heartNumber = curHp/0.5f;
            for (int i = 0; i < Mathf.Floor(curHp); i++)
            {
                Instantiate(fullHeartPrefab, playerHeat);
            }

            if (heartNumber % 2 != 0)
            {
                Instantiate(halfHeartPrefab, playerHeat);
            }

            float emptyHeartNumber = maxHp - curHp;
            for (int i = 0; i < Mathf.Floor(emptyHeartNumber); i++)
            {
                Instantiate(emptyHeartPrefab, playerHeat);
            }

        }
    }

    void RefreshUI()
    {
       
        foreach(Transform child in playerHeat)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void UpdateEnergy()
    {
        energyBar.fillAmount = player.curEnergy / player.maxEnergy;
    }

    void UpdateItemNum()
    {
        coinNum = player.coinNum;
        keyNum = player.keyNum;
        coinTxt.text = "  X " + coinNum;
        keyTxt.text = "  X " + keyNum;
    }
}
