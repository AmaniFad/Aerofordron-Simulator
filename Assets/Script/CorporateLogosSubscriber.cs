using UnityEngine;
using UnityEngine.UI;
using static CorporateLogosManager;

public class CorporateLogosSubscriber : MonoBehaviour
{

    [SerializeField] private bool smallLogo;
    [SerializeField] private bool normalLogo;
    Image logo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logo = GetComponent<Image>();
        CorporateLogosManager.instance.SubscribeItem(this);
        if (CorporateLogosManager.instance.currentCorportaion != null)
            SetCorporateImages(CorporateLogosManager.instance.currentCorportaion);
        else 
            logo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCorporateImages(Corporation corporation)
    {
        if (smallLogo)
            logo.sprite = corporation.smallLogo;
        else
            logo.sprite = corporation.fullLogo;

    }
}
