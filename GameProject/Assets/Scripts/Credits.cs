using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public TextMeshProUGUI creditsText;
    public AudioSource musicSource;
    public Image BlackFade;


    List<string> credits = new List<string>()

    {
        "Miguel Tamayo - Team Leader/Programmer/Artist",
        "Adib Baji - Programmer",
        "Brandon Ankrum - Artist/Animations",
        "Jonathan Do - Programmer",
        "Jason Phung - Artist",
        "Joshua Dickens - Programmer",
        "Rishi Chitturi - Artist",
        "Jeff Onyemachi - Music/SFX",
        "Pouria Narimisa - Artist"
    };

    private void Start()
    {
        StartCoroutine(StartCredits());
    }

    IEnumerator StartCredits()
    {
        creditsText.text = "";

        // Wait 2.5 seconds before starting dialogue
        yield return new WaitForSeconds(2.5f);

        // Iterate over each dialogue line in the prologue
        foreach (string line in credits)
        {
            creditsText.text += line;
            creditsText.text += "\n";

          
            // Pause for 4 seconds after each line
            yield return new WaitForSeconds(1.0f);
        }

        // After dialogue completes start fading to black
        for (int i = 0; i < 100; i++)
        {
            BlackFade.color = new Color(0, 0, 0, Mathf.Min(1, BlackFade.color.a + +0.01f));
            yield return new WaitForSeconds(0.05f);
        }
    }

}
