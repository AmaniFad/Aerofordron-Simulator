using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RescateLevelController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private List<Transform> spawns = new List<Transform>();
    [SerializeField] private List<GameObject> textLevel = new List<GameObject>();
    [SerializeField] private GameObject personToFind;

    [SerializeField] private TMP_Text followRule;
    [SerializeField] private GameObject dron;
    [SerializeField] private GameObject camilla;
    [SerializeField] private Transform flecha;
    [SerializeField] private Transform mando2dron;
    int i;
    public enum SaveState
    {
        BeforeFound,
        AfterFound,
        MaterialLeft,
        PersonResued,

    }
    public SaveState currentState;
    void Start()
    {
        i = 0;
        int randomValue = Random.Range(0, spawns.Count);
        personToFind.transform.position = spawns[randomValue].position;
        personToFind.SetActive(true);
        StartCoroutine(WaitSomeSeconds(5));
    }

    
    void Update()
    {
        if(Vector3.Distance(personToFind.transform.position, dron.transform.position) < 50f)
        {
            EnterState(SaveState.AfterFound);
        }
        if(Vector3.Distance(personToFind.transform.position, camilla.transform.position) < 1f)
        {
            Transform hijoTransform = camilla.transform.Find("man_low");
            if (hijoTransform != null)
            {
                hijoTransform.gameObject.SetActive(true);
                personToFind.SetActive(false);
                EnterState(SaveState.MaterialLeft);
            }
        }
    }
    private void EnterState(SaveState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case SaveState.BeforeFound:
                textLevel[i].SetActive(false);
                i++;
                textLevel[i].SetActive(true);
                break;
            case SaveState.AfterFound:
                textLevel[i].SetActive(false);
                i++;
                textLevel[i].SetActive(true);
                flecha.position = mando2dron.position + Vector3.up * 2f;
                flecha.gameObject.SetActive(true);
            break;
            case SaveState.MaterialLeft:
                textLevel[i].SetActive(false);
                i++;
                textLevel[i].SetActive(true);
            break;
            case SaveState.PersonResued:
                textLevel[i].SetActive(false);
                i++;
                textLevel[i].SetActive(true);
            break;
            default:
                Debug.LogError("no state assigned");
            break;
        }
    }
    private IEnumerator WaitSomeSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        EnterState(SaveState.BeforeFound);
    }
}
