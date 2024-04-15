using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnManager : NetworkBehaviour
{

    public GameObject [] lilyPadObj = null;


    // Start is called before the first frame update
    public override void OnStartServer()
    {
        base.OnStartServer();
        InvokeRepeating("SpawnLilyPad", 2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnLilyPad()
    {
        foreach(GameObject lilyPad in lilyPadObj)
        {
            GameObject tempLilyPad = Instantiate(lilyPad);
            NetworkServer.Spawn(tempLilyPad);
        }

    }
}
