# 1.Cooperative PatthFinding (David Silver)

## 1.1.Multiple Agents PathFinding
  * Cooperative PathFinding: a multi-agent path planning problem where agents must find non-colliding routes to separate destinations, given full information about the routes of other agents
  * Non-Cooperative PathFinding: agents have no knowledge of each other's plans, and must predict their future movements 
  * Antagonistic PathFinding: each agent tries to reach its own goal whilst preventing other agents from reaching theirs
  
## 1.2.A Star Algorithms
  * Local Repair A Star (LRA\*): not performing well when on difficult maps with narrow passageways and many agents
  * Cooperative A Star (CA\*): search space-time for a non-colliding routes
  * Hierarchical Cooperative A Star (HCA\*): use an abstract heuristic to **boost performance**
  * Windowed Hierarchical Cooperative A Star (WHCA\*): limits the space-time search depth to a dynamic window, spreading computation over the duration of the route
  
## 1.3.Local Repair A Star
  * family of algorithms wildly used in video game industry
  * each agent searchs for a route to the destination using A Star, ignoring all other agents except for its current neighbors. The agent then begin to follow their routes , until a collision is imminente
  * cycles problem: increase agents agitation level every time its forced to reroute
  * have several drawbacks in difficult environments
  
## 1.4.Cooperative A Star
  * task is decoupled into a serise of single agent searches
  * the individual searches are performed **in three dimensional space-time**, and take acount of the planned routs by other agents
  * reservation table: represent agent's shared knowledge about each other's plannned routes
  * such algorithms are sensitive to the ordering of the agents, requiring the sensible priorities to be selected for good performance, for example use Latombe's **prioritized planning**
  
## 1.5.Hierarchical Cooperative A Star
  * Hierarchical A Star
  * **heirarchical** refers to a series of abstractions of the state space, each more general than the previous, and is not restrict to spatial hierachy
  * Hierarchical A Star: uses a simple hierachy containing a single domain abstraction, which ignores both the time dimension and reservation table
  * one of the issue with Hierarchical A Star is how best to reuse search data in the abstract domain, Holte introduces 3 different techniques for reusing search data in his paper. And the 4th approach is introduced here, which is to use Reverse Resumable A Star(RRA\*)
  * Reverse Resumable A Star: executes a modified A Star search in a reverse direction. The search starts at the agent's goal G, and heads towards to agent's initial position O. Instead of terminating an O, the search continues until a specific node N is expanded
  * One issue is that if an agent sits on its destination, for example in a narrow corridor, then it may block off parts of the map to other agents. Ideally agents should continue to cooperate after they reaching their destination, so that an agent can move off its destination and allow other to pass
  * Second issue is the sensitivity to agent ordring.
  * Third issue is it must calculate a complete route to the destination in a large, three dimensional state space.
  
## 1.6.Windowed Hierarchical Cooperative A Star
  * With single agent searches, planning and plan execution are often interleaved to achieve greater efficiency by avoiding the need to plan for long term contingencies that do not in fact occur. WHCA\* has similar idea.
  * A simple solution to all of these issues is to windowed the search. The current search is limited to a fixed depth specified by the current window
  * In addition, the windowed search can continue once the agent has reached its destination. The agent's goal is no longer to reach the destination, but to complete the window via a terminal edge.


# 2.Conflict-Based Search for Optimal Multi-Agent Path Finding (2012)

## 2.1.Introduction
  * Conflict-Based Search (CBS): is a continuum of coupled and decoupled approaches. CBS guarantees optimal solutions, like most coupled approaches
  * CBS is two-level algorithm where the high level search is performed in a constraint tree (CT) whose nodes include constraint on time and location for a single agent
  
## 2.2.Previous Optimal Solvers
  * a common heuristic function for MAPF solvers is the sum of individual costs heuristic (SIC)
  * Independence detection (ID): general framework which runs as a base level and can use any possible MAPF on it. The basic idea of ID is to divide the agents into independent groups
  * Conflict avoidance table (CAT): dynamic look up table called the conflict avoidance table
  * Operator decomposition (OD): OD is specific for an A star based solver. OD reduces the branching factor by introducing intermediate states between the regular states
  * The increasing Cost Tree search (ICTS): base on the understanding that a complete solution for entire problem is built from individual paths. ICTS divides the MAPF problem into two levels. At the high level it searches a tree called the increasing cost tree. At low level, it performs a goal test on the high level ICT nodes.
  
## 2.3.The Conflict-Based Search Algorithm (CBS)
  * A consistent path for agent is a path that satisfies all its constraints. Likewise, a constraint solution is a solution that is made up from paths, such that the path for agent is consistent with the constraints.
  * Conflict is a tuple (ai, aj, v, t) where agent ai and agent aj occupy vertex v at time t
  * the key idea of CBS is to grow a set of constraints for each of the agents and find path that are consistent with these constraints.
  
## 2.4.High Level: search the Constraint Tree (CT)
  * A set of constraints:the child of a node in the CT inherits the constraints of the parent and add one new constraint for one agent
  * A solution: a set of k paths
  * The total cost: of the current solution
  * goal node: the Node N in CT is a goal node when N.solution is valid, set of paths for all agents have no conflict
  * validation: checking the conflict
  * resolving conflict: Node that for a given CT node N one does not have to save all its cumulative constraints, instead it can save only its latest constraint and extract the other constraints by traversing the path from N to the root via its ancestor
  
