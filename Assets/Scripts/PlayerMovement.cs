using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Vector3 Direction = Vector3.zero;
    public float speed = 10.0f;
    public GameObject spawnPoint = null;
    private Dictionary<Item.vegetableType, int> ItemInventory = new Dictionary<Item.vegetableType, int>();



    // Start is called before the first frame update
    void Start()
    {

        rbPlayer = GetComponent<Rigidbody>();

        foreach(Item.vegetableType item in System.Enum.GetValues(typeof(Item.vegetableType)))
        {
            ItemInventory.Add(item, 0);
        }

        
    }

    private void AddToInventory(Item item)
    {
        ItemInventory[item.typeofveggie]++;
    }

    private void PrintInventory()
    {
        string output = "";

        foreach(KeyValuePair<Item.vegetableType, int> kvp in ItemInventory)
        {
            output += string.Format("{0}: {1} | ", kvp.Key, kvp.Value);
        }
        Debug.Log(output);
    }





    private void Update()
    {
        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        Direction = new Vector3(horMove, 0, verMove);
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        rbPlayer.AddForce(Direction * speed, ForceMode.Force);
        

        if (transform.position.z > 40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 40);
        }
        else if (transform.position.z < -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        }


    }


    private void Respawn()
    {
        rbPlayer.MovePosition(spawnPoint.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            AddToInventory(item);
            PrintInventory();
        }
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            Respawn();
        }
    }





}
