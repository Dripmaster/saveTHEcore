using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMbase : MonoBehaviour
{
    public enum State
    {
        IDLE = 0,
        WALK,
        ATTACK,
    };
    public State objectState;
    public Animator _anim;
    public bool newState;
    //
    public void Awake()
    {
        objectState = State.IDLE;
        _anim = GetComponent<Animator>();
    }
    public void OnEnable()
    {
        StartCoroutine("FSMmain");
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void setState(State s)
    {
        newState = true;
        objectState = s;
        _anim.SetInteger("State", (int)objectState);
    }


    IEnumerator FSMmain()
    {

        while (true)
        {
            newState = false;
            yield return StartCoroutine(objectState.ToString());
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

        } while (!newState);
    }

}
