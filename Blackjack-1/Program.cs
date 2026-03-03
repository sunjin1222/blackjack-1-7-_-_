using System;
using System.Net.Sockets;


Blackjack blackjack = new();//하나의 덱을 생성한고 게임이 끝날때까지 사용하기위해 사용
Player player = new();
Dealer dealer = new();

while (true)
{
    Console.Clear();
    Console.WriteLine("===블랙잭 게임===");
    Console.WriteLine("카드를 섞는 중...");
    dealer.Reset();
    player.Reset();
    Console.WriteLine("===초기패===");
    player.Betting();
    dealer.AddHands(blackjack.Draw(), blackjack.Draw());
    blackjack.ShowDealer(dealer);
    Console.WriteLine();
    Console.WriteLine();
    player.AddHands(blackjack.Draw(), blackjack.Draw());
    blackjack.ShowPlayer(player);
    Console.WriteLine();

    while (true)
    {
        Console.Write("H(Hit) 또는 S(Stand)를 선택하세요:");
        string input = Console.ReadLine().ToUpper();
        if (input == "H")
        {
            int card = blackjack.Draw();
            player.AddHands(card);
            Console.WriteLine();
            Console.WriteLine($"플레이어가 카드를 받았습니다: [{blackjack.CardSuits[card / 13]} {blackjack.CardNumbers[card % 13]}]");

            blackjack.ShowPlayer(player);
            if (player.IsOver)
            {
                Console.WriteLine();
                Console.WriteLine("21을 넘었습니다");
                Console.WriteLine();

                break;
            }
        }
        else if (input == "S")
        {
            Console.WriteLine("플레이어가 Stand를 선택했습니다");
            Console.WriteLine();
            break;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("다시 선택 해주세요");
        }

    }


    if (player.IsOver) //플레이어 카드가 21을 넘으면 딜러차례없이 게임종료
    {
        GameResult();// 현재코인이 0보다 적으면 그대로 프로그램을 끝나게 만들었는데 모든정보를 리셋하고 코인을 1000 부터 다시 시작하는게 맞는걸까요?
        if (player.CurrentCoin<=0)
        { 
            return;
        }
    }
    else
    {
        dealer.IsOpened = true; //딜러 true 만드는방법이 안떠올라서 그냥 입력해버렸습니다. 확인부탁드려요 
        blackjack.secretcard(dealer); // 딜러의 시크릿 카드 오픈
        blackjack.ShowDealer(dealer);
        Console.WriteLine();

        while (true)
        {
            if (dealer.Score < 17)
            {
                int card = blackjack.Draw();
                dealer.AddHands(card);
                Console.WriteLine();
                Console.WriteLine($"딜러가 카드를 받았습니다: [{blackjack.CardSuits[card / 13]} {blackjack.CardNumbers[card % 13]}]");
                blackjack.ShowDealer(dealer);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                break;

            }
        }
        GameResult();
        if (player.CurrentCoin <= 0)
        {
            return;
        }
        Console.WriteLine();
    }
    for (; ; )
    {
        Console.WriteLine("새 게임을 하시겠습니까? (Y/N): ");
        string input = Console.ReadLine().ToUpper();
        if (input == "Y")
        {
            break;
        }

        else if (input == "N")
        {
            Console.WriteLine("게임을 종료합니다");
            return;
        }
        else
        {
            Console.WriteLine("다시 입력해 주세요");
        }
    }










    void GameResult()
    {
        Console.WriteLine("===게임 결과===");
        Console.WriteLine();
        Console.WriteLine($"플레이어: {player.Score}");
        Console.WriteLine($"딜러: {dealer.Score}");
        Console.WriteLine();
        if (21 < player.Score)
        {
            Console.WriteLine("딜러의 승리");
            player.CurrentCoin -= player.InputCoin;
            Console.WriteLine($"딜러의 승리!(-{player.InputCoin})");
            Console.WriteLine($"보유칩:({player.CurrentCoin})");
            if (player.CurrentCoin <= 0)//현재 가지고 있는 코인이 0이하일 경우 게임오버
            {
                Console.WriteLine($"보유한 코인이 없습니다");
                Console.WriteLine($"게임오버");
            }
        }
        else if (21 < dealer.Score)
        {
            Console.WriteLine("플레이어의 승리");
            player.CurrentCoin += player.InputCoin;
            Console.WriteLine($"플레이어의 승리!(+{player.InputCoin})");
            Console.WriteLine($"보유칩:({player.CurrentCoin})");
        }
        else if (player.Score < dealer.Score)
        {
            Console.WriteLine("딜러의 승리");
            player.CurrentCoin -= player.InputCoin;
            Console.WriteLine($"딜러의 승리!(-{player.InputCoin})");
            Console.WriteLine($"보유칩:({player.CurrentCoin})");
            if (player.CurrentCoin <= 0)
            {
                Console.WriteLine($"보유한 코인이 없습니다");
                Console.WriteLine($"게임오버");
            }
        }          
        else if (player.Score > dealer.Score)
        {
            Console.WriteLine("플레이어의 승리");
            player.CurrentCoin += player.InputCoin;
            Console.WriteLine($"플레이어의 승리!(+{player.InputCoin})");
            Console.WriteLine($"보유칩:({player.CurrentCoin})");
        }
        else
        {
            Console.WriteLine("무승부");
        }
    }

    
}
