using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            //'Player', 'Shop', 'Inventory' 객체 생성
            Player player = new Player("GigaChad", "전사", 1, 10, 5, 100, 15000);
            Shop shop = new Shop();
            Inventory inventory = new Inventory(); 
            
            //플레이어가 게임을 종료할 때까지 루프
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("0. 게임 종료");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": //1 입력 시 'ShowStatus()' 메서드 호출
                        ShowStatus(player, shop,inventory);
                        break;
                    case "2": //2 입력 시 'ShowInventory()' 메서드 호출
                        ShowInventory(player, shop, inventory);
                        break;
                    case "3": //3 입력 시 'VisitShop()' 메서드 호출
                        VisitShop(player, shop, inventory);
                        break;
                    case "0": //0 입력시 게임 종료
                        isRunning = false;
                        break;
                    default: //0,1,2,3이 아니면
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        //Console.WriteLine("아무 키나 눌러 뒤로 돌아가기");
                        //Console.ReadKey();
                        //await Task.Delay(3000);
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        static void ShowStatus(Player player, Shop shop, Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("** 상태 보기 **");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Class} )"); //레벨, 이름, 직업 표시

            int equippedAttack = player.GetEquippedAttack(); //장비 장착 시 총 공격력
            int increasedAttack = equippedAttack - player.Attack; //장비 장착으로 얻게 된 공격력
        

            int equippedDefense= player.GetEquippedDefense();  //장비 장착 시 총 방어력
            int increasedDefense = equippedDefense - player.Defense; // 장비 장착으로 얻게 된 방어력
         

            Console.WriteLine($"공격력 : {equippedAttack} (+{increasedAttack})");
            Console.WriteLine($"방어력 : {equippedDefense} (+{increasedDefense})");
            Console.WriteLine($"체  력 : {player.Health}");
            Console.WriteLine($"GOLD  : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 인벤토리");
            Console.WriteLine("2. 상점");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요: ");
            
            string input = Console.ReadLine();
            
            switch(input)
            {
                case "0":
                    return;
                case "1": ShowInventory(player, shop,inventory); break;
                
                case "2": VisitShop(player, shop, inventory); break;
                
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    //Console.WriteLine("아무 키나 눌러 뒤로 돌아가기");
                    //Console.ReadKey();
                    //await Task.Delay(3000);
                    Thread.Sleep(1000);
                    ShowStatus(player, shop, inventory);
                    break;

            }
        }

        static void ShowInventory(Player player, Shop shop,Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("** 인벤토리 **");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            
            List<Item> items = inventory.GetItems(); //인벤토리 객체에서 아이템 목록 가져오기
            //만약 인벤토리에 아이템이 없다면
            if (items.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
            }
            else //인벤토리에 아이템이 있다면
            {
                for (int i = 0; i < items.Count; i++)
                {
                    //아이템이 장착되어 있으면 "[E]"를, 아니면 빈 문자열을 'equipped'에 저장
                    string equipped = items[i].Equipped ? "[E] " : ""; 
                    //아이템의 번호, 이름, 설명 출력 장착 여부에 따라 "[E]" 출력
                    Console.WriteLine($"{i + 1}. {equipped}{items[i].Name} | {items[i].Description}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요: ");
            string input = Console.ReadLine();
            
            switch(input)
            {
                case "1": //1 입력시 장비 관리 메서드 호출
                    EquipmentsManage(player, shop, inventory); break;
                case "0":
                    //나가기
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    //Console.WriteLine("아무 키나 눌러 뒤로 돌아가기");
                    //Console.ReadKey();
                    //await Task.Delay(3000);
                    Thread.Sleep(1000);
                    ShowInventory(player, shop, inventory);
                    break;
            }
        }

        static void EquipmentsManage(Player player, Shop shop,Inventory inventory) 
        {
            Console.Clear();
            Console.WriteLine("** 인벤토리 - 장착 관리 **");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            List<Item> items = inventory.GetItems();//인벤토리 객체에서 아이템 목록 가져오기
            
            for (int i = 0; i < items.Count; i++)
            {
                //아이템이 장착되어 있다면 "[E]"를, 아니면 빈 문자열을 equipped에 저장
                string equipped = items[i].Equipped ? "[E] " : "";
                Console.WriteLine($"{i + 1}. {equipped}{items[i].Name} | {items[i].Description}");
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 아이템을 선택해주세요: ");
            
            int selectedIndex; //플레이어가 선택한 아이템의 인덱스
            
            //입력한 값을 정수로 저장하고, 범위 안에 있는 지 확인
            if (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 0 || selectedIndex > items.Count)
            {

                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(2000);
                return;
            }

            // 선택한 아이템의 인덱스 조정
            selectedIndex--;

            //플레이어가 나가기를 선택하면
            if (selectedIndex == -1)
            {
                // 나가기
                return;
            }
            //플레이어가 유효한 아이템을 선택한 경우
            else if (selectedIndex >= 0 && selectedIndex < items.Count)
            {
                // 아이템이 장착되어 있는지 확인
                Item selected = items[selectedIndex];
                if (selected.Equipped)
                {
                    // 장착 해제
                    player.UnequipItem(selected);
                    selected.Equipped = false;
                    Console.WriteLine($"{selected.Name}을(를) 장착 해제했습니다.");
                    Thread.Sleep(2000);
                }
                else
                {
                    // 장착
                    player.EquipItem(selected);
                    selected.Equipped = true;
                    Console.WriteLine($"{selected.Name}을(를) 장착했습니다.");
                    Thread.Sleep(2000);
                }
            }

            // 상태 보기 메뉴에서 플레이어의 상태가 업데이트되어야 함
            ShowStatus(player, shop ,inventory);
        }

        static void VisitShop(Player player, Shop shop, Inventory inventory)
        {
            bool exitShop = false;
            //상점에서 나갈 때까지 반복
            while (!exitShop)
            {
                Console.Clear();
                Console.WriteLine("** 상점 **");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine($"[보유 골드] {player.Gold} G");
                Console.WriteLine();

                List<Item> items = shop.GetAvailableItems(player); //플레이어가 구매할 수 있는 목록 가져오기
                //구매 가능한 아이템이 없다면
                if (items.Count == 0)
                {
                    Console.WriteLine("구매 가능한 아이템이 없습니다.");
                    Thread.Sleep(2000); exitShop = true;
                }
                //구매 가능한 아이템이 있다면
                else
                {
                    foreach (var item in items)//구매 가능한 아이템에 대해 반복
                    {
                        //아이템이 이미 구매되었다면 "구매완료", 아니면 가격을 표시
                        string purchased = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                        Console.WriteLine($"- {item.Name} | {item.Description} | {purchased}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PurchaseItem(player, shop, inventory);
                        break;
                    case "2":
                        
                        SellItem(player, shop, inventory);
                        exitShop = true;
                        break;
                    case "0":
                        //나가기
                        exitShop = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;

                }
            }
        }

        static void PurchaseItem(Player player , Shop shop, Inventory inventory)
        {
                Console.Clear();
                Console.WriteLine("** 상점 - 아이템 구매 **");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();

            //구매할 수 있는 아이템 목록 가져오기
            List<Item> availableItems = shop.GetAvailableItems(player); 
            
            //구매 가능한 아이템이 없는 경우
                if (availableItems.Count == 0)
                {
                    Console.WriteLine("구매 가능한 아이템이 없습니다.");
                Thread.Sleep(2000);
                    return;
                }
                //구매 가능한 모든 아이템에 대해 반복
                for (int i = 0; i < availableItems.Count; i++)
                {
                //아이템이 이미 구매되었다면 "구매 완료"를, 아니면 가격을 문자열로 표시
                    string purchased = availableItems[i].IsPurchased ? "구매완료" : $"{availableItems[i].Price} G";
                    Console.WriteLine($"{i + 1}. {availableItems[i].Name} | {availableItems[i].Description} | {purchased}");
                }
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 아이템을 선택해주세요: ");
            
               int selectedIndex; //사용자가 선택한 아이템의 인덱스
                
                //입력한 값을 정수로 변환하고, 범위 안에 있는지 확인
                if (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 0 || selectedIndex > availableItems.Count)
                {
                Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
                    return;
                }

                // 선택한 아이템의 인덱스 조정
                selectedIndex--;

                if (selectedIndex == -1)
                {
                    return;
                }
                else if (selectedIndex >= 0 && selectedIndex < availableItems.Count)
                {
                //사용자가 선택한 아이템 가져오기
                    Item selected = availableItems[selectedIndex];
                //구매 성공 여부
                    bool purchased = shop.PurchaseItem(player, selected);
                    if (purchased) //구매했다면
                    { 
                       //인벤토리에 아이템 추가
                        inventory.AddItem(selected);
                    //구매 할 수 있는 아이템 목록에서 제거
                        availableItems.Remove(selected);
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족하거나 이미 구매한 아이템입니다.");
                    Thread.Sleep(2000);
                    PurchaseItem(player,shop,inventory);
                    }

                }
            
        }

        static void SellItem(Player player, Shop shop, Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("** 상점 - 아이템 판매 **");
            Console.WriteLine("보유 중인 아이템을 판매합니다.");
            Console.WriteLine();

            List<Item> playerItems = inventory.GetItems();

            if(playerItems.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
                Thread.Sleep(2000);
                return;
            }

            for(int i = 0; i< playerItems.Count; i++) 
            {
                Console.WriteLine($"{i+1}. {playerItems[i].Name} | {playerItems[i].Price * 0.85} G"); //판매 가격 = 아이템 가격 85%
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("판매할 아이템을 선택해주세요: ");

            int selectedIndex;

            if (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 0 || selectedIndex > playerItems.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(2000);
                return;
            }

            // 선택한 아이템의 인덱스 조정
            selectedIndex--;

            if (selectedIndex == -1)
            {
                return;
            }
            else if (selectedIndex >= 0 && selectedIndex < playerItems.Count)
            {
                Item selected = playerItems[selectedIndex];
                shop.SellItem(player, selected);
                inventory.RemoveItem(selected);
                Console.WriteLine($"{selected.Name}을(를) 판매했습니다.");
                Thread.Sleep(1000);
                SellItem(player,shop,inventory);
            }
        }
    }

}
