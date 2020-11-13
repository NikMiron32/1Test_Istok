using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp2
{
    class Tank
    {
        int armor;//Броня
        public double health;//Здоровье
        int damage;//Урон
        public int ammo;//Патроны

        public Tank(int a, double h, int d, int p)//конструктор
        {
            armor = a;
            health = h;
            damage = d;
            ammo = p;
        }

        public void Shot(Tank tank, Random rand1, Random rand2)//метод выстрел
        {
            if (ammo > 0)//если патронов больше 0, то стреляем
            {
                int r1 = rand1.Next(1, 11);//
                int r2 = rand2.Next(1, 6);
                if (r1 == 0)//крит урон вероятность 0.1
                {
                    Console.WriteLine("Крит\n");
                    tank.health = tank.health - (damage * 1.2) + tank.armor;
                    ammo -= 1;//вычетает один патрон
                }
                else if (r2 == 0 || r2 == 1)//промах вероятность 0.2
                {
                    Console.WriteLine("Промах\n");
                    ammo -= 1;//вычетает один патрон
                }
                else
                {
                    Console.WriteLine("Выстрел\n");
                    tank.health = tank.health - damage + tank.armor;
                    ammo -= 1;//вычетает один патрон
                }
            }
            else
            {
                Console.WriteLine("Кончились патроны\n");
                Console.WriteLine("Купите патроны\n");
            }
        }

        public void Healing(double startHeal)//метод починка
        {
            if ((health + 5) <= startHeal)
            {
                Console.WriteLine("Починка\n");
                health += 5;
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("Здоровье и так полное\n");
            }
        }

        public void BuyAmmo()//метод покупка патронов
        {
            Console.WriteLine("Покупка\n");
            ammo += 2;
        }

        public void GetInfo()//метод вяводит информацию о состоянии здоровья, брони и урона
        {
            Console.WriteLine($"Броня: {armor} \nЖизнь: {health} \nУрон: {damage} \nПатронов: {ammo}\n\n");
        }

        public static string GetAction()//метод выводит инфу о том, как управлять игрой
        {
            Console.WriteLine("Для выстрела введите S \nДля лечения введите H \nДля покупки введите B\n");
            string action = Console.ReadLine();
            return action;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rand1 = new Random();//для вероятности 0.1 (Равная вероятность гарантируется только в рамках конкретного экземпляра Random)
            Random rand2 = new Random();//для вероятности 0.2

            var myTank = new Tank(5, 100, 10, 5);
            double myTankHealth = myTank.health;//сохраням начальное значение здоровья чтобы починка не работала если здоровье и так максимальное
            Console.WriteLine("My tank");
            myTank.GetInfo();//вывели инфу

            var compTank = new Tank(5, 100, 10, 5);
            double compTankHealth = compTank.health;
            Console.WriteLine("Computer tank");
            compTank.GetInfo();

            string choice = Tank.GetAction();//переменная выбора действия (выстрел/починка/покупка)
            while (choice == "S" || choice == "H" || choice == "B")// программа считает за выход из игры любой символ отличный от символа какого-либо действия
            {
                Console.WriteLine("My tank -> ");//ниже будет действие какое мы выбрали
                switch (choice)
                {
                    case ("S"):
                        myTank.Shot(compTank, rand1, rand2);//стреляем
                        break;
                    case ("H"):
                        myTank.Healing(myTankHealth);//чинимся
                        break;
                    case ("B"):
                        myTank.BuyAmmo();//покупаем патроны
                        break;
                }

                Random rand = new Random();
                int r = rand.Next(1,3);//случайный быбор действия для компа (стрелять/чиниться)
                Console.WriteLine("Computer tank -> ");//ниже будет действие компьютера
                switch (r)
                {
                    case (1):
                        if(compTank.ammo > 0)
                        {
                            compTank.Shot(myTank, rand1, rand2);//стреляет
                        }
                        else
                        {
                            compTank.BuyAmmo();//покупает патроны в лучае их отсутствия
                        }
                        break;
                    case (2):
                        compTank.Healing(compTankHealth);//чинится
                        break;
                    //case (3):
                    //    compTank.BuyAmmo();//покупает патроны
                    //    break;
                }
                //выводим состояния танков на конец хода
                Console.WriteLine("My tank");
                myTank.GetInfo();

                Console.WriteLine("Computer tank");
                compTank.GetInfo();
                //выбираем действие
                choice = Tank.GetAction();
            }
        }
    }
}
