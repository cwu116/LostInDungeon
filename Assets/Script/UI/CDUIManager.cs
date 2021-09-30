using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDUIManager : MonoBehaviour
{
    public Image attack;
    public Image skill;
    public GameObject player;
    KnightController playerCtr;
    // Start is called before the first frame update

    private void Start()
    {
        playerCtr = player.GetComponent<KnightController>();
    }
    // Update is called once per frame
    void Update()
    {
        attack.fillAmount = playerCtr.attackCD / 0.3f;
        if (playerCtr.curEnergy >= 30)
        {
            skill.fillAmount = playerCtr.skillCD / 1;
        }
        else
        {
            skill.fillAmount = 1;
        }
    }
}
