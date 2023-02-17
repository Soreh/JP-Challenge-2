using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] float minRotationSpeed = 30f;
    [SerializeField] float maxRotationSpeed = 75f;

    private float randomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up * randomSpeed * Time.deltaTime);
    }
}
