using System.Collections;
using TMPro;
using UnityEngine;

public class WriteSlowly : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This methos executes a coroutine that writes the text in the time it is given
    /// </summary>
    /// <param name="textToWrite">Text that will appear</param>
    /// <param name="timeToWrite">The time it will take for the entire text to be written, shorter time it writes faster and viceversa</param>
    public void WriteText(TextMeshProUGUI display,string textToWrite, float timeToWrite)
    {
        StartCoroutine(Write(display,textToWrite,timeToWrite));
    }

    private IEnumerator Write(TextMeshProUGUI display, string textToWrite, float timeToWrite)
    {
        print(textToWrite);
        display.text = "";
        int stringIndex = 0;
        while (stringIndex < textToWrite.Length)
        {

            display.text += textToWrite.Substring(stringIndex,1 );
            stringIndex++;  
            yield return new WaitForSeconds(timeToWrite/textToWrite.Length);
        }
    }
}
