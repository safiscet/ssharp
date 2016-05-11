// The MIT License (MIT)
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

namespace SafetySharp.CaseStudies.HemodialysisMachine.Modeling.ExtracorporealBloodCircuit
{
	using SafetySharp.Modeling;

	public class VenousSafetyDetector : Component
	{
		public readonly BloodFlowInToOut MainFlow = new BloodFlowInToOut();

		public bool DetectedGasOrContaminatedBlood = false;

		[Provided]
		public virtual void SetMainFlow(Blood toSuccessor, Blood fromPredecessor)
		{
			toSuccessor.CopyValuesFrom(fromPredecessor);
			if (fromPredecessor.GasFree == false || fromPredecessor.ChemicalCompositionOk != true)
			{
				DetectedGasOrContaminatedBlood = true;
			}
			else
			{
				DetectedGasOrContaminatedBlood = false;
			}
		}

		[Provided]
		public void SetMainFlowSuction(Suction fromSuccessor, Suction toPredecessor)
		{
			toPredecessor.CopyValuesFrom(fromSuccessor);
		}

		public VenousSafetyDetector()
		{
			MainFlow.UpdateBackward=SetMainFlowSuction;
			MainFlow.UpdateForward=SetMainFlow;
		}

		public readonly Fault SafetyDetectorDefect = new TransientFault();

		[FaultEffect(Fault = nameof(SafetyDetectorDefect))]
		public class SafetyDetectorDefectEffect : VenousSafetyDetector
		{
			[Provided]
			public override void SetMainFlow(Blood toSuccessor, Blood fromPredecessor)
			{
				toSuccessor.CopyValuesFrom(fromPredecessor);
				DetectedGasOrContaminatedBlood = false;
			}
		}
	}
}