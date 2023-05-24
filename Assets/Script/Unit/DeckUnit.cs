using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeckUnit
{
    public UnitDataSO Data; // 유닛 기초 정보
    
    [SerializeField] public Stat ChangedStat; // 영구 변화 수치
    public Stat Stat => Data.RawStat + ChangedStat; // Memo : 나중에 낙인, 버프 추가한 스탯으로 수정
    
    [SerializeField] public List<Passive> Stigma = new List<Passive>();

    private int _maxStigmaCount = 3;

    public void SetStigma()
    {
        foreach (Passive stigma in Data.IngerenceStigma)
            AddStigma(stigma);

        foreach (Passive stigma in Stigma)
            AddStigma(stigma);
    }

    public void AddStigma(Passive passive)
    {
        if (Stigma.Contains(passive))
        {
            Debug.Log($"이미 장착된 낙인입니다. : {passive.Name}");
            return;
        }

        if(Stigma.Count >= _maxStigmaCount)
        {
            Debug.Log("최대 낙인 개수");
            return;
        }

        Stigma.Add(passive);
    }

    public Type RemoveRandomStigma()
    {
        if(Stigma.Count <= 0)
        {
            Debug.Log("삭제할 낙인이 없습니다.");
            return null;
        }

        int num = UnityEngine.Random.Range(0, Stigma.Count);
        Type removed = Stigma[num].GetType(); // 지워질 패시브의 정보
        Stigma.RemoveAt(num);
        return removed;
    }
}