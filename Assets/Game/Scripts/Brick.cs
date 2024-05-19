using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public static Brick instance;
    private float height;
    [SerializeField] List<GameObject> Bricks = new List<GameObject>();
    [SerializeField] Transform playerModel;
    [SerializeField] private GameObject brickPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PickDash(GameObject dash)
    {
        Destroy(dash);
        height += 0.2f;
        // Khai báo chiều cao Model của nhân vật tăng lên
        playerModel.position = new Vector3(playerModel.position.x, playerModel.position.y + 0.2f, playerModel.position.z);
        // Object đi theo nhân vật, hàm sinh ra
        GameObject go = Instantiate(brickPrefab, transform);
        go.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        Bricks.Add(go);
    }

    private void BuildBrigh(GameObject dash)
    {

        height -= 0.2f;
        playerModel.position = new Vector3(playerModel.position.x, playerModel.position.y - 0.2f, playerModel.position.z);
        dash.GetComponent<MeshRenderer>().enabled = true;
        dash.GetComponent<BoxCollider>().enabled = false;     
        Bricks.RemoveAt(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Brick")
        {
            Debug.Log("Va cham voi gachhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
            PickDash(other.gameObject);
        }

        if(other.tag == "Bridge")
        {
            Debug.Log("Va cham voi cauuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");
            BuildBrigh(other.gameObject);
            GameObject go = other.transform.GetChild(0).gameObject;
            go.SetActive(true);
            other.tag = "Untagged";
            other.isTrigger = false;
        }
    }
}
