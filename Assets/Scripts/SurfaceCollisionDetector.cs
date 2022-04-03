using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UI;

public class SurfaceCollisionDetector : MonoBehaviour
{
    public GameObject surface1;
    public GameObject surface2;

    public int pulseWidth = 400; //0-400
    public int frequency = 100; //0-100
    

    //The strengths correspond to the channels in the arduino code, NOT the order of elements in the rightHand[] array. Ex. Element 0 == Channel 1 == 'a' 
    public int[] strengthRightHand; //0-255

    public GameObject[] rightHand;

    public Canvas canvas;
    public Image canvasImage;

    private string serialport = "COM19";

    // The order of the letters just corresponds to how the hand elements are assigned to the rightHand[] array, it's all arbitrary
    private string[] letters = { "st", "l", "dk", "c", "n", "fm", "e", "p", "ho", "g", "r", "jq", "i", "b", "a" };

    GameObject hsdIndex3;
    GameObject hsdIndex2;
    GameObject hsdIndex1;

    Sprite[] handSegmentSprites = new Sprite[15];
    GameObject[] handSegmentDisplays = new GameObject[15];
    Image[] handSegmentDisplaysCanvas = new Image[15];
    Sprite empty;
    Sprite baseHand;

    SerialPort stream;

    Color s1BaseColor = new Color(0.0f, 0.0f, .5f);
    Color s2BaseColor = new Color(0.0f, .5f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

        baseHand = Resources.Load<Sprite>("HandSegmentDisplay/hand");

        GameObject image = GameObject.Find("HSD");

        image.GetComponent<SpriteRenderer>().sprite = baseHand;

        handSegmentSprites[0] = Resources.Load<Sprite>("HandSegmentDisplay/palm_red");
        handSegmentSprites[1] = Resources.Load<Sprite>("HandSegmentDisplay/index1_red");
        handSegmentSprites[2] = Resources.Load<Sprite>("HandSegmentDisplay/index2_red");
        handSegmentSprites[3] = Resources.Load<Sprite>("HandSegmentDisplay/index3_red");
        handSegmentSprites[4] = Resources.Load<Sprite>("HandSegmentDisplay/middle1_red");
        handSegmentSprites[5] = Resources.Load<Sprite>("HandSegmentDisplay/middle2_red");
        handSegmentSprites[6] = Resources.Load<Sprite>("HandSegmentDisplay/middle3_red");
        handSegmentSprites[7] = Resources.Load<Sprite>("HandSegmentDisplay/ring1_red");
        handSegmentSprites[8] = Resources.Load<Sprite>("HandSegmentDisplay/ring2_red");
        handSegmentSprites[9] = Resources.Load<Sprite>("HandSegmentDisplay/ring3_red");
        handSegmentSprites[10] = Resources.Load<Sprite>("HandSegmentDisplay/pinky1_red");
        handSegmentSprites[11] = Resources.Load<Sprite>("HandSegmentDisplay/pinky2_red");
        handSegmentSprites[12] = Resources.Load<Sprite>("HandSegmentDisplay/pinky3_red");
        handSegmentSprites[13] = Resources.Load<Sprite>("HandSegmentDisplay/thumb2_red");
        handSegmentSprites[14] = Resources.Load<Sprite>("HandSegmentDisplay/thumb3_red");

        empty = Resources.Load<Sprite>("HandSegmentDisplay/empty");

        for (int i = 0; i < 15; i++)
        {
            handSegmentDisplays[i] = GameObject.Instantiate(image);
            handSegmentDisplays[i].transform.position = image.transform.position;
            handSegmentDisplays[i].GetComponent<SpriteRenderer>().sprite = empty;

            handSegmentDisplaysCanvas[i] = Instantiate(canvasImage);
            handSegmentDisplaysCanvas[i].sprite = empty;
            handSegmentDisplaysCanvas[i].transform.SetParent(canvas.transform, false);
        }

        //Offset base image to ensure hand segments show up. Might not be necessary
        image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y, image.transform.position.z + .01f);

        try
        {
            stream = new SerialPort(serialport, 115200);
            string[] ports = SerialPort.GetPortNames();

            Debug.Log("# Ports: " + ports.Length);
            for (int i = 0; i < ports.Length; i++)
            {
                Debug.Log(ports[i]);
            }

            stream.ReadTimeout = 50;
            stream.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            stream.Open();
        }
        catch (Exception e)
        {

        }

        surface1.GetComponent<Renderer>().material.color = s1BaseColor;
        surface2.GetComponent<Renderer>().material.color = s2BaseColor;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < 15; i++)
        {
            handSegmentDisplays[i].GetComponent<SpriteRenderer>().sprite = empty;
            handSegmentDisplaysCanvas[i].sprite = empty;
        }

        for (int i = 0; i < 15; i++)
        {
            if (colliding(rightHand[i]))
            {
                handSegmentDisplays[i].GetComponent<SpriteRenderer>().sprite = handSegmentSprites[i];
                handSegmentDisplaysCanvas[i].sprite = handSegmentSprites[i]; //Set the Sprite of the Image Component on the new GameObject

            }
        }


        String message1 = pulseWidth + "," + frequency + ",";
        String message2 = "";
        String message3 = "";

        bool anyStimulated = false;
        String alphabet = "abcdefghijklmnopqrst";
        for (int i = 0; i < alphabet.Length; i++)
        {
            bool assigned = false;
            char letter = alphabet[i];
            message2 = message2 + strengthRightHand[letter - 'a'] + ",";
            for (int j = 0; j < 15; j++)
            {
                if (letters[j].Contains(letter.ToString()))
                {
                    assigned = true;
                    if (colliding(rightHand[j]))
                    {
                        message3 = message3 + "1,";
                        anyStimulated = true;
                    }
                    else
                    {
                        message3 = message3 + "0,";
                    }
                }
            }
            if (!assigned)
            {
                message3 = message3 + "0,";
            }
        }
        message3 = message3.Substring(0, message3.Length - 1);

        if (anyStimulated)
        {
            message1 = message1 + "1,";
        }
        else
        {
            message1 = message1 + "0,";
        }

        String wholeMessage = message1 + message2 + message3;
        WriteToSerial(wholeMessage);

        bool s1touch = false;
        bool s2touch = false;
        
        for (int i = 0; i < 15; i++)
        {
            if (rightHand[i].GetComponent<HPTK.Views.Notifiers.CollisionNotifier>().surfaceColliding == 1)
            {
                frequency = 40;
                pulseWidth = 800;
                surface1.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                s1touch = true;
            }
            else if (rightHand[i].GetComponent<HPTK.Views.Notifiers.CollisionNotifier>().surfaceColliding == 2)
            {
                frequency = 100;
                pulseWidth = 400;
                surface2.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                s2touch = true;

            }

        }
        if (!s1touch)
        {
            surface1.GetComponent<Renderer>().material.color = s1BaseColor;

        }
        if (!s2touch)
        {
            surface2.GetComponent<Renderer>().material.color = s2BaseColor;

        }



    }

    public void WriteToSerial(string message)
    {
        if (!stream.IsOpen) return;
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    private static void DataReceivedHandler(
                    object sender,
                    SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();
        Debug.Log("Data Received:");
        Debug.Log(indata);
    }

    private bool colliding(GameObject g)
    {
        return g.GetComponent<HPTK.Views.Notifiers.CollisionNotifier>().colliding;
    }
}
