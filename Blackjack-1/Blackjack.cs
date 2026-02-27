using System;
using System.Collections.Generic;
using System.Text;
internal class Blackjack
{
    public readonly string[] CardNumbers = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public readonly string[] CardSuits = { "♠", "♡", "◇", "♣" };

    public int[] CurrDeck;

    public int Counter;


    public Blackjack()
    {
        NewDeck();
    }
      


    private void NewDeck()
    {
        CurrDeck = new int[52];

        Counter = 0;

        Console.WriteLine("카드를 섞는 중...");

        for (int i = 0; i < CurrDeck.Length; i++)
        {
            CurrDeck[i] = i;
        }

        for (int i = CurrDeck.Length - 1; i > 0; i--)
        {
            Random random = new Random();
            int x = random.Next(0, i + 1);

            int temp = CurrDeck[i];
            CurrDeck[i] = CurrDeck[x];
            CurrDeck[x] = temp;
        }

    }
    public int Draw()
    {
        if (Counter > CurrDeck.Length - 1)
        {
            Console.WriteLine("새로운 덱을 열었습니다");
            NewDeck();
        }
       return CurrDeck[Counter++];
    }


    
    public void ShowDealer(Dealer dealer)
    {
        string cardSuit;
        string cardNumber;

        Console.Write($"딜러의 패: ");
        for (int i = 0; i < dealer.Counter; i++)
        {
            cardSuit = CardSuits[dealer.Hands[i] / 13];
            cardNumber = CardNumbers[dealer.Hands[i] % 13];
            if (!dealer.IsOpened && i == 0)
            {
                Console.Write($"[??] ");
            }
            else
            {
                Console.Write($"[{cardSuit} {cardNumber}] ");
            }
        }
      
        Console.WriteLine();
        Console.Write($"딜러 점수: {(dealer.IsOpened ? dealer.Score : " ??")}");
       

    }

    public void ShowPlayer(Player player)
    {
        string cardSuit;
        string cardNumber;

        Console.Write($"플레이어의 패: ");
        for (int i = 0; i < player.Counter; i++)
        {
            cardSuit = CardSuits[player.Hands[i] / 13];
            cardNumber = CardNumbers[player.Hands[i] % 13];
            
                Console.Write($"[{cardSuit} {cardNumber}] ");
          
        }
        Console.WriteLine();
        Console.WriteLine($"플레이어 점수: {player.Score}");

    }


    public void secretcard(Dealer dealer)//이게 새로만든 매소드 입니다.
    { 
        int i = 0;
        string cardSuit;
        string cardNumber;
        cardSuit = CardSuits[dealer.Hands[i] / 13];
        cardNumber = CardNumbers[dealer.Hands[i] % 13];
        Console.WriteLine($"딜러의 숨겨진 카드: [{cardSuit} {cardNumber}]");
    }



}

