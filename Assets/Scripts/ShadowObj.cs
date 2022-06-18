using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShadowObj : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.ShadowCaster2D caster;
    public PlayerCtrl player;
    public bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        caster.enabled = isOn;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnShadow(bool ison)
    {
        isOn = ison;

        caster.enabled = ison;
    }
}
