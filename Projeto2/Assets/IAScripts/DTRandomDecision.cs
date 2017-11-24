using System;
namespace IPCA.AI.DecisionTrees
{
	/// <summary>
	/// A random decision node. It can contain two or more child branches. When run, it will
	/// randomize a value, and execute the referred branch.
	/// </summary>
	public class DTRandomDecision : DTNode
	{
		readonly DTNode[] actions;
		readonly Random rng;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DecisionTrees.DTRandomDecision"/> class.
		/// </summary>
		/// <param name="child">An array of sub-trees to be chosen randomly from.</param>
		public DTRandomDecision(params DTNode[] child)
		{
			this.actions = child;
			rng = new Random();
		}

		/// <summary>
		/// Randomizes a child, and returns its action.
		/// </summary>
		/// <returns>The chosen action to be executed.</returns>
		public override DTAction MakeDecision()
		{
			return actions[rng.Next(actions.Length)].MakeDecision();
		}
	}
}
