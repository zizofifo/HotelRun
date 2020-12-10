using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Movement player;
    private TextMeshProUGUI uitTimer; //Timer text
    private TextMeshProUGUI tmpSodas;
    private Keycap sodasKeycap;
    private TextMeshProUGUI tmpTowels;
    private Keycap towelsKeycap;
    private Keycap warpKeycap;

    public float timer = 0.0f;
    public int seconds;
    public int minutes;

    void Awake()
    {
        uitTimer = transform.Find("UITimer_Text").GetComponent<TextMeshProUGUI>();

        tmpSodas = transform.Find("TMPSodas_Text").GetComponent<TextMeshProUGUI>();
        sodasKeycap = transform.Find("TMPSodas_Text/SodaKeycap").GetComponent<Keycap>();

        tmpTowels = transform.Find("TMPTowels_Text").GetComponent<TextMeshProUGUI>();
        towelsKeycap = transform.Find("TMPTowels_Text/TowelKeycap").GetComponent<Keycap>();

        warpKeycap = transform.Find("TMPWarp_Text/WarpKeycap").GetComponent<Keycap>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60);

        UpdateGUI();
    }

    void UpdateGUI()
    {
        string timerText = string.Format("{0:0}:{1:00}", minutes, seconds);

        tmpSodas.text = "Sodas:   " + string.Format("{0:#0}", player.sodaCans);

        sodasKeycap.SetAppearance(player.isMotivated ?
            Keycap.KeycapAppearance.PleaseWait :
            !player.isMotivated && player.sodaCans > 0 ? Keycap.KeycapAppearance.Enabled : Keycap.KeycapAppearance.Disabled);

        tmpTowels.text = "Towels:  " + string.Format("{0:#0}", player.towels);

        towelsKeycap.SetAppearance(player.isUsingTowel ?
            Keycap.KeycapAppearance.PleaseWait :
            !player.isUsingTowel && player.towels > 0 ? Keycap.KeycapAppearance.Enabled : Keycap.KeycapAppearance.Disabled);

        warpKeycap.SetAppearance(player.isBeingWarped ?
            Keycap.KeycapAppearance.PleaseWait :
            player.canWarp ? Keycap.KeycapAppearance.Enabled : Keycap.KeycapAppearance.Disabled);

        uitTimer.text = "Time: " + timerText;
    }
}
