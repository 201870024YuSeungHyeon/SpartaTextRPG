using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Player
    {
        public string Name; //{ get; } //플레이어 이름
        public string Class; //{ get; } //플레이어 직업
        public int Level; //{ get; } //레벨
        public int Attack; //{ get; } // 공격력
        public int Defense; //{ get; } // 방어력
        public int Health;//{ get; } // 체력
        public int Gold; //{ get; private set; } // 보유한 골드

        private List<Item> equippedItems; //장착한 아이템 리스트

        //생성자 :
        public Player(string name, string playerClass, int level, int attack, int defense, int health, int gold)
        {
            Name = name;
            Class = playerClass;
            Level = level;
            Attack = attack;
            Defense = defense;
            Health = health;
            Gold = gold;
            equippedItems = new List<Item>();
        }

        //장착한 아이템을 포함한 총 공격력 계산
        public int GetEquippedAttack()
        {
            int total = Attack;
            foreach (var item in equippedItems)
            {
                total += item.AttackBonus;
            }
            return total;
        }
        //장착한 아이템을 포함한 총 방어력 계산
        public int GetEquippedDefense()
        {
            int total = Defense;
            foreach (var item in equippedItems)
            {
                total += item.DefenseBonus;
            }
            return total;
        }
        //플레이어에게 아이템 장착
        public void EquipItem(Item item)
        {
            equippedItems.Add(item);
        }
        //장착 해제
        public void UnequipItem(Item item)
        {
            equippedItems.Remove(item);
        }
        //골드 조정. 지정된 양 만큼 증감
        public void AdjustGold(int amount)
        {
            Gold += amount;
        }
       
    }
}
