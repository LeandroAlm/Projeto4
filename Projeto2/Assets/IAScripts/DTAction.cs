using System;

namespace IPCA.AI.DecisionTrees
{
	/// <summary>
	/// DTAction represents a leaf in a decision tree, and encapsulates
	/// the code to be run when this decision is taken.
	/// </summary>
	public class DTAction : DTNode
	{
		readonly Action action;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DecisionTrees.DTAction"/> class.
		/// </summary>
		/// <param name="action">An Action object, repesenting the action to take.</param>
		public DTAction(Action action)
		{
			this.action = action;
		}

		/// <summary>
		/// For Action nodes, this just returns the defined action to be run. 
		/// </summary>
		/// <returns>The Action to be executed, using <see cref="T:DecisionTrees.DTAction.Run"/> method.</returns>
		public override DTAction MakeDecision()
		{
			return this;
		}

		/// <summary>
		/// Executes que node action.
		/// </summary>
		public void Run()
		{
			this.action();
		}
	}
}