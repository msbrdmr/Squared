using System.Collections;
using UnityEngine;
public class CubeEditor : MonoBehaviour
{
    [SerializeField] private SimpleHelvetica[] textArray;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private GameObject EffectPrefab;
    [SerializeField] private GameObject EffectParent;


    private Rigidbody rb;
    public bool hasSpeed;
    public bool canRemove;
    public bool numberChanged;
    public bool isOnBase;
    
    public int numberofcube = Cube.defaultnumber;
    public GameManager gm;
    public Vector3[] textlocalpositions;
    public Vector3[] textlocalscales;
    public Vector3 startPosition;
    public Vector3 startScale;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        textArray = GetComponentsInChildren<SimpleHelvetica>();
        textlocalpositions = new Vector3[textArray.Length];
        textlocalscales = new Vector3[textArray.Length];

        for (int i = 0; i < textArray.Length; i++)
        {
            textlocalscales[i] = textArray[i].gameObject.transform.localScale;
            textlocalpositions[i] = textArray[i].gameObject.transform.localPosition;
        }



        updateText();
    }
    void Update()
    {
        if (numberChanged)
        {
            numberChanged = false;
            updateText();
        }
    }
    public void updateText()
    {
        for (int i = 0; i < textArray.Length; i++)
        {
            textArray[i].Text = numberofcube.ToString();
            textArray[i].GenerateText();
            if (numberofcube.ToString().Length == 1)
            {
                textArray[i].gameObject.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
            }
            if (numberofcube.ToString().Length == 2)
            {
                textArray[i].gameObject.transform.localScale = new Vector3(0.020f, 0.035f, 0.035f);
            }
            if (numberofcube.ToString().Length == 3)
            {
                textArray[i].gameObject.transform.localScale = new Vector3(0.015f, 0.030f, 0.035f);
            }
            if (numberofcube.ToString().Length == 4)
            {
                textArray[i].gameObject.transform.localScale = new Vector3(0.01f, 0.030f, 0.035f);
            }
        }
        gameObject.name = $"cube {numberofcube}";
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (canRemove)
        {
            if (other.gameObject.tag == "cube")
            {
                other.gameObject.SetActive(false);
                canRemove = false;
            }
        }

        if (isOnBase == true && other.gameObject.tag == tag)
        {
            gm.restartgame();
            return;
        }

        if (other.gameObject.name == gameObject.name & !canRemove)
        {
            if (rb.velocity.magnitude > other.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
            {

                other.gameObject.GetComponent<CubeEditor>().numberofcube = Cube.defaultnumber;
                other.gameObject.name = $"cube {other.gameObject.GetComponent<CubeEditor>().numberofcube}";
                other.gameObject.GetComponent<CubeEditor>().numberChanged = true;
                other.gameObject.SetActive(false);

                var otherpos = other.gameObject.transform.position;
                var pos = gameObject.transform.position;

                var expl1  = Instantiate(EffectPrefab);
                var expl2 = Instantiate(EffectPrefab);

                expl1.gameObject.SetActive(true);
                expl2.gameObject.SetActive(true);

                expl1.transform.position= pos;
                expl2.transform.position= otherpos;

                expl1.transform.SetParent(EffectParent.transform);
                expl2.transform.SetParent(EffectParent.transform);

                Destroy(expl1,1f);
                Destroy(expl2,1f);


                ObjectPool.pool.Enqueue(other.gameObject);
                gm.incrementScore(numberofcube);
                numberofcube *= 2;//number of thrown object is doubled on hit
                numberChanged = true;// other one has made 3 and pushed into queue

                gameObject.transform.position = (otherpos + pos) / 2;
            }

        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (canRemove)
        {
            if (other.gameObject.tag == "cube")
            {
                other.gameObject.SetActive(false);
                canRemove = false;
            }
        }
        if (isOnBase == true && other.gameObject.tag == tag)
        {
            gm.restartgame();
            return;
        }

        if (other.gameObject.name == gameObject.name)
        {
            if (rb.velocity.magnitude > other.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
            {
                other.gameObject.GetComponent<CubeEditor>().numberofcube = Cube.defaultnumber;
                other.gameObject.name = $"cube {other.gameObject.GetComponent<CubeEditor>().numberofcube}";
                other.gameObject.GetComponent<CubeEditor>().numberChanged = true;
                other.gameObject.SetActive(false);


                var otherpos = other.gameObject.transform.position;
                var pos = gameObject.transform.position;

                var expl1  = Instantiate(EffectPrefab);
                var expl2 = Instantiate(EffectPrefab);

                expl1.transform.position= pos;
                expl2.transform.position= otherpos;

                expl1.transform.SetParent(EffectParent.transform);
                expl2.transform.SetParent(EffectParent.transform);

                Destroy(expl1,1f);
                Destroy(expl2,1f);

                ObjectPool.pool.Enqueue(other.gameObject);
                gm.incrementScore(numberofcube);
                numberofcube *= 2;//number of thrown object is doubled on hit
                numberChanged = true;// other one has made 3 and pushed into queue
                gameObject.transform.position = (otherpos + pos) / 2;
            }
        }
    }

}
