using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Shop
    {
        private List<Item> availableItems;

        public Shop()
        {
            availableItems = new List<Item>
            {
                new Item("수련자 갑옷", "방어력 +5 | 수련에 도움을 주는 갑옷입니다.", 1000, 0, 5),
                new Item("낡은 검", "공격력 +2 | 쉽게 볼 수 있는 낡은 검입니다.", 600, 2, 0),
                new Item("청동 도끼", "공격력 +5 | 어디선가 사용됐던거 같은 도끼입니다.", 1500, 5, 0),
                new Item("스파르타의 갑옷", "방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 0, 15),
                new Item("스파르타의 창", "공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, 7, 0)
            };

        }

        public List<Item> GetAvailableItems(Player player)
        {
            List<Item> result = new List<Item>();

            foreach (var item in availableItems)
            {
                if (!item.IsPurchased || player.Gold >= item.Price)
                {
                    result.Add(item);
                }

            }
            return result;
        }

        public bool PurchaseItem(Player player, Item item)
        {
            if (player.Gold >= item.Price && !item.IsPurchased)
            {
                player.AdjustGold(-item.Price); // 아이템 가격만큼 재화를 차감
                item.IsPurchased = true; // 아이템 구매 상태 변경

                return true;

            }
            return false;
        }

        public void SellItem(Player player, Item item)
        {
            int sellPrice = (int)(item.Price * 0.85); //판매 가격은 구매 가격의 85%
            player.AdjustGold(sellPrice);
            item.IsPurchased = false;

            //장착 중인 상태이라면 장착 해제
            if(item.Equipped)
            {
                player.UnequipItem(item);
                item.Equipped = false;
            }
        }
    }
}
