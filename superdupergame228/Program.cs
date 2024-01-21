using System;
using System.Collections.Generic;
using System.Linq;

class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Durability { get; set; }

    public Weapon(string name, int damage, int durability)
    {
        Name = name;
        Damage = damage;
        Durability = durability;
    }
}

class Aid
{
    public string Name { get; set; }
    public int Healing { get; set; }

    public Aid(string name, int healing)
    {
        Name = name;
        Healing = healing;
    }
}

class Enemy
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public Weapon EnemyWeapon { get; set; }

    public Enemy(string name, int maxHealth, Weapon weapon)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        EnemyWeapon = weapon;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}

class Player
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public Aid PlayerAid { get; set; }
    public Weapon PlayerWeapon { get; set; }
    public int Score { get; set; }

    public Player(string name, int maxHealth, Aid aid, Weapon weapon)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        PlayerAid = aid;
        PlayerWeapon = weapon;
        Score = 0;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Heal()
    {
        CurrentHealth += PlayerAid.Healing;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }
}

class Program
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("Назови себя воин:");
        string playerName = Console.ReadLine();

        Weapon playerWeapon = new Weapon("Меч", 20, 100);
        Aid playerAid = new Aid("Аптечка", 10);
        Player player = new Player(playerName, 100, playerAid, playerWeapon);

        while (player.CurrentHealth > 0)
        {
            string[] enemyNames = { "Смирнов", "Дракон", "Скелет", "Стив", "Ведьма" };
            string randomEnemyName = enemyNames[random.Next(enemyNames.Length)];
            Weapon randomEnemyWeapon = new Weapon("Экскалибур", 10, 100);
            Enemy enemy = new Enemy(randomEnemyName, 50, randomEnemyWeapon);

            Console.WriteLine($"{player.Name} встречает противка {enemy.Name} ({enemy.CurrentHealth}hp), " +
                $"у противника на поясе оружие {enemy.EnemyWeapon.Name} ({enemy.EnemyWeapon.Damage})");

            while (enemy.CurrentHealth > 0 && player.CurrentHealth > 0)
            {
                Console.WriteLine("Что вы будете делать?");
                Console.WriteLine("1. Аттаковать");
                Console.WriteLine("2. Пропустить свой ход");
                Console.WriteLine("3. Использовать аптечку");

                int choice = int.Parse(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        int playerDamage = random.Next(player.PlayerWeapon.Damage / 2, player.PlayerWeapon.Damage + 1);
                        enemy.TakeDamage(playerDamage);
                        Console.WriteLine($"{player.Name} аттаковал противника {enemy.Name}");
                        Console.WriteLine($"У противника {enemy.CurrentHealth}hp, у вас {player.CurrentHealth}hp");
                        int enemyDamage = random.Next(enemy.EnemyWeapon.Damage / 2, enemy.EnemyWeapon.Damage + 1);
                        player.TakeDamage(enemyDamage);
                        Console.WriteLine($"Противник {enemy.Name} аттаковал вас!");
                        Console.WriteLine($"У противника {enemy.CurrentHealth}hp, у вас {player.CurrentHealth}hp");
                        break;

                    case 2:
                        // Пропускаем ход
                        enemyDamage = random.Next(enemy.EnemyWeapon.Damage / 2, enemy.EnemyWeapon.Damage + 1);
                        player.TakeDamage(enemyDamage);
                        Console.WriteLine($"Противник {enemy.Name} ударил вас!");
                        Console.WriteLine($"У противника {enemy.CurrentHealth}hp, у вас {player.CurrentHealth}hp");
                        break;

                    case 3:
                        player.Heal();
                        Console.WriteLine($"{player.Name} использовал аптечку");
                        Console.WriteLine($"У противника {enemy.CurrentHealth}hp, у вас {player.CurrentHealth}hp");
                        break;

                    default:
                        Console.WriteLine("Неправильный выбор. Попробуйте еще раз.");
                        break;
                }
            }

            if (enemy.CurrentHealth <= 0)
            {
                Console.WriteLine($"Вы победили {enemy.Name}!");
                player.Score++;
            }
            else
            {
                Console.WriteLine("Вы проиграли. Игра окончена.");
                break;
            }
        }

        Console.WriteLine($"Игра окончена. Ваш счет: {player.Score}");
    }
}