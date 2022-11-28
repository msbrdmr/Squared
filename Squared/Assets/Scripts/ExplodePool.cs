using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePool : MonoBehaviour
{
    public GameObject effectPrefab;
    public GameObject particleholder;
    public static Queue<GameObject> Pool;

    void Awake()
    {
        // Pool = new Queue<GameObject>();
        // for (int j = 0; j < 10; j++)
        // {
        //     GameObject clonedobj = Instantiate(effectPrefab);
        //     clonedobj.transform.SetParent(particleholder.transform);
        //     clonedobj.SetActive(false);
        //     Pool.Enqueue(clonedobj);
        // }
    }

    
}
