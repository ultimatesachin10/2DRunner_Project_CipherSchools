using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKeyRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime * 3f);
    }
}
