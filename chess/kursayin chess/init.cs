using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursayin_chess
{
     class Init
    {
       
         public int[,] ObjectNum = { { 1,2,3,4,5,6,7,8},
                                    {9,10,11,12,13,14,15,16 },
                                    {0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0 },
                                    { 17,18,19,20,21,22,23,24},
                                    { 25,26,27,28,29,30,31,32} };
        public int Number;
     
        public bool IsRight( int startx, int starty, int endx, int endy,int[,] X)
        {
            if (startx == endx && starty == endy)
                return false;
            if(X[starty,startx]==10)
               return Wpawns(startx, starty, endx, endy,X);
            if(X[starty, startx] == -10)
                return Bpawns(startx, starty, endx, endy,X);
            if (X[starty, startx] == 50 || X[starty, startx] == -50)
                return Rook(startx, starty, endx, endy,X);
            if(X[starty, startx] == 33 || X[starty, startx] == -33)
                return Knight(startx, starty, endx, endy);
            if(X[starty, startx] == 30 || X[starty, startx] == -30)
                return Bishop(startx, starty, endx, endy,X);
            if(X[starty, startx] == 90 || X[starty, startx] == -90)
                return Queen(startx, starty, endx, endy,X);
            if(X[starty, startx] == 1000 || X[starty, startx] == -1000)
                return King(startx, starty, endx, endy);
            return false;
        }
        public bool PointChange(int startx, int starty, int endx, int endy, int[,] cheespoint)
        {
            int a;
            if (IsFree(endy, endx, cheespoint))
            {
                a = cheespoint[endy, endx];
                cheespoint[endy, endx] = cheespoint[starty, startx];
                cheespoint[starty, startx] = a;
                a = ObjectNum[endy, endx];
                ObjectNum[endy, endx] = ObjectNum[starty, startx];
                ObjectNum[starty, startx] = a;
            }
            else
            {
                cheespoint[endy, endx] = cheespoint[starty, startx];
                cheespoint[starty, startx] = 0;
                Number = ObjectNum[endy, endx] - 1;
                ObjectNum[endy, endx] = ObjectNum[starty, startx];
                ObjectNum[starty, startx] = 0;
            }
            if(cheespoint[endy,endx]==-10 && endy==7 )
            {
                cheespoint[endy, endx] = -90;
                return true;
                //MainWindow.IsQueen(endy, endx);
            }
            if(cheespoint[endy,endx]==10 && endy==0)
            {
                cheespoint[endy, endx] = 90;
                return true;
            }
            return false;
        }

        public bool IsFree(int endx,int endy,int[,] cheespoint)
        {
            if (cheespoint[endx,endy] == 0)
                return true;
            else
                return false;
        }
        public bool IsOpponent(int startx, int starty, int endx, int endy,int[,] cheespoint)
        {
            if (cheespoint[starty, startx] < 0 && cheespoint[endy, endx] > 0 || cheespoint[starty, startx] > 0 && cheespoint[endy, endx] < 0)
            {
                return true;
            }
            return false;
        }

        private bool King(int startx, int starty, int endx, int endy)
        {
            if (Math.Abs(startx - endx) == 1 && starty == endy || Math.Abs(endy - starty) == 1 && startx == endx)
            {
             
                return true;

            }
                
            if (Math.Abs(startx - endx) == 1 && Math.Abs(starty - endy) == 1)
            {
               
                return true;
            }

            return false;
        }
        //private bool Castling(int startx,int starty,int endx ,int endy)
        //{
            
       // }
        private bool Queen(int startx, int starty, int endx, int endy, int[,] cheespoint)
        {
            if (startx == endx || starty == endy)
                return Rook(startx, starty, endx, endy,cheespoint);
            else
               if (Math.Abs(startx - endx) == Math.Abs(starty - endy))
                return Bishop(startx, starty, endx, endy,cheespoint);
            return false;
        }
        private bool Bishop(int startx, int starty, int endx, int endy,int[,]cheespoint)
        {

            if (Math.Abs(startx - endx) == Math.Abs(starty - endy))
            {
                if (startx > endx)
                {
                    if (starty > endy)
                    {
                        int i = endx+1;
                        int j = endy+1;
                        while (i < startx && j < starty)
                        {
                            if (cheespoint[j, i] != 0)
                                return false;
                            i++;
                            j++;
                        }
                    }
                    else
                    {
                        int i = endx+1;
                        int j = endy-1;
                        while (i < startx && j > starty)
                        {
                            if (cheespoint[j, i] != 0)
                                return false;
                            i++;
                            j--;
                        }
                    }
                }
                else
                {
                    if (starty > endy)
                    {
                        int i = endx-1;
                        int j = endy+1;
                        while (i > startx && j < starty)
                        {
                            if (cheespoint[j, i] != 0)
                                    return false;
                            i--;
                            j++;
                        }
                    }
                    else
                    {
                        int i = endx-1;
                        int j = endy-1;
                        while (i > startx && j > starty)
                        {
                            if (cheespoint[j, i] != 0)
                                return false;
                            i--;
                            j--;
                        }
                     
                    }
                }
                return true;
            }
            return false;
        }
        private bool Knight(int startx, int starty, int endx, int endy)
        {
            if (Math.Abs(startx - endx) == 2 && Math.Abs(starty - endy) == 1 || Math.Abs(startx - endx) == 1 && Math.Abs(starty - endy) == 2)
                return true;
            return false;
        }
        private bool Rook(int startx, int starty, int endx, int endy,int[,] cheespoint)
        {
            int max, min;
            if (startx == endx)
            {
                if (endy < starty)
                {
                    min = endy;
                    max = starty;
                }
                else
                {
                    min = starty;
                    max = endy;
                }
                for (int i = min + 1; i < max; i++)
                {
                    if (cheespoint[i, startx] != 0)
                        return false;
                }
                return true;
            }
            else
                if (starty == endy)
            {
                
                if (endx < startx)
                {
                    min = endx;
                    max = startx;
                }
                else
                {
                    min = startx;
                    max = endx;
                }
                for (int i = min + 1; i < max; i++)
                {
                    if (cheespoint[starty, i] != 0)
                        return false;
                }
                return true;
            }
            return false;
        }
        private bool Bpawns(int startx, int starty, int endx, int endy, int[,] cheespoint)
        {
            if (startx == endx)
            {
                if (((starty == 1 && endy - starty == 2 && cheespoint[2, startx] == 0 && cheespoint[3, startx] == 0 || endy - starty == 1))
                    && cheespoint[starty + 1, startx] == 0)
                {
                    
                    return true;
                }
            }
            else
            if (starty != 7)
            {
                if (startx == 0 && cheespoint[starty + 1, startx + 1] != 0 && starty + 1 == endy && startx + 1 == endx)
                {
                    
                    return true;
                }
                else
                if (startx == 0)
                    return false;
                else
            if (startx == 7 && cheespoint[starty + 1, startx - 1] != 0 && starty + 1 == endy && startx - 1 == endx)
                {
                    
                    return true;
                }
                else
                if (startx == 7)
                    return false;
                else

                if ((cheespoint[starty + 1, startx - 1] != 0 && endy - starty == 1 && startx - endx == 1)
                || (cheespoint[starty + 1, startx + 1] != 0 && endy - starty == 1 && endx - startx == 1))
                {
                    
                    return true;
                }
            }
            return false;
        }
        private bool Wpawns(int startx,int starty,int endx,int endy, int[,] cheespoint)
        {

            if (startx == endx)
            {
                if (((starty == 6 && starty - endy == 2 && cheespoint[5, startx] == 0 && cheespoint[4, startx] == 0 || starty - endy == 1) && cheespoint[starty - 1, startx] == 0))
                {
                    
                    return true;
                }
            }
            else
            if (starty != 0)
            {
                if (startx == 0 && cheespoint[starty - 1, startx + 1] != 0 && starty - 1 == endy && startx + 1 == endx)
                {
                  
                    return true;
                }
                else
                if (startx == 0)
                    return false;
                else
                if (startx == 7 && cheespoint[starty - 1, startx - 1] != 0 && starty - 1 == endy && startx - 1 == endx)
                {
                   
                    return true;
                }
                else
                if (startx == 7)
                    return false;
                else
                    if (cheespoint[starty - 1, startx - 1] != 0 && starty - 1 == endy && startx - 1 == endx
                    || cheespoint[starty - 1, startx + 1] != 0 && starty - 1 == endy && startx + 1 == endx)
                {
                    
                    return true;
                }
            }
            return false;
        }
       
        
    }
}

