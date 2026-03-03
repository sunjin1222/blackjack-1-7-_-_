using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

internal class Player
{
    public int[] Hands;
    public bool IsOver => Score > 21;

    public int Counter;

    public static bool IsOpened = false;

    public int CurrentCoin; //플레이중 변경되는 코인

    public readonly int MaxCoin = 1000; //시작시 가지고있는 코인

    public int InputCoin;//입력받은 코인값 저장


    public Player()
    {
        CurrentCoin = MaxCoin;
        Hands = new int[21];
    }

    public int Score
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < Counter; i++)
            {
                int card = Hands[i] % 13;
                switch (card)
                {
                    case 0:

                        if (sum + 11 > 21)
                        {
                            sum += 1;
                        }
                        else
                        {
                            sum += 11;
                        }
                        break;

                    case 10:
                    case 11:
                    case 12:
                        sum += 10;
                        break;

                    default:
                        sum += card + 1;
                        break;
                }
            }
            return sum;
        }
    }
    private void NewHands()
    {
        Counter = 0;
    }

    public void AddHands(params int[] cards)
    {
        foreach (int card in cards)
        {
            Hands[Counter++] = card;
        }
    }
    public void Reset()
    {
        NewHands();
    }


    public void Betting() //배팅값을 사용자에게 받은후 검사
    {
        Console.WriteLine($"보유 칩: {CurrentCoin}");


        while (true)
        {
            Console.Write($"베팅 금액을 입력하세요: ");
            string input = Console.ReadLine();
            int.TryParse(input, out int inputcoin);

            if (inputcoin > CurrentCoin)
            {
                Console.WriteLine();
                Console.WriteLine($"보유금액을 초과했습니다");
                Console.WriteLine($"다시 입력해 주세요");
                Console.WriteLine();
            }
            else if (inputcoin < 0)
            {
                Console.WriteLine();
                Console.WriteLine($"0이하의 금액을 배팅할수 없습니다");
                Console.WriteLine($"다시 입력해 주세요");
                Console.WriteLine();
            }
            else if (inputcoin <= CurrentCoin && inputcoin > 0)
            {
                Console.WriteLine();
                InputCoin = inputcoin;
                break;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"배팅할 금액을 입력해 주세요");
                Console.WriteLine();
            }

        }
    }

}

