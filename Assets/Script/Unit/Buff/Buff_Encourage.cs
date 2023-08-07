using UnityEngine;

public class Buff_Encourage : Buff
{
    private int attackUp;
    public override void Init(BattleUnit caster, BattleUnit owner)
    {
        _buffEnum = BuffEnum.Encourage;

        _name = "고양";

        _description = "공격력이 50% 증가합니다.";

        _count = 1;

        _countDownTiming = ActiveTiming.DAMAGE_CONFIRM;

        _buffActiveTiming = ActiveTiming.NONE;

        _caster = caster;

        _owner = owner;

        _statBuff = true;

        _dispellable = false;

        _stigmaBuff = false;

        attackUp = owner.DeckUnit.DeckUnitTotalStat.ATK / 2;
    }

    public override void Stack()
    {
        _count += 1;
    }

    public override Stat GetBuffedStat()
    {
        Stat stat = new();  
        stat.ATK += attackUp;

        return stat;
    }
}