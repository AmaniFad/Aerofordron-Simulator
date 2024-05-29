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

    public bool isFirstPart;
    public bool isSecondPart;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFirstPart)
            {
                OnFirstPart.Invoke();
                isFirstPart = true;
            }
            else if (TutorialController.instance.GetFirstRound())
            {
                if (!isSecondPart)
                {
                    OnSecondPart.Invoke();
                    isSecondPart = true;
                }
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
