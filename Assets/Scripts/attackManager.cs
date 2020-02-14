using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackManager : MonoBehaviour
{
    private static attackManager _instance;
    public static attackManager Instatnce
    {
        get { return _instance; }
    }

    public class guardianAttackParameter {
        public int attackType;
        public float atkPoint;
        public float defCut;
        public float criChance;
        public float criDamage;

        public guardianAttackParameter(int a, float b, float c, float d, float e) {
            attackType = a;
            atkPoint = b;
            defCut = c;
            criChance = d;
            criDamage = e;
        }

    };

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void attack(guardianAttackParameter gap, mobFSM target ) {
        switch (gap.attackType) {
            case 0:defaultAttack(gap, target); break;
            case 1: break;
            default: break;
        }

    }

    static void defaultAttack(guardianAttackParameter gap, mobFSM target) {
        float criDamage = Random.Range(1f, 100f) <= gap.criChance ? gap.criDamage : 100f;
        if(target!=null)
        target.dealDamage(gap.atkPoint * criDamage/100f, gap.defCut);
    }


}
