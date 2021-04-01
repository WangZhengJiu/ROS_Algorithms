# LifeLong Plan A\*

## 1.Overview
  * Incremental search: DynamicSWSP-FP
  * LPA\*, an incremental version of A\* that combine artificial intelligence and the algorithm literature. It repeatly finds shortest paths from a given start vertex to a given goal vertex while the edge costs of a graph change or vertices are added or deleted
  * It first search is the same as that of a version of A\* that break ties in favor of vertices with smaller g-value but many subsequent searches are potentially faster, because it reuses those parts of the previous search tree
  * Breadth-first search, they typically neithei take advantage of available heuristics nor resue information from previous searches
  * DynamicSWSP-FP, searches from the goal vertex to all other vertices and thus maintains estimates of the goal distances rather than the start distances
  * LPA\* combines both of DynamicSWSP-FP and A\*, and thus is potentially able to replan faster than either DynamicSWSP-FP or A\*
  
## 2.LPA\* Principle
  * LPA\* maintains two estimates of the start distance of each cell, namely a g-value and an rhs-value. The g-value correspond to the g-value of A\* search. The rhs-value are one-step lookahead values based on the g-values and thus potentially better informed than the g-value, the rhs-value of any cell is the minimum over all of its neighbors of the g-value of the neighbor and cost of moving from the neighbor to the cell.
  * g-value equals rhs-value, we call such cells locally consistent
  * Note that the second approach now needs less time than the first one. Furthermore, the second approach provides a starting point for replanning. Locally inconsistent cells thus provide a starting point for replanning
  * However LPA\* does not make every cells locally consistent. Instead, it uses heuristics to focus its search and updates only the g-value that relevant for computing a shortest path.
  * This is an advantage of reusing parts of previous plan-construction processes rather than adapting previous plans, at the cost of large memory requirments
  
## 3.LPA\* Details
  * **LifeLong Planning A\* Algorithm Details**
  
### 3.1.LPA\* Variables
  * LPA\* maintains an estimate g(s) of the start distance g*(s) of each vertex s, the initial search of LPA\* calculates the g-value of each vertex in exactly same order as A\*
  * LPA\* then carries the g-values forward from search to search
  * LPA\* also maintains a second kind of estimate of the start distances. The rhs-values are one-step lookahead values 
  * A vertex is called **locally consistent** if its g-value equals its rhs-value. This concept is similar to **satisfying the Bellman equation for undiscounted deterministic sequential decision problem**
  * A\* maintains an OPEN and a CLOSED list. The CLOSED list allow A\* to avoid vertex reexpansion. LPA\* does not maintain CLOSED list since it uses local consistency checks to avoid vertex reexpansion
  * The OPEN list is a priority queue that allow A\* to always expand a fringe vertex with a smallest f-value. LPA\* also maintain a priority queue for this purpose. Its priority queue always contains exactly the locally inconsistent vertices. The keys of the vertices in the priority queue roughly correspond to the f-value in the A\*, and LPA\* always recalculates the g-value of the vertex ("expand vertex") in the priority queue with the smallest key
  * the Key k(s) of vertex is a vector with two components: **k(s) = [k1(s);k2(s)]**.
  * k1(s) = min(g(s) + rhs(s)) + h(s), correspond directly to the f-values f(s) := g*(s) + h(s)
  * k2(s) = min(g(s) + rhs(s)), correspond to the g-value of A\*

### 3.2.LPA\* Algorithm
  * A locally inconsistent vertex s is called overconsistent (its like remove obstacle) if g(s) > rhs(s). When ComputeShortestPath() expands a locally overconsistent vertex, then it sets the g-value of vertex to its rhs-value, which makes the vertex consistent
  * A locally inconsistent vertex s is called underconsistent (its like add obstacle) if g(s) < rhs(s). When ComputeShortestPath() expands a locally underconsistent vertex, then it simply sets the g-value of the vertex to infinity. This make the vertex either locally consistent or overconsistent
  * if a vertex was locally overconsistent/underconsistent, then the change of its g-value can affect the local consistency of its successor
  * to maintain invariants, ComputeShortestPath() therefore updates the rhs-values of these vertices, checks its locally consistency, and adds them to or remove them from the priority queue accordingly.
  * LPA\* expand vertices until s(goal) is locally consistent and the key of vertex to expand next is no less then the key of s(goal)
  
## 4.Analytical Results

## 5.Optimization of LifeLong Planning A\*

## 6.Extension of LifeLong Planning A\*

## 7.Experimental Evaluation

## 8.An Application to Symbolic Planning
  *
  
## The Proofs
  
## References

  
# Incremental A\*

## 1.Overview
  * Incremental search techniques find series of similar search tasks much faster than is possible by solving each search task from scrach
  * LPA\*: it applies to finite graph search problems on known graphs whose edges cost can increase or decrease over time
  
## 2.Reusing Information from previous searches

## 3.LifeLong Planning A\*
  * **Algorithm Details**
  
## 4.Optimization of LifeLong Planning A\*
  * a vertex somtimes get removed from priority queue and then immediately reinserted with a different key. In this case, its often more efficiency to leave the vertex in the priority queue and update its key, and only change position in the priority queue
  * when UpdateVertex() computes the rhs-value for a successor of an overconsistent vertex it is unneccessary to take the minimum over all of its respective predecessors. It is sufficient to compute the rhs-value as the minimum of its old-value and sum of the new g-value of the overconsistent vertex and the cost of moving from the overconsistent vertex to the successor
  * when UpdateVertex() compute the rhs-value for a successor of an underconsistent vertex, the only g-value that has changed is the g-value of the underconsistent vertex. Since it increased, the rhs-value of the successor can only get affected if its old rhs-value is based on the old g-value of the underconsistent vertex
  * the second and third optimization concerned the computation of the rhs-value of the successors after the g-value of a vertex has changed. Similar optimization can be made for the computation of the rhs-value of a vertex after the cost of one of its incoming edges changed























  
