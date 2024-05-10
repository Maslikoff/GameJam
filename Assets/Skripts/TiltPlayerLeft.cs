using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltPlayerLeft : MonoBehaviour
{
    [SerializeField] private float forceDrop = 5000f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.rigidbody.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.right * forceDrop, ForceMode.Impulse);
            }
        }
    }
}
