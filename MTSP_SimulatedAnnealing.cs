
using System;
using System.Collections.Generic;

namespace MRTA
{
    /// <summary>
    /// 任务信息
    /// </summary>
    class OrderInfo
    {
        private int _startVertex;           // 任务起点
        private int _stopVertex;            // 任务终点

        public int StartVertex { get { return _startVertex; } set { _startVertex = value; } }
        public int StopVertex { get { return _stopVertex; } set { _stopVertex = value; } }
    }

    /// <summary>
    /// 小车信息
    /// </summary>
    class CarrierInfo
    {
        private int _currentVertex;             // 小车当前位置e

        public int CurrentVertex { get { return _currentVertex; } set { _currentVertex = value; } }
    }

    /// <summary>
    /// 任务分配
    /// TODO: 高优先级任务提取
    /// TODO: 不同类型小车对应不同任务
    /// </summary>
    class OrderAllocation
    {
        class Pt { public int X; public int Y; public Pt(int x, int y) { X = x; Y = y; } }
        private List<Pt> PtList;

        private List<OrderInfo> OrderInfoList;                          // 任务集合
        private List<double> OrderCostList;                             // 任务代价集合
        private List<CarrierInfo> CarrierInfoList;                      // 小车集合
        private Random RandomGenerator;                                 // 产生随机数
        private List<int> CurrentAllocation;                            // 任务的一种分配
        private double CurrentCost;                                     // 当前分配中的代价总和
        private List<int> OptimalAllocation;                            // 最优的一种分配
        private double OptimalCost;                                     // 最优分配的代价
        private const int SPLIT = -1;                                   // 在分配数组中作为分隔符

        /// <summary>
        /// 初始化分配方案
        /// </summary>
        /// <param name="orderInfoList"></param>
        /// <param name="carrierInfoList"></param>
        public OrderAllocation(List<OrderInfo> orderInfoList, List<CarrierInfo> carrierInfoList)
        {
            Pt[] ptArray =
            {                
                new Pt(12, 12), new Pt(24, 7),
                new Pt(20, 36), new Pt(36, 10), 
                new Pt(50, 10), new Pt(60, 30),
                new Pt(55, 50), new Pt(44, 60),
                new Pt(30, 60),

                new Pt(212, 212), new Pt(224, 207),
                new Pt(220, 236), new Pt(236, 210),
                new Pt(250, 210), new Pt(260, 230),
                new Pt(255, 250), new Pt(244, 260),
                new Pt(230, 260),

                new Pt(12, 12), 
                new Pt(0, 0),
                new Pt(212, 212),
                new Pt(200, 200),
            };
            PtList = new List<Pt>();
            PtList.AddRange(ptArray);

            OrderInfoList = orderInfoList;
            OrderCostList = new List<double>();
            GetOrderCostList(OrderInfoList);

            CarrierInfoList = carrierInfoList;

            RandomGenerator = new Random();

            CurrentAllocation = new List<int>();
            for (int i = 0; i < OrderInfoList.Count; ++i)
            {
                CurrentAllocation.Add(i);
            }
            GetRandomAllocation();

            CurrentCost = GetTotalCost();

            OptimalAllocation = new List<int>();
            OptimalAllocation.AddRange(CurrentAllocation);
            OptimalCost = CurrentCost;
        }
        /// <summary>
        /// 展示信息
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            String toString = "OrderAllocation Info:\n";

            toString += "Orders Info:";
            for (int i = 0; i < OrderInfoList.Count; ++i)
            {
                toString += " [" + i + ": " + OrderInfoList[i].StartVertex + ", " + OrderInfoList[i].StopVertex + "]"; 
            }
            toString += "\n";

            toString += "Orders Cost Info:";
            foreach (double orderCost in OrderCostList)
            {
                toString += " [" + orderCost + "]";
            }
            toString += "\n";

            toString += "Carrier Info:";
            foreach (CarrierInfo carrierInfo in CarrierInfoList)
            {
                toString += " [" + carrierInfo.CurrentVertex + "]";
            }
            toString += "\n";

            toString += "Current Allocation:";
            foreach (int orderIndex in CurrentAllocation)
            {
                toString += " [" + orderIndex + "]";
            }
            toString += "\n";

            toString += "Current Cost:" + CurrentCost + "\n";

            toString += "Optimal Allocation:";
            foreach (int orderIndex in OptimalAllocation)
            {
                toString += " [" + orderIndex + "]";
            }
            toString += "\n";

            toString += "Optimal Cost:" + OptimalCost + "\n";

