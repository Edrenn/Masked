using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool rotatingRight = true;
    [SerializeField] private float maxRight;
    [SerializeField] private float maxLeft;
    private float currentRotationSpeed;
    private float limitRight;
    private float limitLeft;

    private List<ProjectileLauncher> canons;

    public bool isFiring = false;

    private void Start()
    {
        currentRotationSpeed = rotationSpeed;
        limitLeft = maxLeft;
        limitRight = maxRight;
        canons = new List<ProjectileLauncher>();
        canons.AddRange(GetComponentsInChildren<ProjectileLauncher>());
    }

    void FixedUpdate()
    {
        // turn right
        if (rotatingRight && this.transform.rotation.eulerAngles.z < limitRight)
            this.transform.rotation = new Quaternion() { eulerAngles = new Vector3(0, 0, this.transform.rotation.eulerAngles.z + (currentRotationSpeed * Time.deltaTime)) };
        else if (rotatingRight && this.transform.rotation.eulerAngles.z >= limitRight)
        {
            rotatingRight = false;
        }

        // turn left
        if (rotatingRight == false && this.transform.rotation.eulerAngles.z > limitLeft)
            this.transform.rotation = new Quaternion() { eulerAngles = new Vector3(0, 0, this.transform.rotation.eulerAngles.z - (currentRotationSpeed * Time.deltaTime)) };
        else if (rotatingRight == false && this.transform.rotation.eulerAngles.z <= limitLeft)
        {
            rotatingRight = true;
        }
    }

    public void StartFiring()
    {
        foreach (var canon in canons)
        {
            canon.StartFiring();
        }
    }

    public void StartHeavyFiring()
    {
        isFiring = true;
        foreach (var canon in canons)
        {
            canon.StartHeavyFire();
        }
        StartCoroutine(CenterTurretAndStartHeavyFire());
    }

    private IEnumerator CenterTurretAndStartHeavyFire()
    {
        currentRotationSpeed = 300;
        limitRight = 180;
        limitLeft = 180;
        yield return new WaitForSeconds(2f);

        currentRotationSpeed = rotationSpeed;
        limitRight = maxRight;
        limitLeft = maxLeft;
    }



    public void StopHeavyFiring()
    {
        isFiring = false;
        foreach (var canon in canons)
        {
            canon.StopHeavyFire();
        }
    }
}
