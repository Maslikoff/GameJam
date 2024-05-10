using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltPlayerRight : MonoBehaviour
{
    [SerializeField] private float forceDrop = 5000f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.rigidbody.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.left * forceDrop, ForceMode.Impulse); 
            }
        }
    }
}
