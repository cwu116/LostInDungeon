using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightController :PlayerController
{
    [Header("玩家状态")]
    public float maxHp;
    public float curHp;
    public float maxEnergy;
    public float curEnergy;
    public int keyNum;
    public int coinNum;
    public float attackDmg;
    public float skillDmg;
    public float skillCost;
    public float ultCost;
    public float playerSpeed;
    public PlayerUI playerUI;
    public GameManager manager; // 马塞克效果

    PlayerSpecialItem specialItem;
    public GameObject playerCollectUI;
    public GameObject playerCollectText;
    public Text playerCollectInfoText;
    public Text playerCollectInfoName;
    public Image playerCollectInfoImage;
    public Image playerCollectInfoPanel;



    public GameObject playerFather;
    public Transform swordPosition;
    public GameObject knightAttack;
    public Transform prefabManager;
    public BoxCollider2D swordCollider1;
    public static KnightController instance;
    public Animator attackAnim;
    public GameObject image;

    Quaternion origion;
    int count;
    bool leftAttack;
    bool startAttack;
   
    SpriteRenderer sp;
    float hurtCount;
    public float hurtLength;
    bool enterHurt;

    [Header("CD时间")]
    public float attackCD;
    public float skillCD;
    public float hurtCD;
    public float attackCDTime;
    public float skillCDTime;
    public float hurtCDTime;


    private void Awake() // 单例
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this) 
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        count = 0;
        leftAttack = true;
        startAttack = false;
        attackCD = 999;
        skillCD = 999;
        hurtCD = 999;
        attackCDTime = 0.8f;
        skillCDTime = 1;
        hurtCDTime = 1;
        sp = GetComponent<SpriteRenderer>();
        specialItem = GetComponent<PlayerSpecialItem>();
        enterHurt = false;

        maxHp = 4;
        curHp = 2;
        maxEnergy = 100;
        curEnergy = maxEnergy;
        attackDmg = 10;
        skillDmg = 40;
        playerSpeed = 160f;
        skillCost = 30;
        ultCost = 75;



        playerCollectInfoPanel = GameObject.FindGameObjectWithTag("CollectInfoPanel").GetComponent<Image>();
        playerCollectInfoImage = GameObject.FindGameObjectWithTag("CollectInfoImage").GetComponent<Image>();
        playerCollectInfoText = GameObject.FindGameObjectWithTag("CollectInfoText").GetComponent<Text>();
        playerCollectInfoName = GameObject.FindGameObjectWithTag("CollectInfoName").GetComponent<Text>();
        playerCollectUI = GameObject.FindGameObjectWithTag("PlayerCollectUI").transform.Find("image").gameObject;
        playerCollectText = playerCollectUI.transform.Find("text").gameObject;


    }

    protected override void Update()
    {
     
        CDcounting();
        specialItem.ItemsEffectAlways();
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
                break;

            case PlayerState.Dead:
                break;
        }
    }
    
        

    protected override void FixedUpdate()
    {

        switch (curState)
        {
            case PlayerState.Move:
                WeponAim();
                Move();
                break;

            case PlayerState.Attack:
                break;

            case PlayerState.Skill:
                break;

            case PlayerState.Hurt:
                Hurt();
                break;

            case PlayerState.Dead:
                break;
        }
    }

    protected override void  Attack()
    {
        
        if (!startAttack)
        {
            origion = weapon.rotation;
            startAttack = true;
            specialItem.ItemsEffectAttack();// 道具效果

        }
        if (count < 5 && leftAttack)
        {
            count++;
            weapon.rotation = Quaternion.Euler(0, 0, 18) * weapon.rotation;
        }

        if (count == 5)
        {
            leftAttack = false;
            swordCollider1.enabled = true;
        }

        if (count > -5 && !leftAttack)
        {
            count--;
            weapon.rotation = Quaternion.Euler(0, 0, -18) * weapon.rotation;
        }

        if (count == -5)
        {
            curState = PlayerState.Move;
            leftAttack = true;
            count = 0;
            weapon.rotation = origion;
            startAttack = false;
            swordCollider1.enabled = false;
            attackAnim.SetTrigger("Attack");
            
        }
    }

    protected override void Skill()
    {

        if (curEnergy - 30 < 0 && !specialItem.specialDictSkill.ContainsKey("BloodCost"))
        {
            curState = PlayerState.Move;
        }
        else
        {

            if (!startAttack)
            {
                origion = weapon.rotation;
                startAttack = true;               
            }
            if (count < 5 && leftAttack)
            {
                count++;
                weapon.rotation = Quaternion.Euler(0, 0, 18) * weapon.rotation;
            }

            if (count == 5)
            {
                leftAttack = false;
            }

            if (count > -5 && !leftAttack)
            {
                count--;
                weapon.rotation = Quaternion.Euler(0, 0, -18) * weapon.rotation;
            }

            if (count == -5)
            {
                GameObject attack1 = Instantiate(knightAttack, new Vector2(swordPosition.position.x, swordPosition.position.y), Quaternion.identity, prefabManager);
                //GameObject attack2 = Instantiate(knightAttack, weapon.position, Quaternion.identity, prefabManager);
                //GameObject attack3 = Instantiate(knightAttack, weapon.position, Quaternion.identity, prefabManager);
                attack1.transform.up = weapon.up;
                //attack2.transform.right =  Quaternion.Euler(0, 0, 45) * weapon.up ;
                // attack3.transform.right =  Quaternion.Euler(0, 0, -45) * weapon.up ;
                curState = PlayerState.Move;
                leftAttack = true;
                count = 0;   // 重置CD
                weapon.rotation = origion;
                startAttack = false;

                specialItem.ItemsEffectSkill(); // 道具效果
                if (!specialItem.specialDictSkill.ContainsKey("BloodCost") || curEnergy >= skillCost)
                {
                    curEnergy -= 30;  //扣能量
                }
            }
        }
    }

    protected override void Ult()
    {

        if (curEnergy - 75 < 0)
        {
            curState = PlayerState.Move;
        }
        else
        {

            if (!startAttack)
            {
                origion = weapon.rotation;
                startAttack = true;
            }
            if (count < 5 && leftAttack)
            {
                count++;
                weapon.rotation = Quaternion.Euler(0, 0, 18) * weapon.rotation;
            }

            if (count == 5)
            {
                leftAttack = false;
            }

            if (count > -5 && !leftAttack)
            {
                count--;
                weapon.rotation = Quaternion.Euler(0, 0, -18) * weapon.rotation;
            }

            if (count == -5)
            {
                GameObject attack1 = Instantiate(knightAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, prefabManager);
                GameObject attack2 = Instantiate(knightAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, prefabManager);
                GameObject attack3 = Instantiate(knightAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, prefabManager);
                GameObject attack4 = Instantiate(knightAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, prefabManager);
                GameObject attack5 = Instantiate(knightAttack, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, prefabManager);
                attack1.transform.up = weapon.up;
                attack2.transform.up = Quaternion.Euler(0, 0, 45) * weapon.up;
                attack3.transform.up = Quaternion.Euler(0, 0, -45) * weapon.up;
                attack4.transform.up = Quaternion.Euler(0, 0, -22.5f) * weapon.up;
                attack5.transform.up = Quaternion.Euler(0, 0, 22.5f) * weapon.up;
                leftAttack = true;
                count = 0;
                weapon.rotation = origion;
                startAttack = false;
                curEnergy -= 75;
                curState = PlayerState.Move;
            }
        }
    }
    public override void GetHurt(float damage, Transform damageFrom)
    {
        if (hurtCD > hurtCDTime)
        {
            hurtCD = 0;
            if (curHp > damage)
            {
                curHp -= damage;
                playerUI.UpdateHpUI();
            }
            else
            {
                curHp = 0;
                playerUI.UpdateHpUI();
                Dead();
            }
            Vector3 hurtMove = transform.position - damageFrom.position;
            transform.position += hurtMove / 1.5f;
            curState = PlayerState.Hurt;
        }
    }

    public override void Hurt()
    {
        
        if (!enterHurt)
        {
            sp.material.SetFloat("_FlashAmount", 1);
            hurtCount = hurtLength;
            enterHurt = true;
        }

        if (enterHurt)
        {
            hurtCount -= Time.deltaTime;
        }

        if(hurtCount <= 0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
            enterHurt = false;
            curState = PlayerState.Move;
        }
    }

    public override void Dead()
    {
        if (specialItem.specialDict.ContainsKey("UnDeadCross"))
        {
            specialItem.specialDict["UnDeadCross"].ItemEffect();
        }
        else
        {
            curState = PlayerState.Dead;
            manager.PlayerDead();
        }
    }
    private void CDcounting()
    {
        attackCD += Time.deltaTime;
        skillCD += Time.deltaTime;
        hurtCD += Time.deltaTime;
    }

    protected override void getMoveInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        //if (horizontal < 0)
        //{
        //    transform.localScale = new Vector2(-1f, 1f);
        //}

        //if (horizontal > 0)
        //{
        //    transform.localScale = new Vector2(1f, 1f);
        //}

        if (attackCD > attackCDTime)
        {
           
            if (Input.GetMouseButton(0))
            {
                attackCD = 0;
                curState = PlayerState.Attack;
            }
            
        }

        if (skillCD > skillCDTime)
        {
            
            if (Input.GetMouseButton(1))
            {
                skillCD = 0;
                curState = PlayerState.Skill;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            curState = PlayerState.Ult;
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            image.SetActive(true);
        }

        if (!Input.GetKey(KeyCode.Tab))
        {
            image.SetActive(false);
        }



    }

    protected override void Move()
    {

        rb.velocity = new Vector2(horizontal * Time.deltaTime * playerSpeed, vertical * Time.deltaTime * playerSpeed);
        if (Mathf.Abs(vertical) > 0)
        {
            anim.SetFloat("Move", Mathf.Abs(vertical));
        }

        if (Mathf.Abs(horizontal) > 0)
        {
            anim.SetFloat("Move", Mathf.Abs(horizontal));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpecialItem"))
        {
            collision.GetComponent<SpecialItem>().isPickAble = true;
            playerCollectUI.transform.position = Camera.main.WorldToScreenPoint(new Vector3(collision.transform.position.x, collision.transform.position.y + 1.2f, collision.transform.position.z));
            playerCollectText.GetComponent<Text>().text = collision.GetComponent<SpecialItem>().desname;
            playerCollectUI.active = true;
            playerCollectText.active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpecialItem"))
        {
            collision.GetComponent<SpecialItem>().isPickAble = false;
            playerCollectUI.active = false;
            playerCollectText.active = false;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("WoodBox") && collision.transform.GetComponent<Boxes>().isOpen == false)
        {
            Debug.Log("hitbox");
            collision.transform.GetComponent<Boxes>().canOpen = true;
            Debug.Log("canOpen");
            playerCollectUI.transform.position = Camera.main.WorldToScreenPoint(new Vector3(collision.transform.position.x, collision.transform.position.y + 1.2f, collision.transform.position.z));
            playerCollectUI.active = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WoodBox"))
        {
            Debug.Log("leaveBox");
            collision.transform.GetComponent<Boxes>().canOpen = false;
            playerCollectUI.active = false;
        }
    }
 

    public void ShowCollectInfo(GameObject item)
    {
       
        playerCollectInfoImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        playerCollectInfoText.text = item.GetComponent<SpecialItem>().description;
        playerCollectInfoName.text = item.GetComponent<SpecialItem>().desname;
        playerCollectInfoImage.enabled = true;
        playerCollectInfoPanel.enabled = true;
        playerCollectInfoText.enabled = true;
        playerCollectInfoName.enabled = true;
        StopAllCoroutines();
        StartCoroutine(CloseCollectionInfo(2f));
    }

    IEnumerator CloseCollectionInfo(float timer)
    {
        yield return new WaitForSeconds(timer);
        ColseCollectInfo();

    }

    public void ColseCollectInfo()
    {
        playerCollectInfoImage.enabled = false;
        playerCollectInfoPanel.enabled = false;
        playerCollectInfoText.enabled = false;
        playerCollectInfoName.enabled = false;
    }

   
}