            return toString;
        }
        /// <summary>
        /// 计算两点间距离，根据地图以及最短路径计算，目前为简化形式
        /// </summary>
        /// <param name="startVertex"></param>
        /// <param name="stopVertex"></param>
        /// <returns></returns>
        private double GetDistance(int startVertex, int stopVertex)
        {
            return Math.Sqrt(Math.Pow(PtList[stopVertex].X - PtList[startVertex].X, 2) + Math.Pow(PtList[stopVertex].Y - PtList[startVertex].Y, 2));
        }
        /// <summary>
        /// 计算任务的代价，并记录到列表中
        /// </summary>
        /// <param name="orderInfoList"></param>
        private void GetOrderCostList(List<OrderInfo> orderInfoList)
        {
            foreach (OrderInfo orderInfo in orderInfoList)
            {
                OrderCostList.Add(GetDistance(orderInfo.StartVertex, orderInfo.StopVertex));
            }
        }
        /// <summary>
        /// 获取一个随机分配，例如5个任务4个小车[x1, x2, x3, SPLIT, x4, SPLIT, x5, SPLIT]
        /// </summary>
        private void GetRandomAllocation()
        {
            for (int i = 0; i < CarrierInfoList.Count - 1; ++i)
            {
                int splitIndex = RandomGenerator.Next(0, CurrentAllocation.Count);
                CurrentAllocation.Insert(splitIndex, SPLIT);
            }
            for (int i = 0; i < CurrentAllocation.Count; ++i)
            {
                int swapIndex = RandomGenerator.Next(0, CurrentAllocation.Count);
                int temp = CurrentAllocation[i];
                CurrentAllocation[i] = CurrentAllocation[swapIndex];
                CurrentAllocation[swapIndex] = temp;
            }
        }
        /// <summary>
        /// 计算小车在指定任务列表之后的代价总和
        /// </summary>
        /// <param name="carrierIndex"></param>
        /// <param name="orderIndexList"></param>
        /// <returns></returns>
        private double GetCarrierCost(int carrierIndex, List<int> orderIndexList)
        {
            double carrierCost = 0;
            int startVertex = CarrierInfoList[carrierIndex].CurrentVertex;
            for (int i = 0; i < orderIndexList.Count; ++i)
            {
                carrierCost += GetDistance(startVertex, OrderInfoList[orderIndexList[i]].StartVertex);
                carrierCost += OrderCostList[orderIndexList[i]];
                startVertex = OrderInfoList[orderIndexList[i]].StopVertex;
            }
            return carrierCost;
        }
        /// <summary>
        /// 计算当前分配的总代价
        /// </summary>
        /// <returns></returns>
        private double GetTotalCost()
        {
            double totalCost = 0;
            int carrierIndex = 0;
            List<int> orderIndexList = new List<int>();
            for (int i = 0; i < CurrentAllocation.Count; ++i)
            {
                if (CurrentAllocation[i] != SPLIT)
                {
                    orderIndexList.Add(CurrentAllocation[i]);
                }
                else
                {
                    totalCost += GetCarrierCost(carrierIndex, orderIndexList);
                    orderIndexList.Clear();
                    ++carrierIndex;
                }
            }
            // 最后一个小车的任务代价
            totalCost += GetCarrierCost(carrierIndex, orderIndexList);
            return totalCost;
        }
        /// <summary>
        /// 随机交换
        /// </summary>
        private void RandomSwapAllocation()
        {
            int firstIndex = RandomGenerator.Next(0, CurrentAllocation.Count);
            int secondIndex = RandomGenerator.Next(0, CurrentAllocation.Count);
            int temp = CurrentAllocation[firstIndex];
            CurrentAllocation[firstIndex] = CurrentAllocation[secondIndex];
            CurrentAllocation[secondIndex] = temp;
        }
        /// <summary>
        /// 随机位移
        /// </summary>
        private void RandomShiftAllocation()
        {
            int splitIndex = RandomGenerator.Next(0, CurrentAllocation.Count);
            List<int> leftPartAllocation = CurrentAllocation.GetRange(0, splitIndex);
            List<int> rightPartAllocation = CurrentAllocation.GetRange(splitIndex, CurrentAllocation.Count - splitIndex);
            CurrentAllocation.Clear();
            CurrentAllocation.AddRange(rightPartAllocation);
            CurrentAllocation.AddRange(leftPartAllocation);
        }
        /// <summary>
        /// 随机倒置
        /// </summary>
        private void RandomReverseAllocation()
        {
            int splitIndex = RandomGenerator.Next(0, CurrentAllocation.Count);
            List<int> leftReversePartAllocation = new List<int>();
            for (int i = splitIndex; i >= 0; --i)
            {
                leftReversePartAllocation.Add(CurrentAllocation[i]);
            }
            List<int> rightReversePartAllocation = new List<int>();
            for (int i = CurrentAllocation.Count - 1; i > splitIndex; --i)
            {
                rightReversePartAllocation.Add(CurrentAllocation[i]);
            }
            CurrentAllocation.Clear();
            CurrentAllocation.AddRange(leftReversePartAllocation);
            CurrentAllocation.AddRange(rightReversePartAllocation);
        }
        /// <summary>
        /// 随机变换分配，包括交换、位移、倒置
        /// </summary>
        /// <param name="ratioSwap">交换概率</param>
        /// <param name="ratio">位移概率</param>
        private void GetNewAllocation(double ratioSwap, double ratioShift)
        {
            double ratioChange = RandomGenerator.NextDouble();
            if (ratioChange < ratioSwap)
            {
                RandomSwapAllocation();
            } 
            else if (ratioChange < ratioSwap + ratioShift)
            {
                RandomShiftAllocation();
            }
            else
            {
                RandomReverseAllocation();
            }
        }
        /// <summary>
        /// 使用模拟退火计算最优分配
        /// </summary>
        public void SimulateAnnealing()
        {
            double initTemperature = 10000000.0;
            double minTemperarure = 1E-7;
            double temperarureDropRate = 0.99995;
            // int maxIteratorTime = 100;
            int currentIteratorTime = 0;
            double currentTemperature = initTemperature;
            while (currentTemperature > minTemperarure)
            {
                GetNewAllocation(0.3, 0.3);
                CurrentCost = GetTotalCost();
                double costDiff = CurrentCost - OptimalCost;
                if ((costDiff < 0) || ((costDiff >= 0) && (Math.Exp(-costDiff / currentTemperature) > RandomGenerator.NextDouble())))
                {
                    OptimalAllocation.Clear();
                    OptimalAllocation.AddRange(CurrentAllocation);
                    OptimalCost = CurrentCost;
                }

                currentTemperature *= temperarureDropRate;
                ++currentIteratorTime;
            }

            //Console.WriteLine("times:" + currentIteratorTime + "\n");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            List<OrderInfo> orderInfoList = new List<OrderInfo>();
            //const int orderNumber = 18;
            //for (int i = 0; i < orderNumber; ++i)
            //{
            //    OrderInfo orderInfo = new OrderInfo();
            //    orderInfo.StartVertex = i + 2;
            //    orderInfo.StopVertex = i + 2;
            //    orderInfoList.Add(orderInfo);
            //}
            int[] orderInfoArray = 
            {
                0, 1,
                1, 2,
                2, 3,
                3, 4,
                4, 5,
                5, 6,
                6, 7,
                7, 8,
                
                //9, 10,
                //10, 11,
                //11, 12,
                //12, 13,
                //13, 14,
                //14, 15,
                //15, 16,
                //16, 17,
                
            };
            for (int i = 0; i < orderInfoArray.Length; i += 2)
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.StartVertex = orderInfoArray[i];
                orderInfo.StopVertex = orderInfoArray[i + 1];
                orderInfoList.Add(orderInfo);
            }

            List<CarrierInfo> carrierInfoList = new List<CarrierInfo>();
            //const int carrierNumber = 1;
            //for (int i = carrierNumber; i > 0; --i)
            //{
            //    CarrierInfo carrierInfo = new CarrierInfo();
            //    carrierInfo.CurrentVertex = 20 - carrierNumber - 1;
            //    carrierInfoList.Add(carrierInfo);
            //}
            int[] carrierInfoArray =
            {
                18,
                //19,
                //20,
                //21,
            };
            for (int i = 0; i < carrierInfoArray.Length; ++i)
            {
                CarrierInfo carrierInfo = new CarrierInfo();
                carrierInfo.CurrentVertex = carrierInfoArray[i];
                carrierInfoList.Add(carrierInfo);
            }

            if ((carrierInfoList.Count > 0) && (orderInfoList.Count > 0))
            {
                OrderAllocation orderAllocation = new OrderAllocation(orderInfoList, carrierInfoList);
                orderAllocation.SimulateAnnealing();
                Console.WriteLine(orderAllocation.ToString());
            } 
            else
            {
                Console.WriteLine("less than 1 orders and less than 1 carrier, no need to do allocation.");
            }
        }
    }
}
