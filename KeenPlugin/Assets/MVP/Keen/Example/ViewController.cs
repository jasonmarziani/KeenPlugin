using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    public Text domainText;
    public InputField domainInput;
    public Text eventCodeText;
    public InputField eventCodeInput;

    private string _domainDefault;
    private string _eventCodeDefault;
    private KeenTracking _tracking;


    // Start is called before the first frame update
    void Start()
    {
        _domainDefault = domainText.text;
        domainText.text = string.Format(_domainDefault, KeenTracking.DOMAIN);
        
        _eventCodeDefault = eventCodeText.text;
        eventCodeText.text = string.Format(_eventCodeDefault, KeenTracking.KEEN_PROJECT_ID);
    }

    public void onDomainUpdate()
    {
        KeenTracking.DOMAIN = domainInput.text;
        domainText.text = string.Format(_domainDefault, KeenTracking.DOMAIN);
    }

    // Update is called once per frame
    public void onEventCodeUpdate()
    {
        KeenTracking.KEEN_PROJECT_ID = eventCodeInput.text;
        eventCodeText.text = string.Format(_eventCodeDefault, KeenTracking.KEEN_PROJECT_ID);
    }
}
