﻿// The MIT License (MIT)
// 
// Copyright (c) 2014-2016, Institute for Software & Systems Engineering
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataStructures
{
	using System.Diagnostics;
	using SafetySharp.Analysis;
	using SafetySharp.Analysis.ModelChecking.Probabilistic;
	using SafetySharp.Modeling;
	using SafetySharp.Runtime;
	using Utilities;
	using Xunit;
	using Xunit.Abstractions;
	using SafetySharp.Utilities.Graph;
	using SafetySharp.Analysis.Probabilistic.MdpBased.ExportToGv;

	public class BuiltinMdpModelCheckerTests
	{
		/// <summary>
		///   Gets the output that writes to the test output stream.
		/// </summary>
		public TestTraceOutput Output { get; }
		
		public BuiltinMdpModelCheckerTests(ITestOutputHelper output)
		{
			Output = new TestTraceOutput(output);
		}


		[Fact]
		public void Prob1ETest_Mdp1()
		{
			var mdp = (new MarkovDecisionProcessExamples.Example1()).Mdp;

			var excludedStates = new Dictionary<int, bool>() { };
			var directlySatisfiedStates = new Dictionary<int, bool>() { { 1, true } };

			var checker = new BuiltinMdpModelChecker(mdp);
			var results = checker.StatesReachableWithProbabilityExactlyOneForAtLeastOneScheduler(directlySatisfiedStates, excludedStates);

			Assert.True(results.ContainsKey(1));
		}


		[Fact]
		public void Prob0ATest_Mdp1()
		{
			var mdp = (new MarkovDecisionProcessExamples.Example1()).Mdp;
			var excludedStates = new Dictionary<int, bool>() { };
			var directlySatisfiedStates = new Dictionary<int, bool>() { { 1, true } };

			var checker = new BuiltinMdpModelChecker(mdp);
			var results = checker.StatesReachableWithProbabilityExactlyZeroWithAllSchedulers(directlySatisfiedStates, excludedStates);

			Assert.True(results.ContainsKey(1));
			
		}

		[Fact]
		public void Prob0ETest_Mdp1()
		{
			var mdp = (new MarkovDecisionProcessExamples.Example1()).Mdp;
			var excludedStates = new Dictionary<int, bool>() { };
			var directlySatisfiedStates = new Dictionary<int, bool>() { { 1, true } };

			var checker = new BuiltinMdpModelChecker(mdp);
			var results = checker.StatesReachableWithProbabilityExactlyZeroForAtLeastOneScheduler(directlySatisfiedStates, excludedStates);

			Assert.True(results.ContainsKey(1));
		}
	}
}
