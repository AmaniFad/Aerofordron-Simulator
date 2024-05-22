using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cone : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFirstPart;
    [SerializeField] private UnityEvent OnSecondPart;
    [SerializeField] private GameObject panel1; 
    [SerializeField] private GameObject panel2;

    private bool isfirstPart;
    private bool isSecondPart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isfirstPart)
            {
                OnFirstPart.Invoke();
                isfirstPart = true;
            }
            else if (!isSecondPart)
            {
                OnSecondPart.Invoke();
                isSecondPart = true;    
            }
        }
    }

    public void changePanel()
    {
        panel1.SetActive(true);
        StartCoroutine(_ChangeCanvas());
    }

    IEnumerator _ChangeCanvas()
    {
        yield return new WaitForSeconds(3.5f);
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
}
