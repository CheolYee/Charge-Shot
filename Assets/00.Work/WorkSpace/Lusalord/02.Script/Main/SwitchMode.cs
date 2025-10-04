using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public bool switchMode = true;

    public GameObject switchOn;
    public GameObject switchOff;


    public void OnClickSwitch()
    {
        switchMode = !switchMode;
        switchOn.SetActive(switchMode);
        switchOff.SetActive(!switchMode);
    }
}
