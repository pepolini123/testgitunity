using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if __DEBUD_AVAILABLE__

using UnityEditor;

#endif

public class GameManager : MonoBehaviour
{
    public Transform[] dialogCommon;
    public Transform[] dialogCharacters;
    public Transform dialogText;

    [System.Serializable]
    public struct DialogData //Tiene mucha ventaja de rendimiento el crear structs cuando tienes que usar más de 3-4 argumentos
    {
        public int character;
        public string text;
    };

    public DialogData[] dialogsData;

    bool showingDialog;

    TextMeshPro dialogTextC;

    int dialogIndex = 1;

    KeyCode[] debugKey = { KeyCode.S, KeyCode.T, KeyCode.A, KeyCode.R };
    int debugKeyProgress = 0;
    // Start is called before the first frame update
    void Start()
    {
        showingDialog = false;

        dialogTextC = dialogText.GetComponent<TextMeshPro>();
    }


#if __DEBUD_AVAILABLE__

    void OnDrawGizmos()
    {
        if(Switches.debugMode && Switches.debugDialogs)
        {
            if(showingDialog)
            Handles.color = Color.white;
            Handles.Label(dialogText.position - Vector3.up * 1.0f, "Dialog Id: " + dialogIndex);
        }
        
    }

#endif

    // Update is called once per frame
    void Update()
    {
#if __DEBUD_AVAILABLE__

        if(Switches.debugMode && Switches.debugDialogs)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                showingDialog = true;
                dialogIndex = 0;
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                dialogIndex = (dialogIndex + 1) % dialogsData.Length; //Así siempre irá pasando por el tamapo de dialogos máximos, gracias al %
            }
        }

#endif

        if(showingDialog)
        {
            for(int i = 0; i < dialogCommon.Length; i++)
            {
                dialogCommon[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < dialogCharacters.Length; i++)
            {
                dialogCharacters[i].gameObject.SetActive(false);
            }

            int character = dialogsData[dialogIndex].character;
            string text = dialogsData[dialogIndex].text;

            dialogCharacters[character].gameObject.SetActive(true);
            dialogTextC.text = text;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                showingDialog = false;
            }

        }
        else
        {
            for (int i = 0; i < dialogCommon.Length; i++)
            {
                dialogCommon[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < dialogCharacters.Length; i++)
            {
                dialogCharacters[i].gameObject.SetActive(false);
            }
           
        }

#if __DEBUD_AVAILABLE__

        if(!Switches.debugMode)
        {
            if(Input.GetKeyDown(debugKey[debugKeyProgress]))
            {
                debugKeyProgress++;
                if(debugKeyProgress == debugKey.Length)
                {
                    Switches.debugMode = true;
                    Debug.Log("Debug mode on");
                }
            }
        }

#endif
    }

    public void OnTriggerDialog(int index)
    {
        showingDialog = true;
        dialogIndex = index;
    }

    public bool IsShowingDialog()
    {
        return showingDialog;
    }
}
