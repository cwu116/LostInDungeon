using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public Transform weapon;
    public float hp;
    public float energy;
    public float ult;
    public float skillDamage;
    public float ultDamage;

    public PlayerState curState;
    protected float vertical;
    protected float horizontal;
    protected Rigidbody2D rb;
    protected Animator anim;

    public static PlayerController instance;

    private void Awake() // 单例
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curState = PlayerState.Move;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
        switch (curState)
        {
            case PlayerState.Move:
                getMoveInput();
                break;

            case PlayerState.Attack:
                Attack();
                break;

            case PlayerState.Skill:
                Skill();
                break;

            case PlayerState.Ult:
                Ult();
                break;

            case PlayerState.Hurt:
                Hurt();
                break;

            case PlayerState.Dead:
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        WeponAim();
        switch (curState)
        {
            case PlayerState.Move:
                Move();
                break;

            case PlayerState.Attack:
                break;

            case PlayerState.Skill:
                break;

            case PlayerState.Hurt:
                break;

            case PlayerState.Dead:
                break;
        }
    }

    protected virtual void getMoveInput()
    {
       vertical  = Input.GetAxis("Vertical");
       horizontal  = Input.GetAxis("Horizontal");      
       if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
       if (horizontal > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        if (Input.GetMouseButton(0))
        {
            curState = PlayerState.Attack;
        }

        if (Input.GetMouseButton(1))
        {
            curState = PlayerState.Skill;
        }
    }

    protected virtual void Move() {
        
        rb.velocity = new Vector2(horizontal * Time.deltaTime * speed,vertical * Time.deltaTime * speed);
        if(Mathf.Abs(vertical) > 0)
        {
            anim.SetFloat("Move", Mathf.Abs(vertical));
        }

        if(Mathf.Abs(horizontal) > 0)
        {
            anim.SetFloat("Move",Mathf.Abs(horizontal));
        }
    }

    protected void WeponAim()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenPosition.z;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        Vector3 aim = mousePositionInWorld - weapon.position;
        weapon.up = aim.normalized;
        

    }

    protected virtual void Attack()
    {

    }

    protected virtual void Skill()
    {

    }

    protected virtual void Ult()
    {

    }

    public virtual void Hurt()
    {

    }

    public virtual void GetHurt(float damage, Transform damageFrom)
    {

    }

    public virtual void Dead()
    {

    }


}

public enum PlayerState
{    
    Idle,
    Move,
    Attack,
    Skill,
    Ult,
    Hurt,
    Dead
}