using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{

    public TextMeshProUGUI prologueText;
    public AudioSource audioSource;
    public Image fadeOutImage;

    List<string> prologueLines = new List<string>() {
        "Long ago, when the world was first created, four beings were brought into existence.",
        "These beings represented the four aspects of life necessary to maintain balance in the world: water, earth, fire, and air.",
        "They are known as the elemental entities, and for millennia they have roamed the world, acting in harmony to preserve balance and to protect the world from being corrupted.",
        "The entities were regarded as deities by humanity for their influence over the world.",
        "Temples were built for them and in the capital city of Creley, the Knights Templar was formed, for the entities are not all-powerful, and there are some who seek to take advantage of their benevolent and pacifist nature.",
        "Many have tried to seize the entities so that they may hold their power in their own hands, but the Knights Templar have always managed to prevent their efforts.",
        "Except for Aldruin’s.",
        "Aldruin is a powerful sorcerer of unknown origin.",
        "As a master of the dark arts, he overpowered nearly all the templars of Creley and took control of the four entities.",
        "With the balance of the world no longer being maintained, corruption spread throughout the land, causing unnatural phenomena wherever it reached.",
        "Your name is Everard, a newly knighted templar.",
        "As a new recruit, you were not present at the battle of Nightwell Keep, where Aldruin resides.",
        "As such, you are one of the few remaining templar knights.",
        "However, when word arrived that the templars at Nightwell Keep had been wiped out, humanity fell into despair, and the remaining templars all lost heart.",
        "All but you.",
        "Refusing to give up, you bided your time and trained, preparing for your encounter with Aldruin.",
        "As the fateful day approaches, you set out to Nightwell Keep, located deep within a forest now known as the Wretched Woods."
    };

    private void Start()
    {
        StartCoroutine(ReadPrologue());
    }

    void StartGameplay()
    {
        GameSceneManager.Instance.LoadNextScene("Sandbox");
    }

    // Start is called before the first frame update
    IEnumerator ReadPrologue()
    {
        prologueText.text = "";

        // Wait 2.5 seconds before starting dialogue
        yield return new WaitForSeconds(2.5f);

        // Iterate over each dialogue line in the prologue
        foreach (string dialogueLine in prologueLines)
        {
            // Iterate over each character in the dialogue line
            foreach (char character in dialogueLine)
            {
                // Append to TMP prologueText current character
                prologueText.text += character;
                // Play dialogue blip sound
                audioSource.Play();
                // Pause for 0.05 seconds after each character
                yield return new WaitForSeconds(0.05f);
                // if character is a comma or colon pause for an extra second
                if (character == ',' || character == ':')
                {
                    yield return new WaitForSeconds(1.0f);
                }
            }
            // Pause for 4 seconds after each line
            yield return new WaitForSeconds(4.0f);
            // Clear TMP prologueText
            prologueText.text = "";
        }

        // After dialogue completes start fading to black
        for (int i = 0; i < 100; i++)
        {
            fadeOutImage.color = new Color(0, 0, 0, Mathf.Min(1, fadeOutImage.color.a + +0.01f));
            yield return new WaitForSeconds(0.05f);
        }

        // Pause one second before tranisitioning from black to sandbox
        yield return new WaitForSeconds(1.0f);
        StartGameplay();
    }

}
