using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class prueba : MonoBehaviour
{
    Slider sl;
    [SerializeField]List<UnityEvent> events;
    public bool sumando ;
    void Start()
    {
        sl = GetComponent<Slider>();
        sl.maxValue = events.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            sumando = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            sumando = false;
            DoAction(sl.value);
            sl.value = 0;
        }
        if(sumando)
        {
            sl.value += Time.deltaTime;
        }
    }
    void DoAction(float _value)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if(_value<i)
            {
                events[i].Invoke();
                break;
            }
        }
    }
    public void Do(int _a)
    {
        print(_a);
    }
}
