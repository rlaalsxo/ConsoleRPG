﻿using System.Runtime.CompilerServices;

namespace ConsoleRPG
{
    internal class Program
    {
        private static Character player;
        static List<Item> items = new List<Item>();
        string gap = "";
        static void Main(string[] args)
        {
            items.Add(new Weapon(0));
            items.Add(new Armor(0));
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 게임종료");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.Write($"공격력 :{player.Atk}");
            foreach(Item item in items)
            {
                if (item.Equip)
                {
                    Console.Write($" +({item.Atk})");
                }
            }
            Console.Write($"\n방어력 : {player.Def}");
            foreach (Item item in items)
            {
                if (item.Equip)
                {
                    Console.Write($" +({item.Def})");
                }
            }
            Console.Write($"\n체력 : {player.Hp}");
            foreach (Item item in items)
            {
                if (item.Equip)
                {
                    Console.Write($" +({item.Hp})");
                }
            }
            Console.WriteLine($"\nGold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine("이름\t\t공격력\t방어력\t체력\t설명");
            if(items.Count > 0)
            {
                foreach (Item item in items)
                {
                    string status = item.Name + "\t" + item.Atk.ToString() + "\t" + item.Def.ToString() + "\t" + item.Hp.ToString() + "\t" + item.Explanation;
                    if (item.Equip)
                    {
                        string eqStatus = status.Insert(0, "[E]");
                        Console.Write(eqStatus);
                    }
                    else
                    {
                        Console.Write(status);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayItemEquip();
                    break;
            }
        }
        static void DisplayItemEquip()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유중인 아이템을 장착할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine("이름\t\t공격력\t방어력\t체력\t설명");
            if(items.Count> 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string status = i.ToString() + ". " + items[i].Name + "\t" + items[i].Atk.ToString() + "\t" + items[i].Def.ToString() + "\t" + items[i].Hp.ToString() + "\t" + items[i].Explanation;
                    if (items[i].Equip)
                    {
                        string eqStatus = status.Insert(0, "[E]");
                        Console.Write(eqStatus);
                    }
                    else
                    {
                        Console.Write(status);
                    }
                    Console.WriteLine();
                }
                int key = CheckValidInput(0, items.Count - 1);
                if (items[key].Equip)
                {
                    Console.WriteLine("장비를 해제했습니다");
                    items[key].Equip = false;
                }
                else
                {
                    items[key].Equip = true;
                    Console.WriteLine("장착이 완료되었습니다");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 재장착");
            Console.WriteLine("0. 나가기");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case 1:
                    DisplayItemEquip();
                    break;
            }
        }
        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Explanation { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Price { get; set; }
        public bool Equip { get; set; }
    }
    public class Weapon : Item
    {
        static string[] name = { "녹슨 검", "기본 검", "좋은 검", "더 좋은 검" };
        int namePad = name.Max(name => name.Length);
        static string[] explanation = { "녹이 슨 검입니다", "기본 검입니다", "좋은 검입니다", "좋은 검보다 더 좋은 검입니다" };
        int exPad = explanation.Max(ex => ex.Length);
        public Weapon(int index)
        {
            this.Index = index;
            this.Name = name[index].PadRight(namePad);
            this.Explanation = explanation[index].PadRight(exPad);
            this.Atk = index + 5;
            this.Def = 0;
            this.Hp = 0;
            this.Price = this.Atk * 200;
            this.Equip = false;
        }
    }
    public class Armor : Item
    {
        static string[] name = { "녹슨 갑옷", "기본 갑옷", "좋은 갑옷", "더 좋은 갑옷" };
        int namePad = name.Max(name => name.Length);
        static string[] explanation = { "녹이 슨 갑옷입니다", "기본 갑옷입니다", "좋은 갑옷입니다", "좋은 갑옷보다 더 좋은 갑옷입니다" };
        int exPad = explanation.Max(ex => ex.Length);
        public Armor(int index)
        {
            this.Index = index;
            this.Name = name[index].PadRight(namePad);
            this.Explanation = explanation[index].PadRight(exPad);
            this.Atk = 0;
            this.Def = index * 2 + 10;
            this.Hp = index * 5 + 20;
            this.Price = index * 600;
            this.Equip = false;
        }
    }
}