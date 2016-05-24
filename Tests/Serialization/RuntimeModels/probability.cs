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

namespace Tests.Serialization.RuntimeModels
{
	using System.Diagnostics;
	using SafetySharp.Analysis;
	using SafetySharp.Modeling;
	using Shouldly;
	using Utilities;

	internal class Probability : TestModel
	{
		protected override void Check()
		{
			var d = new D { P = new SafetySharp.Modeling.Probability(0.7) };
			var m = TestModel.InitializeModel(d);

			Create(m);

			StateFormulas.ShouldBeEmpty();
			RootComponents.Length.ShouldBe(1);
			// For the initial state vector 8 bytes are necessary (because sizeof(double)/sizeof(int)==2).
			// Probability is immutable. Thus for the optimized state vector 0 bytes are necessary, but 
			// the minimal number of StateSlots is 1, so StateSlotCount is aligned to 1.
			StateSlotCount.ShouldBe(1); 

			var root = RootComponents[0];
			root.ShouldBeOfType<D>();

			((D)root).P.Value.ShouldBe(0.7);
		}

		private class D : Component
		{
			public SafetySharp.Modeling.Probability P;
		}
	}
}