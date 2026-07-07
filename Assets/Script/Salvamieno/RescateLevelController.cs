using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RescateLevelController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private List<GameObject> spawns = new List<GameObject>();
    [SerializeField] private GameObject personToFind;

    [SerializeField] private TMP_Text followRule;
    [SerializeField] private GameObject dron;
    [SerializeField] private GameObject camilla;
    [SerializeField] private Transform flecha;
    [SerializeField] private Transform mando2dron;
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
        int randomValue = Random.Range(0, spawns.Count);
        personToFind.transform.position = spawns[randomValue].transform.position;
        personToFind.SetActive(true);
        EnterState(SaveState.BeforeFound);
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
                followRule.text = "Encuentre a la persona desaparecida en la montaña";
            break;
            case SaveState.AfterFound:
                followRule.text = "Diregete al inicio y recoge el material de rescate (cambio de dron). Posteriormente hazlo llegar a la persona encontrada";
                flecha.position = mando2dron.position + Vector3.up * 2f;
                flecha.gameObject.SetActive(true);
            break;
            case SaveState.MaterialLeft:
                followRule.text = "Recoge la camilla con la persona en ella y dirigete lentamente a la zona segura";
            break;
            case SaveState.PersonResued:
                followRule.text = "PERSONA RESCATADA CON EXITO!. Muy buen trabajo";
            break;
        }
    }
}
