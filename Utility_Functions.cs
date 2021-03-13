/// <summary>
/// 任意角度转换为弧度
/// </summary>
/// <param name="degree">任意角度</param>
/// <returns>弧度</returns>
private double RadianOf(double degree)
{
    return (Math.PI / 180) * degree;
}

/// <summary>
/// 计算弧线是否为顺时针方向
/// </summary>
/// <param name="x0"></param>
/// <param name="y0"></param>
/// <param name="startRadian"></param>
/// <param name="x1"></param>
/// <param name="y1"></param>
/// <returns></returns>
private bool IsClockWise(double x0, double y0, double startRadian, double x1, double y1)
{
    double dotProduct = (x1 - x0) * 1 + (y1 - y0) * 0;
    double startToEndLength = Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));            
    double xToLineRadian = Math.Acos(dotProduct / startToEndLength);

    double crossProdcut = 1 * (y1 - y0) - (x1 - x0) * 0;
    if (crossProdcut < 0)
    {
        xToLineRadian = Math.PI * 2 - xToLineRadian;
    }

    if ((startRadian >=0) && (startRadian < Math.PI))
    {
        if ((xToLineRadian > startRadian) && (xToLineRadian < Math.PI + startRadian))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    else
    {
        if ((xToLineRadian < (startRadian - Math.PI)) || (xToLineRadian > startRadian))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

/// <summary>
/// 计算弧线所占的格子集合
/// </summary>
/// <param name="x0"></param>
/// <param name="y0"></param>
/// <param name="startAngle"></param>
/// <param name="x1"></param>
/// <param name="y1"></param>
/// <param name="endAngle"></param>
/// <param name="nextRotateTheta"></param>
/// <returns></returns>
public List<int> GetArcEdgeOccupyCells(double x0, double y0, double startAngle, double x1, double y1, double endAngle, double nextRotateTheta)
{
    double startRadian = RadianOf(startAngle);
    double endRadian = RadianOf(endAngle);

    // 求半径
    double arcRadian = Math.Abs(startRadian - endRadian);
    double lineStartToEnd = Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
    double radius = (lineStartToEnd / 2) / Math.Abs(Math.Sin(arcRadian / 2));

    // 求圆心
    double centerX = 0;
    double centerY = 0;            
    double directionFactor = 0;
    if (IsClockWise(x0, y0, startRadian, x1, y1))
    {
        centerX = x0 + radius * Math.Sin(startRadian);
        centerY = y0 - radius * Math.Cos(startRadian);

        if (endRadian > startRadian)
        {
            endRadian -= Math.PI * 2;
        }
        directionFactor = -1;
    }
    else
    {
        centerX = x0 - radius * Math.Sin(startRadian);
        centerY = y0 + radius * Math.Cos(startRadian);

        if (startRadian > endRadian)
        {
            startRadian -= Math.PI * 2;
        }
        directionFactor = 1;
    }

    // 以起始点每递增5度，取出弧线上的点，然后拿到这5度的线段
    double radianStep = RadianOf(5) * directionFactor;
    double curRadian = startRadian + radianStep;
    double preX = x0;
    double preY = y0;
    List<EdgeInfo> lineEdges = new List<EdgeInfo>();
    while (true)
    {
        if (curRadian * directionFactor < endRadian * directionFactor)
        {
            double curX = centerX + radius * Math.Sin(curRadian) * directionFactor;
            double curY = centerY - radius * Math.Cos(curRadian) * directionFactor;
            EdgeInfo edge = new EdgeInfo(0, preX, preY, 0, curX, curY, 0, 0, EdgeType.Line);
            lineEdges.Add(edge);
            preX = curX;
            preY = curY;
            curRadian += radianStep;
        }
        else
        {
            curRadian = endRadian;
            EdgeInfo edge = new EdgeInfo(0, preX, preY, 0, x1, y1, 0, nextRotateTheta, EdgeType.Line);
            lineEdges.Add(edge);
            break;
        }                
    }

    // 将所有线段的格子叠加
    List<int> arcOccupyCells = new List<int>();
    for (int i = 0; i < lineEdges.Count; ++i)
    {
        EdgeInfo edge = lineEdges[i];

        List<int> lineOccupyCells = GetLineEdgeOccupyCells(edge.StartX, edge.StartY, edge.StopX, edge.StopY, edge.nextRotateTheta);
        foreach (int cell in lineOccupyCells)
        {
            if (!arcOccupyCells.Contains(cell))
            {
                arcOccupyCells.Add(cell);
            }
        }
    }

    return arcOccupyCells;
}

