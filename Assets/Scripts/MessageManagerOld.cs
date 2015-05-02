using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum MessageTypes { intro, win, scrap, artifact }

public class MessageManagerOld : MonoBehaviour
{

    public Image currentImage;
    public int currentIndex;
    public Text currentText;
    public GameObject canvas;

    private List<Sprite> sprites;
    private List<string> texts;
    bool messageInProgress = false;

    public Sprite female;
    public Sprite male;
    public Sprite help;

    public List<MessageTypes> usedTypes = new List<MessageTypes>();

    void Awake()
    {
        GlobalSettings.messageManager = this;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (messageInProgress && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) ))
        {
            NextText();
        }
    }

    void NextText()
    {
        currentIndex++;
        if (currentIndex > texts.Count - 1)
        {
            messageInProgress = false;
            GlobalSettings.TogglePause(false);
            canvas.SetActive(false);
        }
        else
        {
            currentText.text = texts[currentIndex];
            currentImage.sprite = sprites[currentIndex];
        }
    }

    public void SendMessage(MessageTypes MessageTypes)
    {
        if (!usedTypes.Contains(MessageTypes))
        {
            usedTypes.Add(MessageTypes);
            GlobalSettings.TogglePause(true);
            currentIndex = -1;
            messageInProgress = true;
            canvas.SetActive(true);

            switch (MessageTypes)
            {
                case MessageTypes.intro:
                    texts = new List<string>() {"*HELP* Press space/enter to proceed.",
                    "Jackson: \"HQ, I've arrived at the anomaly.\"",
                    "HQ: \"Greetings commander, well done. Begin extraction of artifacts immediately.\"",
                    "Jackson: \"There's a problem, I'm detecting high amounts of space distortion. I may need to improve the shield capacity of my vessel.\"",
                    "HQ: \"Affirmative. Use of deep space automation facility approved.\"",
                    "Jackson: \"I'm going to need resources, maybe there's some useful materials inside the anomaly. Wish me luck HQ, I'm going in.\"",
                    "HQ: \"Affirmative.\"",
                    "HQ: \"... And Jackson?\"",
                    "Jackson: \"Yeah?\"",
                    "HQ: \"Be careful.\"",
                    "Jackson: \"I will, Alice. Jackson out.\"",
                    "*HELP* Reach the middle of the anomaly, the closer to the middle you are, the harsher the environment is."};
                    sprites = new List<Sprite>() {help, male, female, male, female, male, female, female, male, female, male, help};
                    break;
                case MessageTypes.win:
                    texts = new List<string>() {"HQ: \"Jackson?\"",
                    "HQ: \"...\"",
                    "HQ: \"Jackson, please respond!\"",
                    "Jackson: *Static* \"..mazing, HQ, you need to see th..\" *Static*",
                    "HQ: \"Jackson, you're breaking up! Please return to the facility immediately!\"",
                    "Jackson: *static* \"..eres more beyond the horizon. I'll see you all beyo..\" *static*",
					"Jackson: \"... *static* ...\"",
					"HQ: \"... Jackson, please come in...\"",
					"Jackson: \"... *static* ...\"",
                    "HQ: \"All is not lost, ready a rescue party!\"",
                    "*TO BE CONTINUED...?*"};
                    sprites = new List<Sprite>() { female, female, female, male, female, male, male, female, male, female, help};
                    break;

                case MessageTypes.artifact:
                    texts = new List<string>() {"Jackson: \"HQ, I've found something. Sending analysis.\"",
                    "HQ: \"Jackson, that's incredible! Take this back to the facility, we might be able to use this for something.\"",
                    "*HELP* Artifacts are used for unlocking special upgrades, take it back the the facility to research it."};
                    sprites = new List<Sprite>() { male, female, help};
                    break;

                case MessageTypes.scrap:
                    texts = new List<string>() {"Jackson: \"There, some scrap. Alright, gotta take this back to the facility and upgrade my ship.\"",
                    "*HELP* Scrap is used for upgrading your ship, take it to the facility to refine it into materials."};
                    sprites = new List<Sprite>() {male, help};
                    break;
                default:
                    break;
            }

            NextText();
        }


    }


}
