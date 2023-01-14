using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager
{
    List<List<Tile>> _TileArray;
    public List<List<Tile>> TileArray => _TileArray;

    // 게임에 펼쳐져있는 필드의 스크립트
    Field _GameField;
    public Field GameField => _GameField;

    // 필드의 최대 넓이
    const int MaxFieldX = 8;
    const int MaxFieldY = 3;

    
    // 필드의 생성을 위한 필드의 위치
    public Vector3 FieldPosition => new Vector3(0, -1.4f, 0);

    // 필드 생성
    public void FieldSet(Field gameField, GameObject TilePrefabs)
    {
        _GameField = gameField;
        Transform trans = gameField.transform;

        _TileArray = new List<List<Tile>>();

        Vector3 vec = trans.position;

        float disX = trans.localScale.x / MaxFieldX;
        float disY = trans.localScale.y / MaxFieldY;

        for (int i = -1; i < 2; i++)
        {
            _TileArray.Add(new List<Tile>());

            for (int j = -4; j < 4; j++)
            {
                float x = (disX * j) + (disX * 0.5f);
                float y = disY * i;

                GameObject tile = GameObject.Instantiate(TilePrefabs, trans);
                tile.transform.position = new Vector3(vec.x + x, vec.y + y);

                _TileArray[i+1].Add(tile.GetComponent<Tile>());
            }
        }
    }

    // 공격범위가 타일을 넘어섰는지 확인
    public BattleUnit RangeCheck(BattleUnit caster, int x, int y)
    {
        if (0 <= x && x < 8)
        {
            if (0 <= y && y < 3)
            {
                TileArray[y][x].SetColor(Color.red);

                if (TileArray[y][x].isOnTile)
                {
                    if (caster.BattleUnitSO.team != TileArray[y][x].TileUnit.BattleUnitSO.team)
                    {
                        // 넘지 않았다면 타일 위에 있는 유닛을 반환
                        return TileArray[y][x].TileUnit;
                    }
                }
            }
        }
        return null;
    }

    // 지정한 위치에 있는 타일의 좌표를 반환
    public Vector3 GetTileLocate(int x, int y)
    {
        try
        {
            Vector3 vec = _TileArray[y][x].transform.position;

            float sizeX = _TileArray[y][x].transform.localScale.x * 0.5f;
            float sizeY = _TileArray[y][x].transform.localScale.y * 0.5f;
            sizeY += _TileArray[y][x].transform.localScale.y * 0.5f; // 스프라이트 변경으로 인한 임시조치

            vec.x += sizeX;
            vec.y += sizeY;

            return vec;
        }
        catch
        {
            return new Vector3(-1, -1, -1);
        }
    }

    public void FieldClear()
    {
        CanSelectClear();
        FieldColorClear();
    }

    void CanSelectClear()
    {
        foreach(List<Tile> list in TileArray)
        { 
            foreach(Tile tile in list)
            {
                tile.SetCanSelect(false);
            }
        }
    }

    void FieldColorClear()
    {
        foreach(List<Tile> tiles in TileArray)
        {
            foreach(Tile tile in tiles)
            {
                tile.SetColor(Color.white);
            }
        }
    }


    public void EnterTile(BattleUnit ch, int x, int y)
    {
        _TileArray[y][x].EnterTile(ch);
    }

    public void ExitTile(int x, int y)
    {
        _TileArray[y][x].ExitTile();
    }

    public bool GetIsOnTile(int x, int y) => _TileArray[y][x].isOnTile;
    
}
