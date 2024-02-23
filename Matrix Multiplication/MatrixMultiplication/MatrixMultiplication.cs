using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            int[,] output = new int[N, N];
            int[,] a = new int[N / 2, N / 2];
            int[,] b = new int[N / 2, N / 2];
            int[,] c = new int[N / 2, N / 2];
            int[,] c00 = new int[N / 2, N / 2];
            int[,] c01 = new int[N / 2, N / 2];
            int[,] c10 = new int[N / 2, N / 2];
            int[,] c11 = new int[N / 2, N / 2];

            int[,] d = new int[N / 2, N / 2];

            int[,] e = new int[N / 2, N / 2];
            int[,] f = new int[N / 2, N / 2];
            int[,] g = new int[N / 2, N / 2];
            int[,] h = new int[N / 2, N / 2];
            int[,] P1 = new int[N / 2, N / 2];
            int[,] P2 = new int[N / 2, N / 2];
            int[,] P3 = new int[N / 2, N / 2];
            int[,] P4 = new int[N / 2, N / 2];
            int[,] P5 = new int[N / 2, N / 2];
            int[,] P6 = new int[N / 2, N / 2];
            int[,] P7 = new int[N / 2, N / 2];



            if (N <= 64)
            {

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        output[i, j] = 0;
                        for (int k = 0; k < N; k++)
                        {


                            output[i, j] += M1[i, k] * M2[k, j];
                        }
                    }
                }
                return output;
            }
            else
            {
                for (int i = 0; i < N / 2; i++)
                {
                    for (int j = 0; j < N / 2; j++)
                    {
                        a[i, j] = M1[i, j];
                        b[i, j] = M1[i, j + (N / 2)];
                        c[i, j] = M1[i + (N / 2), j];
                        d[i, j] = M1[i + (N / 2), j + (N / 2)];

                        e[i, j] = M2[i, j];
                        f[i, j] = M2[i, j + (N / 2)];
                        g[i, j] = M2[i + (N / 2), j];
                        h[i, j] = M2[i + (N / 2), j + (N / 2)];
                    }
                }
                Parallel.Invoke(

                () => P1 = MatrixMultiply(a, Addition(f, h, '-', N / 2), N / 2),

               () => P2 = MatrixMultiply(Addition(a, b, '+', N / 2), h, N / 2),
               () => P3 = MatrixMultiply(Addition(c, d, '+', N / 2), e, N / 2),
               () => P4 = MatrixMultiply(d, Addition(g, e, '-', N / 2), N / 2),
            () => P5 = MatrixMultiply(Addition(a, d, '+', N / 2), Addition(e, h, '+', N / 2), N / 2),
               () => P6 = MatrixMultiply(Addition(b, d, '-', N / 2), Addition(g, h, '+', N / 2), N / 2),
                () => P7 = MatrixMultiply(Addition(a, c, '-', N / 2), Addition(e, f, '+', N / 2), N / 2)
                );
                int[,] res1 = new int[N / 2, N / 2];
                int[,] res2 = new int[N / 2, N / 2];
                int[,] res3 = new int[N / 2, N / 2];
                int[,] res4 = new int[N / 2, N / 2];



                res1 = (Addition(P5, P4, '+', N / 2));
                res2 = (Addition(res1, P2, '-', N / 2));
                c00 = (Addition(res2, P6, '+', N / 2));
                c01 = (Addition(P1, P2, '+', N / 2));
                c10 = (Addition(P3, P4, '+', N / 2));
                res3 = (Addition(P1, P5, '+', N / 2));
                res4 = (Addition(res3, P3, '-', N / 2));
                c11 = (Addition(res4, P7, '-', N / 2));


                for (int i = 0; i < N / 2; i++)
                {
                    for (int j = 0; j < N / 2; j++)
                    {
                        output[i, j] = c00[i, j];
                        output[i, j + (N / 2)] = c01[i, j];
                        output[i + (N / 2), j] = c10[i, j];
                        output[i + (N / 2), j + (N / 2)] = c11[i, j];


                    }
                }
            }


            return output;


        }



        static public int[,] Addition(int[,] Mat1, int[,] Mat2, Char sign, int N)
        {
            int[,] result = new int[N, N];

            if (sign == '+')
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        result[i, j] = Mat1[i, j] + Mat2[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        result[i, j] = Mat1[i, j] - Mat2[i, j];
                    }
                }

            }
            return result;
        }
    }

    #endregion
}

