using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    public Text eventCodeText;
    public InputField eventCodeInput;

    private string _eventCodeDefault;
    private KeenTracking _tracking;


    // Start is called before the first frame update
    void Start()
    {
        _eventCodeDefault = eventCodeText.text;
        eventCodeText.text = string.Format(_eventCodeDefault, KeenTracking.KEEN_PROJECT_ID);
    }

    // Update is called once per frame
    public void onEventCodeUpdate()
    {
        KeenTracking.KEEN_PROJECT_ID = eventCodeInput.text;
        eventCodeText.text = string.Format(_eventCodeDefault, KeenTracking.KEEN_PROJECT_ID);
    }
}
