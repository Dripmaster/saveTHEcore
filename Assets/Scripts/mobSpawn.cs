using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobSpawn : MonoBehaviour
{
    public float spawnRate = 0.25f;
    public Transform spawnPos;
    bool endSpawn = false;
    public List<GameObject> gList = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawn() {
        do
        {
            GameObject g = Instantiate(gList[0], spawnPos.position, Quaternion.identity);

            yield return new WaitForSeconds(1 / spawnRate);

        } while (!endSpawn);
        
    }
}
