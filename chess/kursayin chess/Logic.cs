using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kursayin_chess
{

    class Logic
    {
        Init init;
        public int[,] moves = new int[35,6];
        public int[,] moves1 = new int[500, 6];
        int[] min = new int[50];
        public int[,] CopyPoint = new int[8, 8];
        public int[,] CopyPoint1 = new int[8, 8];
        public int[,] cheespoint = new int[8, 8];
        int maxrating = 0;
        int[,] Copy = new int[8, 8];
        int i = 0, max = -1;
        public int BlackRating = 0, WhiteRating = 0, Rating;
        public int count;


        public Logic(Init init1,int[,] x)
        {
            init = init1;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    cheespoint[i, j] = x[i, j];
                }

        }
        public int BlackFigures(int[,] x)
        {
            for (int l = 0; l < 8; l++)
                for (int j = 0; j < 8; j++)
                {
                    cheespoint[l, j] = x[l, j];
                }
           // RatingPosition();
            int i, k = 0;
            Random random = new Random();
            for (int startx=0;startx<8;startx++)
                for(int starty=0;starty<8;starty++)
                {
                  

                    if (cheespoint[startx,starty]<0)
                    {
                       i= BlackMove(startx,starty);
                         for(int j=0;j<i;j++)
                        {
                            moves1[j + k, 0] = moves[j, 0];
                            moves1[j + k, 1] = moves[j, 1];
                            moves1[j + k, 2] = moves[j , 2];
                            moves1[j + k, 3] = moves[j , 3];    
                            moves1[j + k, 4] = moves[j, 4];
                            moves1[j + k, 5] = moves[j, 5];
                        }
                        if (0 < i)
                            k += i;
                    }
                }
            count = k;
           
            return min[random.Next(Min(k))];
        }


        int BlackMove(int startx, int starty)
        {
            int i = 0;
            for (int endy = 0; endy < 8; endy++)
                for (int endx = 0; endx < 8; endx++)
                {

                    for (int q = 0; q < 8; q++)
                        for (int j = 0; j < 8; j++)
                        {
                            CopyPoint[q, j] = cheespoint[q, j];
                        }


                    if ((init.IsFree(endx, endy, cheespoint) && init.IsRight(starty, startx, endy, endx, cheespoint)) || (init.IsRight(starty, startx, endy, endx, cheespoint) && init.IsOpponent(starty, startx, endy, endx, cheespoint)))
                    {

                        PointChange(startx, starty, endx, endy);
                       
                        //dirqi gnahatum spitak
                        moves[i, 0] = startx;
                        moves[i, 1] = starty;
                        moves[i, 2] = endx;
                        moves[i, 3] = endy;
                        WhiteFigures(i);
                        //moves[i,4]=sum(CopyPoint);
                        i++;
                        

                    }


                    for (int q = 0; q < 8; q++)
                        for (int j = 0; j < 8; j++)
                        {
                            cheespoint[q, j] = CopyPoint[q, j];
                        }
                }
            return i;
        }


        void WhiteFigures(int k)
        {
           // max = 0;
            i = 0;


            for (int q = 0; q < 8; q++)
                for (int j = 0; j < 8; j++)
                {
                    CopyPoint1[q, j] = cheespoint[q, j];
                }


            for (int startx = 0; startx < 8; startx++)
                for (int starty = 0; starty < 8; starty++)
                {
                    if (cheespoint[startx, starty] > 0)
                    {

                        WhiteMove(startx, starty, k);
                    }
                }


            for (int q = 0; q < 8; q++)
                for (int j = 0; j < 8; j++)
                {
                    cheespoint[q, j] = CopyPoint1[q, j];
                }
        }


        void WhiteMove(int startx, int starty, int k)
        {
            

            for (int endy = 0; endy < 8; endy++)
                for (int endx = 0; endx < 8; endx++)
                {
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            Copy[i, j] = cheespoint[i, j];
                        }


                    if ((init.IsFree(endx, endy, cheespoint) && init.IsRight(starty, startx, endy, endx, cheespoint)) || (init.IsRight(starty, startx, endy, endx, cheespoint) && init.IsOpponent(starty, startx, endy, endx, cheespoint)))
                    {
                        PointChange(startx, starty, endx, endy);
                        RatingPosition(cheespoint);
                        if (i == 0)
                        {
                            max = Sum(cheespoint);
                            maxrating = Rating;
                            i++;
                        }
                        else
                            if (Sum(cheespoint) > max)
                        {
                            max = Sum(cheespoint);
                            
                        }
                        if (maxrating < Rating)
                        {
                            maxrating = Rating;
                        }
                        moves[k, 4] = max;
                        moves[k, 5] = maxrating;
                        
                    }

                    
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            cheespoint[i, j] = Copy[i, j];
                        }
                }
        }

       public void RatingPosition(int[,] cheespoint)
        {
            int k;
           // WhiteRating = 0;
            BlackRating = 0;
            for(int startx=0;startx<8;startx++)
                for(int starty=0;starty<8;starty++)
                {
                    //if ( cheespoint[startx, starty] == -1000)
                    //    k = 6;
                    //else
                    //    k = 1;

                    //if (cheespoint[startx, starty] < 0)
                    //{
                    //    for (int endx = 0; endx < 8; endx++)
                    //        for (int endy = 0; endy < 8; endy++)
                    //        {
                    //            if (cheespoint[endx, endy] < 0 && init.IsRight(starty, startx, endy, endx, cheespoint))
                    //            {
                    //                switch (cheespoint[endx, endy])
                    //                {
                    //                    case -10:
                    //                        BlackRating = BlackRating + k * 1;
                    //                        break;
                    //                    case -30:
                    //                        BlackRating = BlackRating + k * 2;
                    //                        break;
                    //                    case -33:
                    //                        BlackRating = BlackRating + k * 3;
                    //                        break;
                    //                    case -50:
                    //                        BlackRating = BlackRating + k * 4;
                    //                        break;
                    //                    case -90:
                    //                        BlackRating = BlackRating + k * 5;
                    //                        break;


                    //                }
                    //            }
                    //        }
                    //}

                    if (cheespoint[startx, starty] == -1000)
                        k = 2;
                    else
                        k = 1;
                    if (cheespoint[startx, starty] < 0)
                    {
                        for (int endx = 0; endx < 8; endx++)
                            for (int endy = 0; endy < 8; endy++)
                            {
                                if (cheespoint[endx, endy] < 0 && init.IsRight(starty, startx, endy, endx, cheespoint))
                                {
                                    BlackRating = BlackRating + k;
                                }
                            }
                    }

                }

            Rating = BlackRating;
        }



        int Min(int k)
        {
            int number = 0, maxreting = 0;
            int min = moves1[0, 4];
          
            for (int i = 0; i < k; i++)
                if (moves1[i, 4] <= min)
                {
                    min = moves1[i, 4];
                    maxreting = moves1[i, 5];
                }
            for (int i = 0; i < k; i++)
            {
                if (moves1[i, 4] == min && moves1[i, 5] > maxreting)
                {
                    maxreting = moves1[i, 5];
                }
            }
            for (int i = 0; i < k; i++)
            {
                if (moves1[i, 4] == min && moves1[i,5]==maxreting)
                {
                    this.min[number] = i;
                    number++;
                }

            }
            //RatingPosition();
            return number;

        }

       


        
        int Sum(int[,] x)
        {
            int sum = 0;
            for(int i=0;i<8;i++)
                for(int j=0;j<8;j++)
                {
                    sum += x[i, j];
                }
            return sum;
        }



       
        public void PointChange(int startx, int starty, int endx, int endy)
        {
          
            {
                cheespoint[endx, endy] = cheespoint[startx, starty];
                cheespoint[startx, starty] = 0;
               
            }
        }

        

    }
}