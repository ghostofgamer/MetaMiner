using System;
using UnityEngine;

[Serializable]
public class DAOState
{
    public DAOState(string id, string name, string communityChatLink, string peopleCount, string summeryBalance, string percentSummeryBalance)
    {
        Id = id;
        Name = name;
        CommunityChatLink = communityChatLink;
        PeopleCount = peopleCount;
        SummaryBalance = summeryBalance;
        PercentSummaryBalance = percentSummeryBalance;
    }

    [field: SerializeField]
    public string Id { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public string CommunityChatLink { get; private set; }

    [field: SerializeField]
    public string PeopleCount { get; private set; }

    [field: SerializeField]
    public string SummaryBalance { get; private set; }

    [field: SerializeField]
    public string PercentSummaryBalance { get; private set; }
}