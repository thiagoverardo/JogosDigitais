using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulha: State
{
    SteerableBehaviour steerable;

    public override void Awake()
    {
        base.Awake();

        // Criamos e populamos uma nova transição
        Transition ToAtacando = new Transition();
        ToAtacando.condition = new ConditionDistLT(transform,GameObject.FindWithTag("Player").transform,20.0f);
        ToAtacando.target = GetComponent<StateAtaque>();
        // Adicionamos a transição em nossa lista de transições
        transitions.Add(ToAtacando);

        steerable = GetComponent<SteerableBehaviour>();
    }

    float angle = 0;
    public override void Update()
    {
        angle += 0.1f * Time.deltaTime;
        Mathf.Clamp(angle, 0.0f, 2.0f * Mathf.PI);
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);

        steerable.Thrust(y, y);
    }
}