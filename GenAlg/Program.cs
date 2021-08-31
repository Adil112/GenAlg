using System;

namespace GenAlg
{
    class Program
    {
        static double Y = -10;
        static double X = 0;
        static Random rand = new Random();
        static int size = 100;   
        static double coef = 0.5 * size;
        static void Main(string[] args)
        {
            Population populmas = new Population(size); // создание первого поколения
            int N = 0;
            while(true)
            {
                N++;
                // оценивание
                double[] populForCross = new double[(int)coef];
                populForCross = Value(populmas);
                //скрещивание
                int k = 0;
                while (k < size)
                {
                    int i = rand.Next(0, (int)coef-1);
                    int j = rand.Next(0, (int)coef-1);
                    if (0.7 > rand.NextDouble())
                    {
                        double c1 = populForCross[i];
                        double c2 = populForCross[j];
                        Cross(ref c1, ref c2);
                        populmas.popul[k] = c1;
                        populmas.popul[k + 1] = c2;
                        k = k + 2;
                    }
                    else
                    {
                        populmas.popul[k] = populForCross[i];
                        populmas.popul[k + 1] = populForCross[j];
                    }
                };
                //мутация
                Mutation(ref populmas.popul);
                Console.WriteLine($"Поколение: {N}, Лучшая особь: {populForCross[0]}");
                double localY = 1/populForCross[0];
                if (Y < localY) 
                {
                    Y = localY;
                    X = populForCross[0];
                }
                if (N >= 30) break;
            }
            Console.WriteLine($"Значение X: {X}, Значение Y: {Y}");
            
            void Mutation(ref double[] mas)//мутация
            {
                double p = 0.1;
                for(int i =0; i < size; i++)
                {
                    if(p > rand.NextDouble())
                    {
                        mas[i] = rand.NextDouble() * (-4 - 0);
                    }
                }
            }
            void Cross(ref double p1, ref double p2)//скрещивание
            {
                double c1 = 1;
                double c2 = 1;
                c1 = (p1 + p2) * 0.49;
                c2 = (p1 + p2) * 0.51;
                while (c1 < -4) c1 += 0.000001;
                while (c2 < -4) c2 += 0.000001;
                p1 = c1;
                p2 = c2;
            }
            double[] Value(Population pop) // метод оценивания
            {
                double[] fpop = new double[size];
                fpop = pop.popul;
                double[] lpop = new double[(int)coef];
                double temp;
                for (int i = 0; i < size; i++)
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        double ii = 100 - (Math.Abs(-4 - fpop[i])) * 25;
                        double jj = 100 - (Math.Abs(-4 - fpop[j])) * 25;
                        if (ii < jj)
                        {
                            temp = fpop[i];
                            fpop[i] = fpop[j];
                            fpop[j] = temp;
                        }
                    }
                }
                for(int i = 0; i < coef; i++)
                {
                    lpop[i] = fpop[i];
                }
                return lpop;
            }
            Console.ReadKey();
        }
        public class Population//класс популяции
        {
            public double[] popul;
            public Population(int n) 
            {
                popul = new double[n];
                for(int i =0; i < n; i++)
                {
                    popul[i] = Start();
                }
            }
            double Start() 
            {
                double z = rand.NextDouble() * (-3 - 0);
                return z;
            }
        }
    }
}