## 2.5.Low Level: Find Path For CT Nodes
  * given an agent, ai, and set of associated constraints
  * whenever a state x is generated with g(x) = t and there exists a constraint (ai, x, t) in the current CT node this state is invalid
  
## 2.6.Theoretical Analysis: Optimality of CBS

## 2.7.Comparison with other algorithms

## 2.8.Discussion and future work
  * CBS outperforms other algorithms in cases where corridors and boutlnecks are more dominant
  * adapting Meta-agent ideas from ICTS+3E
  * performing independent detection for CT nodes
  * finding admissible heuristic for the CT
  * the approach of mixing constraints and search is related to rescent work on the theoretical properties of A star and SAT algorithms


# 3.Conflict-Based Search for Optimal Multi-Agent Path Finding (2015)

## 3.1.MA-CBS
  * Meta-Agent CBS is not restricted to single-agent searches at low level. Instead, MA-CBS allows agents to be merged into small groups og joint agents, this mitigates some of the drawbacks of basic CBS and further improves performance
  * In fact, MA-CBS is a framework that can be built on top of any optimal and complete MAPF solver in order to enhance its performance
  * the number of conflicts allowed between any pair of agents is bounded by a predefined parameter B. When the number of conflicts exceed B, the conflicting agents are merged into a meta-agent and then treated as a joint composite agent by the low level solver. In Low level search, **MA-CBS uses any possible complete MAPF solver to find paths for meta-agent**
  * MA-CBS can be viewed as a solving framework where low level solvers are plugged in
  * Different merge polices give rise to different special cases.

## 3.2.Introduction
  * optimal and sub-optimal
  
## 3.3.Problem definition and terminology
  * Problem Input: graph, k agents with (S, G)
  * Actions: move action, wait action
  * MAPF Constraints: disallowing more than one agent to traverse the same edge between successive time steps. A conflict is a case where a constraint is violated
  * MAPF Task: set of non-conflict paths
  * Cost Function: sum of cost
  * Districuted vs. Centralized:
  * Examples
  
## 3.4.Survey of centralized MAPF algorithms
  * Reduction-based solver: reducing to Boolean satisfiability (SAT), Integer Linear Programming (ILP), Answer Set Programming (ASP)
  * MAPF-specific sub-optimal solvers
    * Search-based sub-optimal solvers: HCA\*, WHCA\*
    * Rule-based sub-optimal solvers: (includes specific movement rules for different scenarios and usually do not include massive search. The agents plan their route accorrding to the specific rules) TASS, Push and Swap, pepple motion coordination problem (PMC)
    * Hybrid solvers: mix
  * Optimal Solvers: k-agent state space
    * Admissible heuristics for MAPF: sum of individual costs heuristic (SIC)
    * Drawbacks of A star for MAPF: size of th state space, branching factor
    * Reducing the effective number of agents with Independence Detection: Independence Detection (ID), **Plan a path for the merged group**, A\*\+ID
    * Enhancements to ID (EID): in order to improve identifying independent groups of agents, Standley proposed a tie-breaking rule using a conflict avoidance table (CAT)
    * M Star (M\*): enhanced version - Recursive M Star (RM\*)
    * Avoiding surplus nodes
    * Operator decomposition (OD)
    * Enhanced patial expansion (EPEA\*)
    * Increasing Cost tree search (ICT): two level search algorithm, pruning rules
    
## 3.5.The conflict based search algorithm (CBS)
  * Definition of CBS: path, solution, conflict, constraint
  * High level
    * constraint tree
    * Processing a node in the CT
    * Resolving Conflict
    * Edge conflict: (ai, aj, v1, v2, t), edge constraint (ai, v1, v2, t)
    * Pseudo-code and example
  * Low level
  
## 3.6.Theoretical analysis
  * optimal of CBS
  * completeness of CBS
  * comparison with other algorithms
  
## 3.7.CBS empirical evaluation
  * Experiment Setting

## 3.8.CBS using different cost functions

## 3.9.Meta-agent conflict based search (MA-CBS)
  * general CBS-based framework
  * motivation for meta-agent cbs: understanding the realtion between map topologies and MAPF algorithms performance. MA-CBS is a first step towards dynamically adapting algorithms
    * CBS behaves poorly when a set agents is strongly coupled, when there is a high rate of internal conflict between agents in a set.
    * MA-CBS remedies this behavior of CBS and automatically identifying sets of stronly coupled agents and merge them into meta-agent.
    * consequently, the low-level solver of MA-CBS must be an MAPF solver (A\*+OD, EPEA\*, M\*)
  * Merging agents into a meta-agent:
    * Branch: CBS
    * Merge: low-level search for a meta-agent of size M is in fact an optimal MAPF problem for M agents and should be solved with optimal MAPF solver. Note that the f-cost of this CT node may increase due to this merge action.
    * two important components: a merging policy to decide which option to choose; a constraint-merging mechanism to define the constraints imposed on the new meta-agent
  * Merging policy: conflict bound parameter (MA-CBS(B)), conflict matrix (CM), shouldMerge
  * Merging constraints: meta constraint, meta conflict
  * Low-level solvers
  * Completeness and optimality of MA-CBS
  
## 3.10.MA-CBS experimental results
  * Density
  * Topology
  * Low-level Solver
  
## 3.11.Summary, conslusion, and future work

## 3.12.Memory restricted CBS
  * duplicate CT.Node
  * for memory efficiency variant, we define a new constraint ^(ai, v, t) means that agent ai must be at location v at time t
  * now when a conflict is found, three children are generated




































