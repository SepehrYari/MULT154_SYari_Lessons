using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ItemCollect : NetworkBehaviour
{
    private Dictionary<Item.vegetableType, int> ItemInventory = new Dictionary<Item.vegetableType, int>();

    public delegate void CollectItem(Item.vegetableType item);
    public static event CollectItem ItemCollected;

    Collider itemCollider = null;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Item.vegetableType item in System.Enum.GetValues(typeof(Item.vegetableType)))
        {
            ItemInventory.Add(item, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }


        if (itemCollider && Input.GetKeyDown(KeyCode.Space))
        {
            Item item = itemCollider.gameObject.GetComponent<Item>();
            AddToInventory(item);
            PrintInventory();
            CmdItemCollected(item.typeofveggie);
        }
        
    }

    [Command]
    void CmdItemCollected(Item.vegetableType itemtype)
    {
        Debug.Log("CommandItemCollected: " + itemtype);
        RpcItemCollected(itemtype);
    }

    [ClientRpc]
    void RpcItemCollected(Item.vegetableType itemtype)
    {
        ItemCollected?.Invoke(itemtype);
    }


    private void AddToInventory(Item item)
    {
        ItemInventory[item.typeofveggie]++;
    }

    private void PrintInventory()
    {
        string output = "";

        foreach (KeyValuePair<Item.vegetableType, int> kvp in ItemInventory)
        {
            output += string.Format("{0}: {1} | ", kvp.Key, kvp.Value);
        }
        Debug.Log(output);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }


        if (other.CompareTag("Item"))
        {
            itemCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }


        if (other.CompareTag("Item"))
        {
            itemCollider = null;

        }
    }

}
