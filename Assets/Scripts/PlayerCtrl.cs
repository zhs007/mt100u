using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // public Transform m_transform;
    public float zAngle;
    public float lastOffAngle;
    public DynamicJoystick joystick;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        joystick = GameObject.Find("Dynamic Joystick").GetComponent<DynamicJoystick>();
        // m_transform = this.transform;
        zAngle = 0.0f;

        mainCamera.transform.position.Set(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }

    void ProcKeyboard()
    {
        float dstAngle = 0.0f;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                dstAngle = 45.0f;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                dstAngle = 135.0f;
            }
            else
            {
                dstAngle = 90.0f;
            }
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                dstAngle = -45.0f;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                dstAngle = -135.0f;
            }
            else
            {
                dstAngle = -90.0f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                dstAngle = 0.0f;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                if (zAngle < 0)
                {
                    dstAngle = -180.0f;
                }
                else
                {
                    dstAngle = 180.0f;
                }
            }
            else
            {
                dstAngle = zAngle;
            }
        }

        if (Mathf.Abs(dstAngle - zAngle) > 180)
        {
            if (zAngle < -180)
            {
                zAngle += 360.0f;
            }
            else if (zAngle > 180)
            {
                zAngle -= 360;
            }
            else if (dstAngle < 0)
            {
                zAngle -= 360;
            }
            else if (dstAngle > 0)
            {
                zAngle += 360;
            }
        }


        float offAngle = 0.0f;
        if (zAngle > dstAngle)
        {
            offAngle = -90.0f * Time.deltaTime;
            if (zAngle + offAngle <= dstAngle)
            {
                offAngle = dstAngle - zAngle;
                zAngle = dstAngle;
            }
            else
            {
                zAngle += offAngle;
            }
        }
        else if (zAngle < dstAngle)
        {
            offAngle = 90.0f * Time.deltaTime;
            if (zAngle + offAngle >= dstAngle)
            {
                offAngle = dstAngle - zAngle;
                zAngle = dstAngle;
            }
            else
            {
                zAngle += offAngle;
            }
        }

        transform.Rotate(new Vector3(0, 0, offAngle), Space.Self);

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1f * Time.deltaTime, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcKeyboard();

        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            return;
        }

        // float offAngle = Vector3.Angle(Quaternion.AngleAxis(m_zAngle, Vector3.up) * Vector3.up, new Vector3(m_variableJoystick.Horizontal, m_variableJoystick.Vertical, 0));
        // float offAngle = Vector3.Angle(new Vector3(Mathf.Sin(m_zAngle * Mathf.Deg2Rad), Mathf.Cos(m_zAngle * Mathf.Deg2Rad), 0), new Vector3(m_variableJoystick.Horizontal, m_variableJoystick.Vertical, 0));
        float offAngle = Vector3.SignedAngle(new Vector3(Mathf.Sin(zAngle * Mathf.Deg2Rad), Mathf.Cos(zAngle * Mathf.Deg2Rad), 0), new Vector3(joystick.Horizontal, -joystick.Vertical, 0), Vector3.forward);
        // if ((offAngle < 0 && lastOffAngle < 0) || (offAngle > 0 && lastOffAngle > 0))
        {
            offAngle *= Time.deltaTime;
            // if (Mathf.Abs(offAngle) > 1)
            {
                transform.Rotate(new Vector3(0, 0, offAngle), Space.Self);
                zAngle += offAngle;

                zAngle %= 360;
            }
        }

        lastOffAngle = offAngle;

        // Debug.Log("za - " + m_zAngle + " oa - " + offAngle);
        // Debug.Log("za - " + new Vector3(Mathf.Sin(m_zAngle * Mathf.Deg2Rad), Mathf.Cos(m_zAngle * Mathf.Deg2Rad), 0) + " oa - " + new Vector3(m_variableJoystick.Horizontal, -m_variableJoystick.Vertical, 0));

        // m_transform.position += new Vector3(0, m_variableJoystick.Vertical * Time.deltaTime, 0);
        // m_transform.position += new Vector3(m_variableJoystick.Horizontal * Time.deltaTime, 0, 0);

        transform.position += new Vector3(-Mathf.Sin(zAngle * Mathf.Deg2Rad), Mathf.Cos(zAngle * Mathf.Deg2Rad), 0) * Time.deltaTime;

        mainCamera.transform.position += new Vector3(-Mathf.Sin(zAngle * Mathf.Deg2Rad), Mathf.Cos(zAngle * Mathf.Deg2Rad), 0) * Time.deltaTime;

        // Debug.Log("player - " + transform.position);
        // Debug.Log("camera - " + mainCamera.transform.position);
    }
}
