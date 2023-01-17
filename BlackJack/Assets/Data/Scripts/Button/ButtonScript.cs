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

    [SerializeField]
    private bool reachMaxValue;

    private void Start()
    {
        
        slider.maxValue = events.Count;
        slider.gameObject.SetActive(false);

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
            if (slider.value <= slider.maxValue && !reachMaxValue)
            {
                slider.value += Time.deltaTime *slider.maxValue;
                
            }
            else
            {
                slider.value -= Time.deltaTime * slider.maxValue;
                if (slider.value == slider.minValue)
                {
                    reachMaxValue = false;
                }
            }

            slider.gameObject.SetActive(true);
        }

        if (slider.value == slider.maxValue)
        {
            reachMaxValue = true;
        }
        
        if (!Clicked && slider.value != 0)
        {
            StartCoroutine(eventWork());
        }
   
    }

    IEnumerator eventWork()
    {

        int eventWhoWork = (int)Mathf.Round(slider.value / slider.maxValue);
        //print(eventWhoWork);
        CountEvent(eventWhoWork);
        slider.value = 0;
        slider.gameObject.SetActive(false);
        yield break;
    }

    void CountEvent(float value)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (value == i)
            {
                events[i].Invoke();
                break;
            }
        }
    } 


    public void DoA()
    {
        print("a");
    } 
    public void DoB()
    {
        print("b");
    } 
    
    
}
