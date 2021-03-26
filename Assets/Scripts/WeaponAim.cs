using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    private float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        float horizontalAxeValue = Input.GetAxisRaw("RightHorizontal");
        float verticalAxeValue = Input.GetAxisRaw("RightVertical");

        // Arrow rotation management
        if (horizontalAxeValue != 0 || verticalAxeValue != 0)
        {
            Vector2 direction = new Vector2(horizontalAxeValue, verticalAxeValue);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
