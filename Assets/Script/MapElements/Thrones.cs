using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrones : MonoBehaviour
{

    ThronesState state;
    Animator anim;
    float activeCD = 0;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ThronesState.idle:
                anim.Play("idle");
                break;

            case ThronesState.switchOn:
                anim.Play("ThronesSwitchOn");
                break;

            case ThronesState.active:
                if (activeCD < 3)
                {
                    activeCD += Time.deltaTime;
                }
                else
                {
                    activeCD = 0;
                    state = ThronesState.switchOff;
                }
                break;

            case ThronesState.switchOff:
                anim.Play("ThronesSwitchOFF");
                break;
        }
    }

    enum ThronesState
    {
        idle,
        switchOn,
        switchOff,
        active,
    }

    void ActiveThrone()
    {
        anim.Play("ThroneActive");
        state = ThronesState.active;
    }

    void ActiveOffThrone()
    {
        state = ThronesState.idle;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(state == ThronesState.idle)
            {
                state = ThronesState.switchOn;
            }

            if (state == ThronesState.active)
            {
                player.GetHurt(0.5f, this.transform);
            }

        }
    }


}
