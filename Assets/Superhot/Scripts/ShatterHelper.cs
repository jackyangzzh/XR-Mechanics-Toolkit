using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShatterHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject shatterObject = null;

    [SerializeField]
    private float shatterForce = 300f;

    [SerializeField]
    private float shatterRadius = 3f;

    private void Start()
    {
        Shatter();
    }

    public void Shatter()
    {
        if (shatterObject == null)
        {
            Debug.LogWarning("Shatter object cannot be empty");
            return;
        }

        GameObject shatteredObj = Instantiate(shatterObject, transform.position, transform.rotation);
        foreach (Transform t in shatteredObj.transform)
        {
            if (t.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(shatterForce, transform.position, shatterRadius);
            }
        }

        Destroy(shatteredObj, 2f);
        Destroy(gameObject);
    }
}
