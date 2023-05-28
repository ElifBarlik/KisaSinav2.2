using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MeanShiftClusteringExample
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] veri= new double[][] {
                new double[] { 516.0127058, 393.0145139 },
                new double[] { 436.2117622, 408.6565849 },
                new double[] { 512.0526012, 372.0220136 },
                new double[] { 489.1404645, 401.8071594 },
                new double[] { 446.2079859, 338.5166822 },
                new double[] { 516.4143943, 354.1946332 },
                new double[] { 499.3863531, 414.4322961 },
                new double[] { 489.3139371, 408.0665956 },
                new double[] { 440.1093924, 394.8216766 },
                new double[] { 444.1191708, 367.8503051 },
                new double[] { 430.8531367, 364.0463181 },
                new double[] { 462.7448851, 411.3374508 },
                new double[] { 525.390835, 344.2468509  },
                new double[] { 489.9870399, 366.080455  },
                new double[] { 513.9066881, 378.0179973 },
                new double[] { 485.212895, 347.6738397  },
                new double[] { 529.120055, 359.8463187  },
                new double[] { 518.6871304, 382.7215566  },
                new double[] { 464.5499943, 317.7993655  },
                new double[] { 489.1362873, 322.4817914  },
                new double[] { 517.0391005, 359.4994999  },
                new double[] { 501.924084 , 406.5793763  },
                new double[] { 517.3271821 ,407.1939073  },
                new double[] { 482.4187003, 402.3376873  },
                new double[] { 502.8058242,348.7704175  },
                new double[] { 493.3914849 ,396.1919795  },
                new double[] { 494.2250926 ,320.3687051  },
                new double[] { 506.7176077, 321.3100159  },
                new double[] { 477.906008 , 376.6104354  },
                new double[] { 475.7710041, 385.6888465  },
                new double[] { 526.3674862, 385.9015929  },
                new double[] { 458.4423104 ,412.2611059  },
                new double[] { 541.4330242, 349.1140024  },
                new double[] { 478.2167532, 375.6246257  },
                new double[] { 460.5624526, 391.5014153  },
                new double[] { 530.4595984, 400.3740378  },
                new double[] { 522.9458641, 366.8870077  },
                new double[] { 482.9750995, 385.9668065  },
                new double[] { 494.3666172, 412.2156096  },
                new double[] { 539.5281302, 428.940196  },
                new double[] { 488.6095005 ,352.8908423  },
                new double[] { 463.1618097, 433.6460556  },
                new double[] { 515.9308341,365.3147131 },
                new double[] { 510.5066111 ,356.9058436 },
                new double[] { 468.1945532 ,436.0717679 },
                new double[] { 482.6366921, 416.6518975 },
                new double[] { 491.7874294, 377.3912061 },
                new double[] { 479.6792213, 391.9782906 },
                new double[] { 507.0110518, 413.986641 },
                new double[] { 416.8202611, 406.4029812 },
                new double[] { 493.3263135, 447.4476281  },
                new double[] { 425.0833565, 391.5358349  },
                new double[] { 419.7509494, 357.607321  },
                new double[] { 484.4617012, 387.1707757  },
                new double[] { 440.6937746, 444.2206921  },
            };

            //Kumeleme Gerceklesir
            List<List<double[]>> kumeler = MeanShiftClustering(veri, 2.5);

            //Kumeleri Yazdir
            Console.WriteLine("Kumeler:");
            for (int i = 0; i < kumeler.Count; i++)
            {
                Console.WriteLine($"Kume {i + 1}:");
                foreach (var points in kumeler[i])
                {
                    Console.WriteLine($"({points[0]}, {points[1]})");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        static List<List<double[]>> MeanShiftClustering(double[][] veri, double bantGenisligi)
        {
            List<List<double[]>> kumeler = new List<List<double[]>>();

            //Verileri listeye kopyalar
            List<double[]> points = new List<double[]>(veri);

            while (points.Count > 0)
            {
                //rastgele bir yerel nokta secer
                Random rand = new Random();
                int randomIndex = rand.Next(points.Count);
                double[] randomPoint = points[randomIndex];

                //secilen noktada ortalama kaydirma gerceklesir
                List<double[]> kumelenmisler = new List<double[]>();
                kumelenmisler.Add(randomPoint);

                while (true)
                {
                    //ortalama kaydirma vektorunu hesaplar
                    double[] vektor = new double[randomPoint.Length];
                    int sabit = 0;

                    foreach (var point in points)
                    {
                        double uzaklik = OklidUzakligi(randomPoint, point);

                        if (uzaklik <= bantGenisligi)
                        {
                            for (int i = 0; i < point.Length; i++)
                            {
                                vektor[i] += point[i];
                            }
                                sabit++;
                        }
                    }

                    for (int i = 0; i < vektor.Length; i++)
                    {
                        vektor[i] /= sabit;
                    }

                    
                    if (OklidUzakligi(vektor, randomPoint) <= 0.00001)
                        break;

                    //noktayi gunceller
                    randomPoint = vektor;
                    kumelenmisler.Add(randomPoint);
                }

                //kaydirilan noktalar yeni bir kumeye eklenir
                 kumeler.Add(kumelenmisler);

                
                for (int i = 0; i < kumelenmisler.Count; i++)
                {
                    points.Remove(kumelenmisler[i]);
                }
            }
            

            return kumeler;
        }

        static double OklidUzakligi(double[] point1, double[] point2)
        {
            double toplam = 0;

            for (int i = 0; i < point1.Length; i++)
            {
                double fark = point1[i] - point2[i];
                toplam += fark * fark;
            }

            return Math.Sqrt(toplam);
        }
    }
}
