using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the arrow that appears when the player is aiming
/// </summary>
public class AimArrow : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Slider fireLoadingSlider;
    [SerializeField] private float fillingSpeed = .9f;
    [SerializeField] private GameObject sliderBackground;
    [SerializeField] private GameObject sliderFillArea;
    [SerializeField] private Image fillImage;
    [SerializeField] private FireBehaviour fireBehaviour;
    
    void Start()
    {
        fireLoadingSlider = GetComponent<Slider>();
        HideAimArrow();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalAxeValue = Input.GetAxisRaw("RightHorizontal");
        float verticalAxeValue = Input.GetAxisRaw("RightVertical");

        // Arrow rotation management
        if (horizontalAxeValue != 0||verticalAxeValue  != 0)
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

        if (sliderFillArea.activeInHierarchy)
        {
            fireLoadingSlider.value += fillingSpeed;
        }

        // Left mouse released
        if (Input.GetButtonUp("Fire1"))
        {
            HideAimArrow();
        }
    }

    public void ShowAimArrow()
    {
        fireLoadingSlider.value = 0;
        sliderBackground.SetActive(true);
        sliderFillArea.SetActive(true);
    }

    public void HideAimArrow()
    {
        sliderBackground.SetActive(false);
        sliderFillArea.SetActive(false);
    }

    public void ChangeArrowColor()
    {
        if (fireLoadingSlider.value == 100)
        {
            fillImage.color = Color.green;
            fireBehaviour.CanFire = true;
        }
        else
            fillImage.color = Color.red;
    }
}
