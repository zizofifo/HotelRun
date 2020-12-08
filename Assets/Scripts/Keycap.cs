using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Keycap : MonoBehaviour
{
    public char keycapLetter;
    public bool enabledAppearance = true;

    private TextMeshProUGUI keycapTMP;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        keycapTMP = transform.Find("KeycapPrint").GetComponent<TextMeshProUGUI>();
        keycapTMP.text = keycapLetter.ToString();
        if (enabledAppearance)
        {
            SetEnableAppearance(true);
        }
        else
        {
            SetEnableAppearance(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetEnableAppearance(bool enableKeycap)
    {
        if (enableKeycap)
        {
            enabledAppearance = true;
            anim.SetBool("isEnabled", true);
        }
        else
        {
            enabledAppearance = false;
            anim.SetBool("isEnabled", false);
        }
    }
}
