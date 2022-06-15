using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Transform m_transform;
    public float m_zAngle;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        m_zAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
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
                if (m_zAngle < 0)
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
                dstAngle = m_zAngle;
            }
        }

        if (Mathf.Abs(dstAngle - m_zAngle) > 180)
        {
            if (m_zAngle < -180)
            {
                m_zAngle += 360.0f;
            }
            else if (m_zAngle > 180)
            {
                m_zAngle -= 360;
            }
            else if (dstAngle < 0)
            {
                m_zAngle -= 360;
            }
            else if (dstAngle > 0)
            {
                m_zAngle += 360;
            }
        }


        float offAngle = 0.0f;
        if (m_zAngle > dstAngle)
        {
            offAngle = -90.0f * Time.deltaTime;
            if (m_zAngle + offAngle <= dstAngle)
            {
                offAngle = dstAngle - m_zAngle;
                m_zAngle = dstAngle;
            }
            else
            {
                m_zAngle += offAngle;
            }
        }
        else if (m_zAngle < dstAngle)
        {
            offAngle = 90.0f * Time.deltaTime;
            if (m_zAngle + offAngle >= dstAngle)
            {
                offAngle = dstAngle - m_zAngle;
                m_zAngle = dstAngle;
            }
            else
            {
                m_zAngle += offAngle;
            }
        }

        m_transform.Rotate(new Vector3(0, 0, offAngle), Space.Self);

        if (Input.GetKey(KeyCode.A))
        {
            m_transform.position += new Vector3(-1f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_transform.position += new Vector3(0, -1f * Time.deltaTime, 0);
        }
    }
}
