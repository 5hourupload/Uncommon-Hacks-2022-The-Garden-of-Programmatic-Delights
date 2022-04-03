using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public OSC_motors motors; 
    public OSC_EMS ems;
    // Start is called before the first frame update

    public bool debug;
    public bool index;
    public bool middle;
    public bool ring;
    public bool pinky;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debug){
            if (index){
                PlayD();
            }
            if (middle){
                PlayE();
            }
            if (ring){
                PlayF();
            }
            if (pinky){
                PlayG();
            }
        }
    }

    public void InitDevice(){
        motors.unlockAllMotors();
        // int index, int channel, int intensity, int pulse_count, int pulse_width, float pulse_delay
        // pulse[0] = thumb flex, pulse[1] = thumb extend, pulse[2] = index flex, pulse[3] = index extend, ..., pulse[9] = pinky extend
        ems.Calibrate(0, 2, 4, 40, 500, 0.01f);
        ems.Calibrate(1, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(2, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(3, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(4, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(5, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(6, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(7, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(8, 5, 2, 13, 455, 0.0055f);
        ems.Calibrate(9, 5, 2, 13, 455, 0.0055f);
    }

    public void LockAllPIP(){
        // lock DIP instead for the thum
        motors.lockMotor(1);
        motors.lockMotor(3);
        motors.lockMotor(5);
        motors.lockMotor(7);
        motors.lockMotor(9);
    }

    IEnumerator EMSCoroutine(int index){
     float timePassed = 0;

     // Actuate the finger for 2 seconds, could change based on trial and error.
     while (timePassed < 2)
     {
         ems.SendEMSPulse(index);
         timePassed += Time.deltaTime;
 
         yield return null;
     }

    }

    IEnumerator MotorCoroutine(int index){
        yield return new WaitForSeconds(2);
        motors.lockMotor(index);
    }

    // public void PlayC(){
    //     motors.unlockMotor(0);
    //     motors.lockMotor(2);
    //     motors.lockMotor(4);
    //     motors.lockMotor(6);
    //     motors.lockMotor(8);

    //     StartCoroutine(EMSCoroutine(0));
    //     StartCoroutine(MotorCoroutine(0));
    // }

    public void PlayD(){
        motors.unlockMotor(2);
        motors.lockMotor(0);
        motors.lockMotor(4);
        motors.lockMotor(6);
        motors.lockMotor(8);

        StartCoroutine(EMSCoroutine(2));
        StartCoroutine(MotorCoroutine(2));
    }

    public void PlayE(){
        motors.unlockMotor(4);
        motors.lockMotor(0);
        motors.lockMotor(2);
        motors.lockMotor(6);
        motors.lockMotor(8);

        StartCoroutine(EMSCoroutine(4));
        StartCoroutine(MotorCoroutine(4));

    }

    public void PlayF(){
        motors.unlockMotor(6);
        motors.lockMotor(0);
        motors.lockMotor(2);
        motors.lockMotor(4);
        motors.lockMotor(8);

        StartCoroutine(EMSCoroutine(6));
        StartCoroutine(MotorCoroutine(6));

    }

    public void PlayG(){

        motors.unlockMotor(8);
        motors.lockMotor(0);
        motors.lockMotor(2);
        motors.lockMotor(4);
        motors.lockMotor(6);

        StartCoroutine(EMSCoroutine(8));
        StartCoroutine(MotorCoroutine(8));

    }
}
