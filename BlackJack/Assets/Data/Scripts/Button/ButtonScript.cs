using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    private Slider slider;

    [SerializeField]
    List<UnityEvent> events;

    [SerializeField]
    private bool Clicked;

    private void Start()
    {
        slider.maxValue = events.Count;
    }
    public void isButtonClicked()
    {
        Clicked = true;
    }

    public void isNotClicked()
    {
        Clicked = false;

    }


    private void Update()
    {
        
        if (Clicked)
        {
            slider.value += Time.deltaTime;
        }

        if (!Clicked && slider.value >= 0)
        {
            StartCoroutine(eventWorks());
        }
        
    }

    IEnumerator eventWorks()
    {
        CountEvent((int)slider.value);
        yield return new WaitForSeconds(1);
        slider.value = 0;
    }

    void CountEvent(int value)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (value < i)
            {
                events[i].Invoke();
                break;
            }
        }
    } 

}
