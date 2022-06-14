using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Transform m_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_transform.position += new Vector3(-0.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_transform.position += new Vector3(0.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_transform.position += new Vector3(0, 0.01f, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_transform.position += new Vector3(0, -0.01f, 0);
        }
    }
}
