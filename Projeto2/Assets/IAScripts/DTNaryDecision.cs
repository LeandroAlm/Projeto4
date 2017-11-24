using System;
namespace IPCA.AI.DecisionTrees
{
	/// <summary>
	/// A decision with n possible sub-trees, that will be accessible through an integer.
	/// </summary>
	public class DTNaryDecision : DTNode
	{
		readonly DTNode[] actions;
		readonly Func<int> test;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DecisionTrees.DTNaryDecision"/> class.
		/// </summary>
		/// <param name="test">The test, that should be written as a closure, that returns an intger,
		/// starting from 0, stating which child subtree to evaluate.</param>
		/// <param name="child">An array of sub-trees to be evaluated accordingly with the test function.</param>
		public DTNaryDecision(Func<int> test,
		                      params DTNode[] child)
		{
			this.test = test;
			this.actions = child;
		}

		/// <summary>
		/// Takes the decision accordingly with the test function, and calls
		/// recursivelly the child subtree.
		/// </summary>
		/// <returns>The decision.</returns>
		public override DTAction MakeDecision()
		{
			int ans = test();
			if (ans < 0 || ans >= this.actions.Length)
				throw new Exception("Test function returned out of range integer");
			return this.actions[ans].MakeDecision();
		}
	}
}
