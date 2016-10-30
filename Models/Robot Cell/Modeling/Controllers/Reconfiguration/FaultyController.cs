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

namespace SafetySharp.CaseStudies.RobotCell.Modeling.Controllers.Reconfiguration
{
	using System.Collections.Generic;
	using SafetySharp.Modeling;
	using Odp;

	class FaultyController : Component, IController
	{
		private readonly IController _controller;

		public FaultyController(IController controller)
		{
			_controller = controller;
		}

		protected FaultyController() { }

		// composition
		public BaseAgent[] Agents => _controller.Agents;
		public virtual bool ReconfigurationFailure =>_controller.ReconfigurationFailure;
		public virtual ConfigurationUpdate CalculateConfigurations(params ITask[] tasks)
		{
			return _controller.CalculateConfigurations(tasks);
		}

		// fault & effect
		public readonly Fault ReconfigurationFault = new TransientFault();

		[FaultEffect(Fault = nameof(ReconfigurationFault))]
		public abstract class ReconfigurationFailureEffect : FaultyController
		{
			public override bool ReconfigurationFailure => true;

			public override ConfigurationUpdate CalculateConfigurations(params ITask[] tasks)
			{
				return null;
			}
		}
	}
}