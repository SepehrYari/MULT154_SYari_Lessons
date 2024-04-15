using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    public List<GameObject> items;



    // Start is called before the first frame update
    void Start()
    {
        ItemCollect.ItemCollected += incrementItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void incrementItem(Item.vegetableType itemType)
    {
        InventoryCountGui cg = items[(int)itemType].GetComponent<InventoryCountGui>();
        cg.UpdateCount();

    }

}
