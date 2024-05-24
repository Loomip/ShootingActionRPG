using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // 체력 올라갈 량
    [SerializeField] private int hpAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PHealth pHealth = other.GetComponent<PHealth>();
            if(pHealth != null )
            {
                pHealth.Heal(hpAmount);
            }
        }

        Destroy(gameObject);
    }
}
