using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Keycap : MonoBehaviour
{

    public TextMeshProUGUI keycapTMP;
    public char keycapLetter;
    public bool enabledAppearance = true;

    private Image image;

    public GameObject KEYCAP_DISABLESTROKE_GAMEOBJECT;
    public Sprite KEYCAP_ENABLED_SPRITE;
    public Sprite KEYCAP_DISABLED_SPRITE;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
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


    public void SetEnableAppearance(bool state)
    {
        if (state)
        {
            enabledAppearance = true;
            KEYCAP_DISABLESTROKE_GAMEOBJECT.SetActive(false);
            image.overrideSprite = KEYCAP_ENABLED_SPRITE;
        }
        else
        {
            enabledAppearance = false;
            KEYCAP_DISABLESTROKE_GAMEOBJECT.SetActive(true);
            image.overrideSprite = KEYCAP_DISABLED_SPRITE;
        }
    }
}
