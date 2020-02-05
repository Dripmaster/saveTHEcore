using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataManager : MonoBehaviour
{

    private static dataManager _instance;
    public static dataManager Instatnce
    {
        get { return _instance; }
    }

    // Start is called before the first frame update
    void Awake()
    {
       if(_instance == null)
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
}
