using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDetector : MonoBehaviour
{
    [SerializeField] private StopPlayerFromLeavingScenario limitController;


    //Si el limite utiliza un collider trigger es un softLimit que solo da un aviso, este trigger tiene que ocupar todo el espacio en el que quieras que aparezca el mensaje
    //Si el collider no es trigger es un hardLimit y instantaneamente se carga el dron, el hardlimit tiene que ser fino


    private void OnCollisionEnter(Collision collision)
    {
        limitController.ReachedLimits(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        limitController.EnteredBoundary();
    }

    private void OnTriggerExit(Collider other)
    {
        limitController.LeftBoundary();   
    }
    public void SetLimitController(StopPlayerFromLeavingScenario limitController)
    {
        this.limitController = limitController;
    }
}
