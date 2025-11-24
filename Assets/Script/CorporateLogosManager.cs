using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorporateLogosManager : MonoBehaviour
{
    [System.Serializable]
    public class Corporation
    {
        public string name;
        public Sprite smallLogo;
        public Sprite fullLogo;
    }

    public static CorporateLogosManager instance;
    List<CorporateLogosSubscriber> subscribedItems;
    [SerializeField] private Corporation[] corporations;
    public Corporation currentCorportaion;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        subscribedItems = new List<CorporateLogosSubscriber>();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SubscribeItem(CorporateLogosSubscriber item)
    {
        subscribedItems.Add(item);
    }
    public void ChangeCorporation(string name)
    {
        foreach (Corporation item in corporations)
        {
            if (item.name == name) 
            {
                currentCorportaion = item;
                SetImages(item);
            }
        }
    }

    public void SetImages(Corporation corp)
    {
        foreach (CorporateLogosSubscriber subscriber in subscribedItems)
        {
            subscriber.SetCorporateImages(corp);
        }
    }
    
}
