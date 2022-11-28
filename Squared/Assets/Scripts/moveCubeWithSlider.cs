using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class moveCubeWithSlider : MonoBehaviour
{
    public ObjectPool pool;
    public Rigidbody rbCube;
    public float spawnTimer = 0.5f;
    public Slider moveslider;
    public Slider heightslider;
    public StoreItems str;

    public TextMeshProUGUI cannotbuytext;
    public GameObject storeMenu;
    public bool notenough;
    public bool notenough2;
    public GameObject controllerMenu;
    private GameObject cube; //controlled cube
    private float throwforce = Cube.defaultspeed;

    private void Start()
    {
        heightslider.value = heightslider.minValue;
        heightslider.gameObject.SetActive(false);
        moveslider.value = (moveslider.minValue + moveslider.maxValue) / 2;
        cube = pool.getCubefromPool();// position and rotation has set in this method

    }
    private void Update()
    {
        if (notenough)
        {
            StartCoroutine(moveText());
        }
        if (notenough2)
        {

            StartCoroutine(moveTextBack());
        }
    }
    public void closeStoreMenu()
    {
        storeMenu.SetActive(false);
        controllerMenu.SetActive(true);
    }
    IEnumerator GenerateBoxCoroutine()
    {
        yield return new WaitForSeconds(spawnTimer);
        generateCube();
    }

    public void generateCube()
    {

        if (cube == null)
        {
            cube = pool.getCubefromPool();
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            cube.GetComponent<CubeEditor>().numberofcube = Cube.currentnumber;
            cube.GetComponent<CubeEditor>().numberChanged = true;
            cube.transform.position = new Vector3(cube.transform.position.x, heightslider.value, moveslider.value);
            cube.transform.rotation = Quaternion.Euler(-90,0,0);
            cube.GetComponent<CubeEditor>().isOnBase = true;
        }
    }
    #region buystuff
    public void buySpeedBoost()
    {
        if(notenough || notenough2) return;

        // Debug.Log(GameManager.score + " -- " + str.storeItems[0].price);
        if (GameManager.score >= str.storeItems[0].price)
        {

            throwforce += 2.5f;

            GameManager.score -= str.storeItems[0].price;
        }
        else notenough = true;
        closeStoreMenu();
    }
    public void buyAdjustHeight()
    {
        if(notenough || notenough2) return;

        if (GameManager.score >= str.storeItems[1].price)
        {
            heightslider.gameObject.SetActive(true);
            GameManager.score -= str.storeItems[1].price;
        }
        else notenough = true;
        closeStoreMenu();

    }
    public void buyDoubleCube()
    {
        if(notenough || notenough2) return;

        if (GameManager.score >= str.storeItems[2].price)
        {
            GameManager.score -= str.storeItems[2].price;

            Cube.currentnumber *= 2;
            cube.gameObject.GetComponent<CubeEditor>().numberofcube = Cube.currentnumber;
            cube.gameObject.GetComponent<CubeEditor>().numberChanged = true;
            cube.gameObject.GetComponent<CubeEditor>().isOnBase = true;
            Vector3 pos = cube.transform.position;
            cube.transform.position = pos;
        }
        else notenough = true;
        closeStoreMenu();
    }
    public void buyRemoveOnHit()
    {
        if(notenough || notenough2) return;

        if (GameManager.score >= str.storeItems[3].price)
        {
            GameManager.score -= str.storeItems[3].price;

            cube.gameObject.GetComponent<CubeEditor>().canRemove = true;

        }
        else notenough = true;
        closeStoreMenu();
    }

    #endregion buystuff

    public void moveCube()
    {
        try
        {
            cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, moveslider.value);
        }
        catch (System.Exception)
        {
        }
    }
    public void adjustHeight()
    {
        try
        {
            cube.transform.position = new Vector3(cube.transform.position.x, heightslider.value, cube.transform.position.z);
        }
        catch (System.Exception)
        {
        }
    }
    public void restartCube()
    {
        cube.SetActive(true);
    }
    public void throwCube()
    {
        if (cube == null) return;
        cube.GetComponent<BoxCollider>().enabled = true;
        cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        cube.GetComponent<Rigidbody>().velocity = Vector3.left * .01f;
        cube.GetComponent<Rigidbody>().AddForce(new Vector3(-throwforce, 0f, 0f), ForceMode.Impulse);
        cube.GetComponent<CubeEditor>().hasSpeed = true;
        cube.GetComponent<CubeEditor>().isOnBase = false;
        cube = null;
        StartCoroutine(GenerateBoxCoroutine());
    }


    public IEnumerator moveText()
    {

        cannotbuytext.rectTransform.anchoredPosition3D = Vector3.Lerp(cannotbuytext.rectTransform.anchoredPosition3D,
         new Vector3(0, cannotbuytext.rectTransform.anchoredPosition3D.y, cannotbuytext.rectTransform.anchoredPosition3D.z),
          0.1f);
        yield return new WaitForSeconds(1f);
        notenough = false;
        notenough2 = true;
    }

    public IEnumerator moveTextBack()
    {

        cannotbuytext.rectTransform.anchoredPosition3D = Vector3.Lerp(cannotbuytext.rectTransform.anchoredPosition3D,
         new Vector3(-1000, cannotbuytext.rectTransform.anchoredPosition3D.y, cannotbuytext.rectTransform.anchoredPosition3D.z),
          0.1f);
        yield return new WaitForSeconds(0f);

        notenough2 = false;
    }


}
