using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject CubePrefab;
    [SerializeField] GameObject CubeHolder;
    [SerializeField] private int poolsize;
    [SerializeField] Camera cam;
    public Color[] colorArray;

    public static Queue<GameObject> pool;

    void Awake()
    {

        pool = new Queue<GameObject>();
        for (int i = 0; i < poolsize; i++)
        {
            GameObject clonedobj = Instantiate(CubePrefab);
            clonedobj.transform.SetParent(CubeHolder.transform);
            clonedobj.SetActive(false);
            pool.Enqueue(clonedobj);
        }
    }

    private void Start()
    {
        CubePrefab.gameObject.SetActive(false);
    }

    public GameObject getCubefromPool()
    {
        GameObject Cube = pool.Dequeue();
        var rb = Cube.GetComponent<Rigidbody>();
        Cube.transform.position = CubePrefab.transform.position;
        Cube.transform.rotation = Quaternion.Euler(-90,0,0);

        rb.velocity = Vector3.zero;

        int randval = Random.Range(0, 8);
        Color color = colorArray[randval];
        Cube.GetComponent<Renderer>().material.color = color;

        Cube.SetActive(true);
        return Cube;
    }


}
