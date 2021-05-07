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
            if (Input.GetButtonDown("Jump") )
            {
                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialog.Length)
                        dialogBox.SetActive(false);
                    else
                        dialogText.text = dialog[currentLine];
                }
                else
                    justStarted = false;
            }
        }
    }

    public void ShowDialog(string[] newdialog)
    {
        dialog = newdialog;
        currentLine = 0;
        dialogText.text = dialog[0];
        dialogBox.SetActive(true);

        justStarted = true;
    }
}
