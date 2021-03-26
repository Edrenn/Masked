using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneezeRange : MonoBehaviour
{
    List<IInfectable> allInfectablesInRange;

    public List<IInfectable> GetAllInfectablesInRange()
    {
        return allInfectablesInRange;
    }

    private void Start()
    {
        allInfectablesInRange = new List<IInfectable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInfectable infectableObject = collision.GetComponent<IInfectable>();
        if (infectableObject != null)
        {
            allInfectablesInRange.Add(infectableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInfectable infectableObject = collision.GetComponent<IInfectable>();
        if (infectableObject != null)
        {
            allInfectablesInRange.Remove(infectableObject);
        }
    }
}
