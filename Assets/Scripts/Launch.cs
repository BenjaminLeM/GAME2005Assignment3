
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//using UnityEngine.UIElements;

public class Launch : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Body body;
    public float launchSpeed = 0.0f;
    public float launchPitch = 0.0f;
    public float launchYaw = 0.0f;
    public float drag = 1.0f;
    public float gravity = 1.0f;
    private float startX, startY;
    private float mouseX, mouseY;
    private float mouseVelX, mouseVelY;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Shoot()
    {
        GameObject newObject = Instantiate(projectilePrefab);
        newObject.transform.position = transform.position;
        body = newObject.GetComponent<Body>();
        body.vel = new Vector3(launchSpeed * (Mathf.Cos(Mathf.Deg2Rad * launchYaw) * Mathf.Cos(Mathf.Deg2Rad * launchPitch)),
                                launchSpeed * (Mathf.Sin(Mathf.Deg2Rad * launchYaw) * Mathf.Cos(Mathf.Deg2Rad * launchPitch)),
                                launchSpeed * (Mathf.Sin(Mathf.Deg2Rad * launchPitch)));
        body.drag = drag;
        body.grav = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startX = Input.mousePosition.x;
            startY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseX = Input.mousePosition.x;
            mouseY = Input.mousePosition.y;
            mouseVelX = Mathf.Pow((mouseX - startX), 2);
            mouseVelY = Mathf.Pow((mouseY - startY), 2);
            launchSpeed = 0.1f * Mathf.Sqrt(mouseVelX + mouseVelY);
            launchYaw = Mathf.Rad2Deg * Mathf.Atan2(mouseVelY, mouseVelX);
            Debug.Log(launchYaw);
            Shoot();
        }
    }
}