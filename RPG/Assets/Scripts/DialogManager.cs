using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialog;
    public int currentLine;

    private bool justStarted;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Jump") )
            {
                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialog.Length)
                    {
                        currentLine = -1;
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogActive = false;
                    }
                    else
                    {
                        CheckIfName();
                        dialogText.text = dialog[currentLine];
                    }
                }
                else
                    justStarted = false;
            }
        }
    }

    public void ShowDialog(string[] newdialog, bool isPerson)
    {
        dialog = newdialog;
        currentLine = 0;

        CheckIfName();
        dialogText.text = dialog[currentLine];
        dialogBox.SetActive(true);

        justStarted = true;

        nameBox.SetActive(isPerson);
 
        GameManager.instance.dialogActive = true;
    }

    private void CheckIfName()
    {
        if (dialog[currentLine].StartsWith("n-"))
        {
            nameText.text = dialog[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
}
