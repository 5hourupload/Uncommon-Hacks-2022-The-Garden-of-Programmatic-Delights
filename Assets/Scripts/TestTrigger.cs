using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTrigger : MonoBehaviour
{
    public OSC osc;
    private string name = "/motors";
    private string command = "0000";
    private int count = 0;
    // Start is called before the first frame update
    void Awake(){
        // DebugLog = GameObject.Find("DebugLog");
    }
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Send();
        Debug.Log(count);
        count = count + 1;
        // first = false;
    }


    private void Send()
    {
        OscMessage message = new OscMessage();

        message.address = name;
        message.values.Add(command);
        osc.Send(message);
        Debug.Log(System.DateTime.Now.ToString("HH:mm:ss.ffffff"));
    }

}
