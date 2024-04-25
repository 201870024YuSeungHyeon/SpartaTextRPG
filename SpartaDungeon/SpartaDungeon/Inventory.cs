using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Inventory
    {
        private List<Item> items; //인벤토리에 보유한 아이템을 저장하는 리스트

        //생성자:
        public Inventory()
        {
            //인벤토리 객체 초기화. 내부적으로 아이템을 저장할 리스트 생성
            items = new List<Item>();
        }

        //아이템 조회
        public List<Item> GetItems()
        {
            //인벤토리에 있는 모든 아이템 반환. 다른 클래스에서 인벤토리에 접근하여 확인할 수 있음.
            return items;
        }

        public void AddItem(Item item)
        {
            //주어진 아이템을 인벤토리에 추가. 추가할 아이템을 파라미터로 전달받아 리스트에 추가.
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            //주어진 아이템을 인벤토리에서 제거함. 제거할 아이템을 파라미터로 전달받아 리스트에서 제거.
            items.Remove(item);
        }
    }
}
