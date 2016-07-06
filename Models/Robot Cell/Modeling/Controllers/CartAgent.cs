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

namespace SafetySharp.CaseStudies.RobotCell.Modeling.Controllers
{
	using Plants;

	internal class CartAgent : Agent
	{
		public CartAgent(Cart cart)
		{
			Cart = cart;
		}

		public Cart Cart { get; }

		public override void TakeResource(Agent agent)
		{
			// If we fail to move to the robot, the cart loses its route
			if (Cart.MoveTo(((RobotAgent)agent).Robot))
				return;

			Disconnect(this, agent);
			Disconnect(agent, this);
            CheckConstraints();
        }

		public override void PlaceResource(Agent agent)
		{
			// If we fail to move to the robot, the cart loses its route
			if (Cart.MoveTo(((RobotAgent)agent).Robot))
				return;

			Disconnect(this, agent);
			Disconnect(agent, this);
            CheckConstraints();
        }

		public override void OnReconfigured()
		{
			base.OnReconfigured();

			// For now, the resource disappears magically...
			Cart.LoadedWorkpiece = null;
		}

		protected override bool CheckInput(Agent agent)
		{
			return Cart.CanMove(((RobotAgent)agent).Robot);
		}

		protected override bool CheckOutput(Agent agent)
		{
			return Cart.CanMove(((RobotAgent)agent).Robot);
		}
	}
}