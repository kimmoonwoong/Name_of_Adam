using UnityEngine;

public class Buff_Stigma_Martyrdom : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Martyrdom;

        _name = "����";

        _description = "����.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.BEFORE_ATTACK;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        if (_owner.BattleUnitTotalStat.FallMaxCount / 2 >= _owner.Fall.GetCurrentFallCount())
            caster.ChangeFall(1);

        return false;
    }
}