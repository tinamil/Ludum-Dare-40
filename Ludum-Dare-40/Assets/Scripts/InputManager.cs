using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public enum InputEvent
    {
        Forward, Left, Right, Backward, Fire, OpenMenu
    }

    static Dictionary<InputEvent, KeyCode> mapping;
    static Dictionary<InputEvent, List<Action>> actions;

    static public void AddAction(InputEvent evt, Action act) {
        if (actions.ContainsKey(evt))
        {
            actions[evt].Add(act);
        } else
        {
            actions.Add(evt, new List<Action>() { act });
        }
    }

    static public void RemoveAction(InputEvent evt, Action act) {
        if (actions.ContainsKey(evt))
        {
            actions[evt].Remove(act);
        }
    }

    static public bool IsFiring(InputEvent evt) {
        return Input.GetKey(mapping[evt]);
    }

    private void Awake() {
        mapping = new Dictionary<InputEvent, KeyCode>();
        actions = new Dictionary<InputEvent, List<Action>>();
    }

    private void OnEnable() {
        mapping.Add(InputEvent.Forward, (KeyCode)PlayerPrefs.GetInt(InputEvent.Forward.ToString(), (int)KeyCode.W));
        mapping.Add(InputEvent.Left, (KeyCode)PlayerPrefs.GetInt(InputEvent.Left.ToString(), (int)KeyCode.A));
        mapping.Add(InputEvent.Right, (KeyCode)PlayerPrefs.GetInt(InputEvent.Right.ToString(), (int)KeyCode.D));
        mapping.Add(InputEvent.Backward, (KeyCode)PlayerPrefs.GetInt(InputEvent.Backward.ToString(), (int)KeyCode.S));
        mapping.Add(InputEvent.Fire, (KeyCode)PlayerPrefs.GetInt(InputEvent.Backward.ToString(), (int)KeyCode.Space));
        mapping.Add(InputEvent.OpenMenu, (KeyCode)PlayerPrefs.GetInt(InputEvent.Backward.ToString(), (int)KeyCode.Escape));
    }

    // Update is called once per frame
    void Update() {
        foreach (var k in mapping)
        {
            if (Input.GetKeyDown(k.Value))
            {
                if (actions.ContainsKey(k.Key))
                {
                    actions[k.Key].ForEach(act => act.Invoke());
                }
            }
        }
    }
}
