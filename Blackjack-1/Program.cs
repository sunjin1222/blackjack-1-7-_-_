using System;
using System.Net.Sockets;


Blackjack blackjack = new();//이거 Whilea 문안에넣으면 재시작할때마다 덱을 리셋할거 같아서 일단 밖으로움겨두었습니다 확인 부탁드려요  

while (true)
{
    Console.Clear();
    Console.WriteLine("===블랙잭 게임===");
    Console.WriteLine("카드를 섞는 중...");
    Dealer dealer = new();
    Player player = new();
    Console.WriteLine("===초기패===");
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


    if (player.IsOver) //21이 넘으면 바로 게임을 끝나게 만듬
    {
        GameResult();
    }
    else
    {

        dealer.IsOpened = true; //딜러 true 만드는방법이 안떠올라서 그냥 입력해버렸습니다. 확인부탁드려요 
        blackjack.secretcard(dealer); // 잘몰라서 그냥 블랙잭 클래스안에서 하나 만들었습니다.
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
        }
        else if (21 < dealer.Score)
        {
            Console.WriteLine("플레이어의 승리");
        }
        else if (player.Score < dealer.Score)
        {
            Console.WriteLine("딜러의 승리");
        }
        else if (player.Score > dealer.Score)
        {
            Console.WriteLine("플레이어의 승리");
        }
        else
        {
            Console.WriteLine("무승부");
        }
    }
}






