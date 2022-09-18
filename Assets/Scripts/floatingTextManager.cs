using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class floatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();


    private void Start()
    {
        DontDestroyOnLoad (gameObject);
    }

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingtext = GetFloatingText();

        floatingtext.txt.text = msg;
        floatingtext.txt.fontSize = fontSize;
        floatingtext.txt.color = color;
        floatingtext.go.transform.position = Camera.main.WorldToScreenPoint(position); 
        floatingtext.motion = motion;
        floatingtext.duration = duration;

        floatingtext.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<TextMeshProUGUI>();
            
            floatingTexts.Add(txt);
        }

        return txt;
    }
}
