using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUnit
{
    public string name { get; }
    public int DarkEssence { get; set; }
    public Sprite image { get; }

    public RewardUnit(string name, int DarkEssence,Sprite image)
    {
        this.name = name;
        this.DarkEssence = DarkEssence;
        this.image = image;
    }
} 
public class RewardController
{
    #region 변수
    Dictionary<int, RewardUnit> dic_units;
    int prev_DarkEssence;
    #endregion
    #region 함수
    public void Init(List<DeckUnit> units, int DarkEssence)
    {
        this.dic_units = new Dictionary<int, RewardUnit>();
        this.prev_DarkEssence = DarkEssence;

        
        foreach (DeckUnit unit in units)
        {
            dic_units.Add(unit.UnitID, new RewardUnit(unit.Data.Name, unit.DeckUnitStat.FallCurrentCount, unit.Data.CorruptPortraitImage));
        }
    }
    public void RewardSetting(List<DeckUnit> units, UI_RewardScene rewardScene)//리워드씬에서 값 세팅해주기
    {
        rewardScene.Init(units.Count,GameManager.Data.DarkEssense - prev_DarkEssence);
        RewardUnit unit,fallunit;
        for (int i=0;i<units.Count;++i)
        {
            if(dic_units.TryGetValue(units[i].UnitID,out unit))//기존에 있던 친구들
            {
                unit.DarkEssence -= units[i].DeckUnitStat.FallCurrentCount;
                rewardScene.setContent(i, unit);
                dic_units.Remove(units[i].UnitID);
            }
            else//새로 플레이어 덱에 들어온 친구들
            {
                
                fallunit = new RewardUnit(units[i].Data.Name, units[i].Data.RawStat.FallMaxCount-units[i].Data.RawStat.FallCurrentCount
                    -units[i].DeckUnitStat.FallCurrentCount, units[i].Data.CorruptPortraitImage);
                rewardScene.setContent(i, fallunit,true);
            }
        }
        foreach(var u in dic_units)//죽은 친구들
        {
            unit = u.Value;
            Debug.Log("죽은 친구들 : " + unit.name);
        }
        dic_units.Clear();
    }
   
    #endregion
}
