using System;
namespace IPCA.AI.DecisionTrees
{
	/// <summary>
	/// A binary decision node. It includes a boolean function (the test function) that
	/// will be tested on each tree run, and two branches of nodes, one in the case the
	/// test function returns a true value, and the other in case the test function returns
	/// a false value.
	/// 
	/// It is suggested that the test function be defined as a closure.
	/// </summary>
	public class DTBinaryDecision : DTNode
	{
		readonly Func<Boolean> testFunction;
		readonly DTNode trueTree, falseTree;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DecisionTrees.DTBinaryDecision"/> class.
		/// </summary>
		/// <param name="test">The boolean test, as a function returning a boolean.
		/// When possible use it as a closure.</param>
		/// <param name="tTree">Sub-decision tree to be evaluated when the test holds a true value.</param>
		/// <param name="fTree">Sub-decision tree to be evaluated when the test holds a false value.</param>
		public DTBinaryDecision(Func<Boolean> test, DTNode tTree, DTNode fTree) 
		{
			testFunction = test;
			trueTree = tTree;
			falseTree = fTree;
		}

        /// <summary>
        /// Executes the test function, and returns the action to be executed.
        /// </summary>
        /// <returns>The branch that shall be executed accordingly with the test function.</returns>
		public override DTAction MakeDecision()
		{
			bool value = testFunction();
			return (value ? trueTree : falseTree).MakeDecision();
		}
	}
}
