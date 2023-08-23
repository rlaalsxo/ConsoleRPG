namespace ConsoleRPG
{
    internal class Program
    {
        private static Character player;
        static List<Item> eqItem= new List<Item>();
        static List<Item> weaponShop = new List<Item>();
        static List<Item> armorShop = new List<Item>();
        static List<Item> items = new List<Item>();
        static Dungeon[] dungeons = new Dungeon[5];
        static void Main(string[] args)
        {
            for(int i = 0; i < 4; i++)
            {
                weaponShop.Add(new Weapon(i));
                armorShop.Add(new Armor(i));
            }
            for(int i = 0; i < dungeons.Length; i++)
            {
                dungeons[i].ThisDungeon(i);
            }
            items.Add(new Weapon(0));
            items.Add(new Armor(0));
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 5000);

            // 아이템 정보 세팅
        }

        static void DisplayGameIntro()
        {
            Console.Clear();
            TextColor("스파르타 마을에 오신 여러분 환영합니다.\n", ConsoleColor.Magenta);
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전");
            Console.WriteLine("5. 게임종료");
            Console.WriteLine();
            TextColor("원하시는 행동을 입력해주세요.\n", ConsoleColor.Red);

            int input = CheckValidInput(1, 5);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
                case 4:
                    DisplayDungeon();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();
            TextColor("상태보기\n", ConsoleColor.Green);
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
            Console.WriteLine();
            MyGold();
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
            TextColor("인벤토리\n", ConsoleColor.Green);
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]\n");
            ItemWrite();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("2. 강화");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayItemEquip();
                    break;
                case 2:
                    DisplayEnforce();
                    break;
            }
        }
        static void DisplayItemEquip()
        {
            Console.Clear();
            TextColor("인벤토리 - 장착관리\n", ConsoleColor.Green);
            Console.WriteLine("보유중인 아이템을 장착할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]\n");
            ItemWrite();
            Console.WriteLine("\n0. 나가기");
            while (true)
            {
                int key = CheckValidInput(0, items.Count);
                if (key == 0)
                {
                    DisplayInventory();
                    break;
                }
                else if (items[key - 1].Equip)
                {
                    Item xItem = null;
                    if (eqItem.Count >= 0)
                    {
                        foreach (Item _item in eqItem)
                        {
                            if (items[key - 1] == _item)
                            {
                                xItem = _item;
                                TextColor("장비를 해제하였습니다\n", ConsoleColor.DarkCyan);
                                items[key - 1].Equip = false;
                            }
                        }
                        eqItem.Remove(xItem);
                    }
                    else
                    {
                        Console.WriteLine("알수없는 오류 발생");
                    }
                }
                else if (!items[key - 1].Equip)
                {
                    if (eqItem.Count >= 0 && eqItem.Count <= 6)
                    {
                        eqItem.Add(items[key - 1]);
                        items[key - 1].Equip = true;
                        TextColor("장착을 완료하였습니다\n", ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        Console.WriteLine("장비를 장착할 수 없습니다");
                    }
                }
                Thread.Sleep(700);
                DisplayItemEquip();
            }
        }
        static void DisplayShop()
        {
            Console.Clear();
            TextColor("상점\n", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 상점");
            Console.WriteLine("2. 방어구 상점");
            Console.WriteLine("3. 판매 상점");
            int key = CheckValidInput(0, 3);
            if(key == 0)
            {
                DisplayGameIntro();
            }
            else if (key == 3)
            {
                DisplaySellShop();
            }
            else
            {
                ShopDisplay(key);
            }
        }
        static void ShopDisplay(int key)
        {
            List<Item> Shop = null;
            if (key == 1)
            {
                Shop = weaponShop;
                ShopWrite("무기", Shop);
            }
            else if(key == 2)
            {
                Shop = armorShop;
                ShopWrite("방어구", Shop);
            }
            MyGold();
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            while (true)
            {
                int buyItem = CheckValidInput(0, 4);
                switch(buyItem)
                {
                    case 0:
                        DisplayShop(); break;
                    default:
                        if(Shop.Count > 0)
                        {
                            if (Shop[buyItem -1] != null)
                            {
                                if (Shop[buyItem - 1].Price <= player.Gold)
                                {
                                    player.Gold -= Shop[buyItem - 1].Price;
                                    TextColor("구매에 성공했습니다\n", ConsoleColor.Red);
                                    items.Add(Shop[buyItem - 1]);
                                }
                                else
                                {
                                    TextColor("돈이 부족합니다\n", ConsoleColor.Red);
                                }
                                MyGold();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다");
                                break;
                            }
                        }
                        break;
                }
            }
        }
        static void DisplaySellShop()
        {
            ShopWrite("판매", items);
            TextColor("\n판매시 가격의 절반의 금액만 얻을 수 있습니다\n", ConsoleColor.DarkBlue);
            MyGold();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (true)
            {
                int sellItem = CheckValidInput(0, items.Count);
                switch (sellItem)
                {
                    case 0:
                        DisplayShop(); break;
                    default:
                        if (items.Count > 0)
                        {
                            if (items[sellItem - 1] != null)
                            {
                                if (items[sellItem - 1].Equip)
                                {
                                    TextColor("장착중인 아이템은 판매할 수 없습니다\n", ConsoleColor.Red);
                                }
                                else
                                {
                                    player.Gold += (items[sellItem - 1].Price / 2);
                                    TextColor("판매에 성공했습니다\n", ConsoleColor.Red);
                                    items.Remove(items[sellItem - 1]);
                                }
                                Thread.Sleep(500);
                                Console.Clear();
                                DisplaySellShop();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다");
                                break;
                            }
                        }
                        break;
                }
            }
        }
        static void DisplayEnforce()
        {
            Console.Clear();
            TextColor("강화\n", ConsoleColor.Green);
            Console.WriteLine();
            ItemWrite();
            Console.WriteLine();
            MyGold();
            Console.WriteLine("\n0. 나가기");
            while (true)
            {
                int price = 0;
                int efItem = CheckValidInput(0, items.Count);
                switch (efItem)
                {
                    case 0:
                        DisplayInventory(); break;
                    default:
                        if(items.Count > 0)
                        {
                            foreach(Item item in items)
                            {
                                if(item == items[efItem - 1])
                                {
                                    price = item.Level * 100 + 200;
                                    if (player.Gold >= price)
                                    {
                                        Console.Clear();
                                        DrawWalls(0);
                                        Console.SetCursorPosition(32, 8);
                                        TextColor("강화에 성공했습니다\n", ConsoleColor.Cyan);
                                        player.Gold -= price;
                                        Console.SetCursorPosition(32, 10);
                                        MyGold();
                                        item.Level++;
                                        item.ItemEnforce();
                                    }
                                    else
                                    {
                                        TextColor("돈이 부족합니다", ConsoleColor.Red);
                                    }
                                    Thread.Sleep(1000);
                                    DisplayEnforce();
                                }
                            }
                        }
                        break;
                }
            }
        }
        static void DisplayDungeon()
        {
            Console.Clear();
            TextColor("던전\n\n", ConsoleColor.Green);
            TextColor("입장하실 던전을 선택해 주세요\n", ConsoleColor.Blue);
            TextColor("\n1.초급 던전\n", ConsoleColor.Green);
            TextDungeonStatus(0);
            TextColor("\n2.중급 던전\n", ConsoleColor.DarkGreen);
            TextDungeonStatus(1);
            TextColor("\n3.상급 던전\n", ConsoleColor.Cyan);
            TextDungeonStatus(2);
            TextColor("\n4.최상급 던전\n", ConsoleColor.DarkCyan);
            TextDungeonStatus(3);
            TextColor("\n5.보스 던전\n", ConsoleColor.Red);
            TextDungeonStatus(4);
            Console.WriteLine("\n0. 나가기");
            int key = CheckValidInput(0, dungeons.Length);

            switch (key)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case (int)DunguenType.Bagic:
                    ChoiceDungeon("초급 던전", ConsoleColor.Green, key);
                    break;
                case (int)DunguenType.Middle:
                    ChoiceDungeon("중급 던전", ConsoleColor.Green, key);
                    break;
                case (int)DunguenType.Hard:
                    ChoiceDungeon("상급 던전", ConsoleColor.Green, key);
                    break;
                case (int)DunguenType.VeryHard:
                    ChoiceDungeon("최상급 던전", ConsoleColor.Green, key);
                    break;
                case (int)DunguenType.Boss:
                    ChoiceDungeon("보스 던전", ConsoleColor.Green, key);
                    break;
                default:
                    TextColor("잘못된 입력입니다", ConsoleColor.Red);
                    break;
            }
            int clear = 0;

            foreach(Dungeon dungeon in dungeons)
            {
                if(dungeons[key - 1].Index == dungeon.Index)
                {
                    if (dungeon.ProAtk <= player.MyAtk(eqItem))
                    {
                        clear++;
                    }
                    if (dungeon.ProDef <= player.MyDef(eqItem))
                    {
                        clear++;
                    }
                    if (dungeon.ProHp <= player.MyHp(eqItem))
                    {
                        clear++;
                    }
                }
                else
                {
                    
                }
            }

            Random random = new Random();
            int dungeonClear = random.Next(clear, 5);

            if(dungeonClear >= 3)
            {
                TextColor("던전을 클리어 하였습니다", ConsoleColor.Yellow);
                player.Gold += dungeons[key - 1].Gain;
            }
            else
            {
                TextColor("던전 공략에 실패 하였습니다", ConsoleColor.Red);
            }
            Thread.Sleep(1000);
            Console.Clear();
            TextColor("다시 도전하시겠습니까?\n\n\n", ConsoleColor.DarkRed);
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 재도전");

            key = CheckValidInput(0, 1);
            switch (key)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayDungeon();
                    break;
            }
        }
        static void ShopWrite(string shop, List<Item> list)
        {
            Console.Clear();
            TextColor("상점\n", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine(shop + " 상점입니다.");
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine("이름\t\t가격\t공격력\t방어력\t체력\t설명");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string status = (i + 1).ToString() + "." + list[i].Name + "\t" + list[i].Price.ToString() + "G" + "\t" + list[i].Atk.ToString() + "\t" + list[i].Def.ToString() + "\t" + list[i].Hp.ToString() + "\t" + list[i].Explanation;
                    Console.WriteLine(status);
                    Console.WriteLine();
                }
            }
        }
        static void ItemWrite()
        {
            Console.WriteLine("인덱스\t이름\t\t공격력\t방어력\t체력\t설명");
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string status = (i + 1).ToString() + ".\t" + items[i].Name + "\t" + items[i].Atk.ToString() + "\t" + items[i].Def.ToString() + "\t" + items[i].Hp.ToString() + "\t" + items[i].Explanation;
                    if (items[i].Equip)
                    {
                        string eqStatus = status.Insert(0, "[E]");
                        Console.Write(eqStatus);
                    }
                    else
                    {
                        string eqStatus = status.Insert(0, "[X]");
                        Console.Write(eqStatus);
                    }
                    Console.WriteLine();
                }
            }
        }
        static void TextColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        static void MyGold()
        {
            Console.Write($"보유 골드 : {player.Gold}");
            TextColor("G\n", ConsoleColor.Yellow);
        }
        static void ChoiceDungeon(string choice, ConsoleColor color, int _index)
        {
            Console.Clear();
            TextColor(choice, color);
            Console.WriteLine("을 선택하였습니다");
            TextDungeonStatus(_index);
            Console.SetCursorPosition(30, 10);
            TextColor("던전 진행중", ConsoleColor.DarkYellow);
            DrawWalls(5);
            Console.SetCursorPosition(30, 15);
        }
        static void DrawWalls(int pos)
        {
            // 상 벽 그리기
            for (int i = 0; i < 80; i++)
            {
                Console.SetCursorPosition(i, pos);
                Console.Write("@");
                Thread.Sleep(10);
            }
            // 우 벽 그리기
            for (int i = pos; i < 20 + pos; i++)
            {
                Console.SetCursorPosition(80, i);
                Console.Write("@");
                Thread.Sleep(10);
            }
            //하 벽 그리기
            for (int i = 80; i > 0; i--)
            {
                Console.SetCursorPosition(i, 20 + pos);
                Console.Write("@");
                Thread.Sleep(10);
            }
            //좌 벽 그리기
            for (int i = 20 + pos; i > pos; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("@");
                Thread.Sleep(10);
            }
            Console.WriteLine();
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
        static void TextDungeonStatus(int i)
        {
            Console.WriteLine("적정 공격력\t적정 방어력\t적정 체력");
            Console.WriteLine($"{dungeons[i].ProAtk}\t\t{dungeons[i].ProDef}\t\t{dungeons[i].ProHp}");
        }
    }
    

    public class Character
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
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
        public int MyAtk(List<Item> eqitem)
        {
            int atk = this.Atk;
            foreach(Item item in eqitem)
            {
                if (item.Equip)
                {
                    atk += item.Atk;
                }
            }
            return atk;
        }
        public int MyDef(List<Item> eqitem)
        {
            int def = this.Def;
            foreach (Item item in eqitem)
            {
                if (item.Equip)
                {
                    def += item.Def;
                }
            }
            return def;
        }
        public int MyHp(List<Item> eqitem)
        {
            int hp = this.Hp;
            foreach (Item item in eqitem)
            {
                if (item.Equip)
                {
                    hp += item.Hp;
                }
            }
            return hp;
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
        public int Level { get; set; }
        public bool Equip { get; set; }
        public virtual void ItemEnforce()
        {
            
        }
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
            this.Level = 0;
        }
        public override void ItemEnforce()
        {
            this.Atk += this.Level * 2;
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
            this.Def = index * 2 + 10 + Level * 5;
            this.Hp = index * 5 + 20 + Level * 10;
            this.Price = index * 600;
            this.Equip = false;
            this.Level = 0;
        }
        public override void ItemEnforce()
        {
            this.Def += this.Level * 4;
            this.Hp += this.Level * 10;
        }
    }
    public struct Dungeon
    {
        public int Index { get; set; }
        public int ProAtk { get; set; }
        public int ProDef { get; set; }
        public int ProHp { get; set; }
        public int Gain { get; set; }
        public void ThisDungeon(int _index)
        {
            this.Index = _index;
            this.ProAtk = _index * 5 + 5;
            this.ProDef = _index * 8 + 8;
            this.ProHp = _index * 80 + 80;
            this.Gain = _index * 100 + 100;
        }
    }
    public enum DunguenType
    {
        Bagic = 1,
        Middle,
        Hard,
        VeryHard,
        Boss
    }
}