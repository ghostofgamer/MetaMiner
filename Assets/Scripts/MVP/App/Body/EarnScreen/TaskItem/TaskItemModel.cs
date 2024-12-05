using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TaskItemModel
{
    public class TaskState
    {
        public TaskState(string icon, string caption, string reward, string link, bool completed)
        {
            Icon = icon;
            Caption = caption;
            Reward = reward;
            Completed = completed;
            Link = link;
        }

        public string Icon { get; private set; }
        public string Caption { get; private set; }
        public string Reward { get; private set; }
        public string Link { get; private set; }
        public bool Completed { get; private set; }
    }

    public ReactiveProperty<bool> Completed = new ReactiveProperty<bool>();
    public ReactiveProperty<string> Caption = new ReactiveProperty<string>();
    public ReactiveProperty<string> Reward = new ReactiveProperty<string>();
    public ReactiveProperty<string> Icon = new ReactiveProperty<string>();
    public ReactiveProperty<string> Link = new ReactiveProperty<string>();
}
