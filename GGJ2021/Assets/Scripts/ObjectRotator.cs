using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// rotate rotate rotate that dowsing rod
/// </summary>
public class ObjectRotator : MonoBehaviour
{
    [SerializeField] bool rotateAroundParent = false;
    [SerializeField] Transform lookTarget;

    Vector2 input;
    Vector3 mousePos;
    Vector3 difference;

    // Update is called once per frame
    void Update()
    {
        if (lookTarget == null) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

             difference = mousePos - transform.position;
        }
        else {
             difference = lookTarget.position - transform.position;
        }
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);


        if (rotateAroundParent) {
            transform.RotateAround(transform.parent.transform.position, new Vector3(0, 0, 1), 100 * Time.deltaTime);
        }


    }
}
