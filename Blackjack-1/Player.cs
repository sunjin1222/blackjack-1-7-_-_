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

    public Player()
    {
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

}

