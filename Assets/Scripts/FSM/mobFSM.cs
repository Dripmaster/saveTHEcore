﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobFSM : FSMbase
{


    public enum State
    {
        IDLE = 0,
        WALK,
        DIE,
    };
    public enum Direction
    {
        LEFT = 0,
        RIGHT,
        UP,
        DOWN
    };
    State mobState;
    Direction mobDir;
    Vector2 moveDir;
    Transform[] targetVector;
    int currentTarget = 1;
    /// monster status
    string mobNAME;
    int level = 1;
    float mobSpeed = 5;
    float nowMobSpeed;
    float def = 1;
    float nowDef;
    float maxHP = 1000;
    float nowHP;
    /// monster status
    public bool isDie;

    public void Awake()
    {
        base.Awake();
        targetVector = new Transform[4];
        targetVector[0] = GameObject.Find("left-up").transform;
        targetVector[1] = GameObject.Find("left-down").transform;
        targetVector[2] = GameObject.Find("right-down").transform;
        targetVector[3] = GameObject.Find("right-up").transform;
        loadStat();
        mobDir = Direction.DOWN;
        setState(State.WALK);
        setDir(mobDir);
        isDie = false;
        
    }
    public void Update()
    {

    }
    public void setState(State s)
    {
        newState = true;
        mobState = s;
        _anim.SetInteger("State", (int)mobState);
        _anim.SetInteger("Dir", (int)mobDir);

    }
    void loadStat() {// mobname을 이용해 몹 스탯 로딩(엑셀)
        //로딩

        nowHP = maxHP;
        nowMobSpeed = mobSpeed;
        nowDef = def;
    }
    public void dealDamage(float d, float defCut) {
        nowHP -= d*(1-(nowDef-defCut/100));
        if (nowHP <= 0) {
            nowHP = 0;
            //몹 죽고 실행될 코드
            isDie = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            _anim.speed = 0;
        }
    }
    public void setDir(Direction d)
    {
        mobDir = d;
        _anim.SetInteger("Dir", (int)mobDir);
        _anim.speed = nowMobSpeed;
    }
    IEnumerator FSMmain()
    {

        while (true)
        {
            newState = false;
            yield return StartCoroutine(mobState.ToString());
        }
    }
    IEnumerator IDLE()
    {

       
        do
        {
            yield return null;
            

        } while (!newState);
    }

    IEnumerator WALK()
    {

        do
        {
            yield return null;
            if (isDie) {
                continue; //임시

            }
            Vector2 targetPos = targetVector[currentTarget].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime* nowMobSpeed);
            
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                currentTarget++;
                if (currentTarget >= targetVector.Length) {
                    currentTarget = 0;
                }

                switch (currentTarget) {
                    case 0: setDir(Direction.LEFT); break;
                    case 1: setDir(Direction.DOWN); break;
                    case 2: setDir(Direction.RIGHT); break;
                    case 3: setDir(Direction.UP); break;
                    default:setDir(Direction.LEFT); break;
                }
                
            }
            
            
        } while (!newState);
    }
    IEnumerator ATTACK()
    {
        do
        {
            yield return null;

        } while (!newState);
    }
}