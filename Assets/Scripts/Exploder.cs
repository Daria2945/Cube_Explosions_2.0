using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Exploder : MonoBehaviour
{
    private float _explosionRadius = 50;
    private float _explosionForse = 250;

    private Rigidbody _objectToExplode;

    private void Start()
    {
        _objectToExplode = GetComponent<Rigidbody>();
        ChangeForseAndRadiuse();
    }

    public void BlowUp()
    {
        foreach (Rigidbody explodableObgect in GetExplodableObjects(_explosionRadius))
            explodableObgect.AddExplosionForce(_explosionForse, transform.position, _explosionRadius);

        Destroy(_objectToExplode.gameObject);
    }

    private List<Rigidbody> GetExplodableObjects(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }

    private void ChangeForseAndRadiuse()
    {
        Vector3 halfScale = new(0.5f, 0.5f, 0.5f);
        Vector3 quarterScale = new(0.25f, 0.25f, 0.25f);

        if(_objectToExplode.transform.localScale == halfScale)
        {
            _explosionRadius = 100;
            _explosionForse = 250;
        }
        else if(_objectToExplode.transform.localScale == quarterScale)
        {
            _explosionRadius = 150;
            _explosionForse = 300;
        }
        else
        {
            _explosionRadius = 200;
            _explosionForse = 350;
        }
    }
}