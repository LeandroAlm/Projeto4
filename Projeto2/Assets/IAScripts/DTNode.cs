using System;
namespace IPCA.AI.DecisionTrees
{
	/// <summary>
	/// An abstract decision tree node.
	/// </summary>
	abstract public class DTNode
	{
		/// <summary>
		/// Takes the decision on how to evaluate the current tree node, and
		/// evaluates recursively the subtree.
		/// </summary>
		/// <returns>The action to be run.</returns>
		abstract public DTAction MakeDecision();
	}
}
