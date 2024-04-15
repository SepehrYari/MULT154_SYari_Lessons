using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCountGui : MonoBehaviour
{
    private TextMeshProUGUI tmproElem;
    public string itemName;
    int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        tmproElem = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCount()
    {
        count++;
        tmproElem.text = itemName + ": " + count;
    }
}
