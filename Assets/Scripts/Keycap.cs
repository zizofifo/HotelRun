using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Keycap : MonoBehaviour
{
    public enum KeycapAppearance
    {
        Enabled,
        Disabled,
        PleaseWait,
    };

    public char keycapLetter;
    public KeycapAppearance appearance = KeycapAppearance.Enabled;

    private TextMeshProUGUI keycapTMP;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        keycapTMP = transform.Find("KeycapPrint").GetComponent<TextMeshProUGUI>();
        keycapTMP.text = keycapLetter.ToString();
        SetAppearance(appearance);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAppearance(KeycapAppearance appearance)
    {
        switch (appearance)
        {
            case KeycapAppearance.Enabled:
                this.appearance = KeycapAppearance.Enabled;
                anim.SetBool("isEnabled", true);
                anim.SetBool("isWaiting", false);
                break;
            case KeycapAppearance.Disabled:
                this.appearance = KeycapAppearance.Disabled;
                anim.SetBool("isEnabled", false);
                anim.SetBool("isWaiting", false);
                break;
            case KeycapAppearance.PleaseWait:
                this.appearance = KeycapAppearance.PleaseWait;
                anim.SetBool("isEnabled", false);
                anim.SetBool("isWaiting", true);
                break;
        }
    }
}
