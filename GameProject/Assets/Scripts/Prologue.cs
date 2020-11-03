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

    bool startFading = false;
    float sentencePauseTime;
    float timePerCharacter = 0.05f;
    //float timePerCharacter = 0.0f;
    float timeStarted;
    float timeSinceLastCharacter;
    bool lineComplete = false;
    int prologueLineIndex = 0;
    int characterIndex = 0;

    List<string> prologueLines = new List<string>() {
        "Long ago, when the world was first created, four beings were brought into existence.",
        "These beings represented the four aspects of life necessary to maintain balance in the world: water, earth, fire, and air.",
        "They are known as the elemental entities, and for millennia, they have roamed the world, acting in harmony to preserve balance and to protect the world from being corrupted.",
        "The entities were regarded as deities by humanity for their influence over the world.",
        "Temples were built for them and in the capital city of Creley, the Knights Templar was formed, for the entities are not all powerful, and there are some who seek to take advantage of their benevolent and pacifist nature.",
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




    // Start is called before the first frame update
    void Start()
    {
        timeStarted = Time.time;
        timeSinceLastCharacter = Time.time;
    }

    void StartGameplay()
    {
        GameSceneManager.Instance.LoadNextScene("Sandbox");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >  timeStarted + 3.0f)
        {
            if (!lineComplete)
            {
                if (Time.time >= timeSinceLastCharacter + timePerCharacter)
                {
                    prologueText.text = prologueLines[prologueLineIndex].Substring(0, characterIndex);
                    audioSource.Play();
                    characterIndex++;
                    lineComplete = characterIndex == prologueLines[prologueLineIndex].Length + 1;
                    timeSinceLastCharacter = Time.time;
                }
            }
            else
            {
                if (Time.time >= timeSinceLastCharacter + prologueLines[Mathf.Min(prologueLineIndex, prologueLines.Count - 1)].Length * timePerCharacter * 1.2f)
                {
                    lineComplete = false;
                    characterIndex = 0;
                    prologueLineIndex++;
                }
                if (prologueLineIndex == prologueLines.Count - 1)
                {
                    lineComplete = true;
                }
            }

            if (lineComplete && prologueLineIndex == prologueLines.Count - 1)
            {
                Invoke("StartGameplay", 10.0f);
                startFading = true;
            }

            if (startFading)
            {
                if (Time.time > timeSinceLastCharacter + timePerCharacter)
                {
                    timeSinceLastCharacter = Time.time;
                    fadeOutImage.color = new Color(0, 0, 0, Mathf.Min(255, fadeOutImage.color.a + + 0.008f));
                }
            }
        }
    }
}
