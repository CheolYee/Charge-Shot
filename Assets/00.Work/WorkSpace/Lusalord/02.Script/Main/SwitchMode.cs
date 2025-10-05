using System;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public bool switchMode;

    public GameObject switchOn;
    public GameObject switchOff;

    private void Start()
    {
        switchMode = false; // off 상태로 시작
    }

    public void OnClickSwitch()
    {
        switchMode = !switchMode;
        switchOn.SetActive(switchMode); // On 인거 넣어야함
        switchOff.SetActive(!switchMode); // Off 인거 넣어야함
    }
}
