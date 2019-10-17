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
    private MVPMetricsTracking _tracking;


    // Start is called before the first frame update
    void Start()
    {
        _domainDefault = domainText.text;
        domainText.text = string.Format(_domainDefault, MVPMetricsTracking.DOMAIN);
        
        _eventCodeDefault = eventCodeText.text;
        eventCodeText.text = string.Format(_eventCodeDefault, MVPMetricsTracking.PROJECT_ID);
    }

    public void onDomainUpdate()
    {
        MVPMetricsTracking.DOMAIN = domainInput.text;
        domainText.text = string.Format(_domainDefault, MVPMetricsTracking.DOMAIN);
    }

    // Update is called once per frame
    public void onEventCodeUpdate()
    {
        MVPMetricsTracking.PROJECT_ID = eventCodeInput.text;
        eventCodeText.text = string.Format(_eventCodeDefault, MVPMetricsTracking.PROJECT_ID);
    }
}
