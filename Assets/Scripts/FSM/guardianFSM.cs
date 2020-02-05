using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardianFSM : FSMbase
{

    public enum State
    {
        IDLE = 0,
        ATTACK,
    };
 
    State gdState;
    mobFSM targetMob;
    /// guardian status
    string gdNAME;
    int level = 1;
    float atkPoint = 10;
    float nowAtkPoint;
    float atkSpeed = 5;
    float nowAtkSpeed;
    float maxMana = 10;
    float nowMana;
    float defCut = 10;
    float nowDefCut;
    float criChance = 50;
    float nowCriChance;
    float criDamage = 200;
    float nowCriDamage;
    float atkRange = 10;
    /// guardian status
    public GameObject myBullet;
    public Transform bulletPos;

    public void Awake()
    {
        base.Awake();
        setState(State.IDLE);

        loadStat();
    }
    public void Update()
    {

    }
    public void setState(State s)
    {
        newState = true;
        gdState = s;
        _anim.SetInteger("State", (int)gdState);

    }
    void loadStat()
    {// name,레벨을 이용해 가디언 스탯 로딩(엑셀)
        //로딩

        nowAtkPoint = atkPoint;
        nowAtkSpeed = atkSpeed;
        nowMana = 0;
        nowDefCut = defCut;
        nowCriChance = criChance;
        nowCriDamage = criDamage;
        
    }
    public void attackCommand() {
        attackManager.guardianAttackParameter gap = new attackManager.guardianAttackParameter(0,nowAtkPoint,nowDefCut,nowCriChance,nowCriDamage);

        attackManager.attack(gap,targetMob);

    }
    void detectEnemy() {
        GameObject[] allMonster = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject g in allMonster) {
            if (Vector2.Distance(g.transform.position, transform.position) <= atkRange) {
                targetMob = g.GetComponent<mobFSM>();
                if (targetMob.isDie)
                    continue;
            }
        }
    }

    IEnumerator FSMmain()
    {

        while (true)
        {
            newState = false;
            yield return StartCoroutine(gdState.ToString());
        }
    }
    IEnumerator IDLE()
    {
        do
        {
            yield return null;
            detectEnemy();
            if (targetMob != null) {
                setState(State.ATTACK);
            }
        } while (!newState);
    }
    IEnumerator ATTACK()
    {
        do
        {
            if (targetMob == null||targetMob.isDie) {
                setState(State.IDLE);
                break;
            }
            
            if (Vector2.Distance(targetMob.transform.position, transform.position) > atkRange)
            {//사거리 넘어감
                targetMob = null;
                setState(State.IDLE);
            }
            else {
                //방향전환
                if (transform.localScale.x==-1&&targetMob.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if(transform.localScale.x == 1 && targetMob.transform.position.x < transform.position.x) {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                //공격(발사체 발사 방식)
                GameObject g = Instantiate(myBullet, bulletPos.position, Quaternion.identity);
                g.GetComponent<bulletScript>().bulletInit(targetMob.transform,this);
                yield return new WaitForSeconds(1 / atkSpeed);
            }
        } while (!newState);
    }
}