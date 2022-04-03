using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VHand : MonoBehaviour
{
    public Control control;
	private GameObject Hand; 
	private Transform HandMesh;
    private GameObject Text1; 
    private GameObject Text2;
    private Color alphaColor;
    private bool _triggered;
    private bool _switch;
    private bool _first;
    private float timeToFade = 2.0f;

    void Awake()
	{
		_triggered = false;
        _switch = false;
        _first = false;
		Hand = GameObject.Find("VirtualHand");
		HandMesh = Hand.transform.GetChild(0).transform.GetChild(1);
        Text1 = GameObject.Find("Text1");
        Text2 = GameObject.Find("Text2");

        StartCoroutine(TimerCoroutine());
        StartCoroutine(TimerCoroutine2());

	}

    void Start()
    {
    	 alphaColor = HandMesh.GetComponent<SkinnedMeshRenderer>().material.color;
         alphaColor.a = 0;

         control.InitDevice();
    }

    // Update is called once per frame
    void Update()
    {
    	if (_triggered == true && Text1.GetComponent<Text>().text != "DextrEMS OFF"){
    		StartCoroutine(OnTriggerCoroutine());
            Text1.GetComponent<Text>().text = "DextrEMS ON";
            if (!_first){
                 StartCoroutine(DeviceCoroutine());
            }
            _first = true;
    	}

        if (_switch == true){
            Text1.GetComponent<Text>().text = "DextrEMS OFF";
        }
        
    }

    private void OnTriggerEnter(Collider collider)
    {
    	Debug.Log(collider.name);
    	_triggered = true;
    }

    IEnumerator OnTriggerCoroutine()
    {
        yield return new WaitForSeconds(5);
        HandMesh.GetComponent<SkinnedMeshRenderer>().material.color = 
            Color.Lerp(HandMesh.GetComponent<SkinnedMeshRenderer>().material.color, 
            alphaColor, timeToFade * Time.deltaTime);

    }

    IEnumerator DeviceCoroutine()
    {
        yield return new WaitForSeconds(30);
        // control.PlayC();

        // yield return new WaitForSeconds(4);
        control.PlayD();

        yield return new WaitForSeconds(4);
        control.PlayE();

        yield return new WaitForSeconds(4);
        control.PlayF();

        yield return new WaitForSeconds(4);
        control.PlayG();
    }


    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(52);
        _switch = true;
    }

     IEnumerator TimerCoroutine2()
    {
        yield return new WaitForSeconds(60);
        Text2.GetComponent<Text>().text = "Your Score: 90";
    }

}
